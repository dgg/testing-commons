properties {
    $project = 'All'
	$configuration = 'Debug'
	$base_dir  = resolve-path .	
    $release_path = "$base_dir\release"
}

task default -depends Clean #, Compile, Test, Deploy

task Clean {
    exec { msbuild .\Testing.Commons.sln /t:clean /p:configuration=$configuration /m }
    Remove-Item $base_dir\*.htm -Force
    Remove-Item $base_dir\*.xml -Force
    Remove-Item $release_path -Recurse -Force -ErrorAction SilentlyContinue | Out-Null
}

task Compile {
    exec { msbuild .\Testing.Commons.sln /p:configuration=$configuration /m }
}

task Test {
    Ensure-Release-Folders
    $test_assemblies = Calculate-Test-Assemblies $project $base_dir $configuration
    Run-Tests $test_assemblies
    Report-On-Test-Results
}

task Deploy {
    $release_folders = Ensure-Release-Folders

    $commons = Src-Folder $base_dir $configuration "Testing.Commons"
    $nunit = Src-Folder $base_dir $configuration "Testing.Commons.NUnit"
    $serviceStack = Src-Folder $base_dir $configuration "Testing.Commons.ServiceStack"
       
    Get-ChildItem -Path ($commons, $nunit, $serviceStack) -Filter 'Testing.*.dll' |
        Copy-To $release_folders
    Get-ChildItem -Path ($commons, $nunit, $serviceStack) -Filter 'Testing.*.pdb' |
        Copy-Item -Destination $release_path
    Get-ChildItem -Path ($commons, $nunit, $serviceStack) -Filter 'Testing.*.xml' |
        Copy-To $release_folders
    Get-ChildItem $base_dir -Filter '*.nuspec' |
        Copy-Item -Destination $release_path
}

task Pack {
    $release_folders = Ensure-Release-Folders

    $nuget = "$base_dir\tools\nuget\nuget.exe"

    Get-ChildItem -File -Filter '*.nuspec' -Path $release_path  | 
        ForEach-Object { exec { & $nuget pack $_.FullName /o $release_path } }
}

function Calculate-Test-Assemblies ($set, $base, $config)
{
    $assemblies = @()
    if ($set -eq 'Commons' -or $set -eq 'All'){
        $assemblies += Test-Assembly $base $config 'Testing.Commons'
    }
    if ($set -eq 'NUnit' -or $set -eq 'All'){
        $assemblies += Test-Assembly $base $config 'Testing.Commons.NUnit'
    }
    if ($set -eq 'ServiceStack' -or $set -eq 'All'){
        $assemblies += Test-Assembly $base $config 'Testing.Commons.ServiceStack'
    }
    return $assemblies
}

function Test-Assembly($base, $config, $name)
{
    return "$base\src\$name.Tests\bin\$config\$name.Tests.dll"
}

function Src-Folder($base, $config, $name)
{
    return "$base\src\$name\bin\$config\"
}

function Run-Tests($test_assemblies){
    $nunit_console = "$base_dir\tools\NUnit.Runners.lite\nunit-console.exe"

	exec { & $nunit_console $test_assemblies /nologo /nodots /result="$release_path\TestResult.xml"  }
}

function Report-On-Test-Results()
{
    $nunit_summary_path = "$base_dir\tools\NUnitSummary"
    $nunit_summary = Join-Path $nunit_summary_path "nunit-summary.exe"
    $alternative_details = Join-Path $nunit_summary_path "AlternativeNUnitDetails.xsl"
    $alternative_details = "-xsl=" + $alternative_details

    exec { & $nunit_summary $release_path\TestResult.xml -html -o="release\TestSummary.htm" }
    exec { & $nunit_summary $release_path\TestResult.xml -html -o="release\TestDetails.htm" $alternative_details }
}

function Ensure-Release-Folders()
{
    $release_folders = ($release_path, "$release_path\lib\net40")
    foreach ($f in $release_folders) { md $f -force | Out-Null }
    return $release_folders
}

function Copy-To($destinations)
{
    Process { foreach ($d in $destinations) { Copy-Item -Path $_.FullName -Destination $d } }
}