properties {
    $configuration = 'Debug'
	$base_dir  = resolve-path .	
    $release_path = "$base_dir\release"
}

task default -depends Clean, Compile, Test, Deploy, Pack

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
    Ensure-Release-Folders $release_path

    $commons = Test-Assembly $base_dir $configuration 'Testing.Commons'
    $nunit = Test-Assembly $base_dir $configuration 'Testing.Commons.NUnit'
    $serviceStack = Test-Assembly $base_dir $configuration 'Testing.Commons.ServiceStack'
    
    Run-Tests $base_dir $release_path ($commons, $nunit, $serviceStack)
    Report-On-Test-Results $base_dir
}

task Deploy {
    $release_folders = Ensure-Release-Folders  $release_path

    $commons = Bin-Folder $base_dir $configuration "Testing.Commons"
    $nunit = Bin-Folder $base_dir $configuration "Testing.Commons.NUnit"
    $serviceStack = Bin-Folder $base_dir $configuration "Testing.Commons.ServiceStack"
    $serviceStackSrc = Src-Folder $base_dir "Testing.Commons.ServiceStack"

    $beforeLast = $release_folders.Length-2
    Get-ChildItem -Path ($commons, $nunit, $serviceStack) -Filter 'Testing.*.dll' |
        Copy-To $release_folders[0..$beforeLast]

    Get-ChildItem -Path ($commons, $nunit, $serviceStack) -Filter 'Testing.*.pdb' |
        Copy-Item -Destination $release_path

    Get-ChildItem -Path ($commons, $nunit) -Filter 'Testing.*.xml' |
        Copy-To $release_folders[0..$beforeLast]

    Get-ChildItem -Path "$serviceStackSrc\v3" -Filter "*.cs" |
        Copy-Item -Destination $release_folders[$beforeLast+1]

    Get-ChildItem $base_dir -Filter '*.nuspec' |
        Copy-Item -Destination $release_path
}

task Pack {
    Ensure-Release-Folders $release_path

    $nuget = "$base_dir\tools\nuget\nuget.exe"

    Get-ChildItem -File -Filter '*.nuspec' -Path $release_path  | 
        ForEach-Object { exec { & $nuget pack $_.FullName /o $release_path } }
}

function Test-Assembly($base, $config, $name)
{
    return "$base\src\$name.Tests\bin\$config\$name.Tests.dll"
}

function Bin-Folder($base, $config, $name)
{
    $project = Src-Folder $base $name
    return Join-Path $project "bin\$config\"
}

function Src-Folder($base, $name)
{
    return "$base\src\$name\"
}

function Run-Tests($base, $release, $test_assemblies){
    $nunit_console = "$base\tools\NUnit.Runners.lite\nunit-console.exe"

	exec { & $nunit_console $test_assemblies /nologo /nodots /result="$release\TestResult.xml"  }
}

function Report-On-Test-Results($base)
{
    $nunit_summary_path = "$base\tools\NUnitSummary"
    $nunit_summary = Join-Path $nunit_summary_path "nunit-summary.exe"

    $alternative_details = Join-Path $nunit_summary_path "AlternativeNUnitDetails.xsl"
    $alternative_details = "-xsl=" + $alternative_details

    exec { & $nunit_summary $release_path\TestResult.xml -html -o="release\TestSummary.htm" }
    exec { & $nunit_summary $release_path\TestResult.xml -html -o="release\TestDetails.htm" $alternative_details }
}

function Ensure-Release-Folders($base)
{
    $release_folders = ($base, "$base\lib\net40", "$base\content\Support")

    foreach ($f in $release_folders) { md $f -Force | Out-Null }

    return $release_folders
}

function Copy-To($destinations)
{
    Process { foreach ($d in $destinations) { Copy-Item -Path $_.FullName -Destination $d } }
}