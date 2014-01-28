properties {
    $project = 'All'
	$configuration = 'Debug'
	$base_dir  = resolve-path .	
    $release_path = "$base_dir\release"
}

task default -depends Compile

task Release -depends Clean, Compile, Test

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
    Ensure-Release-Folder
    $test_assemblies = Calculate-Test-Assemblies $project $base_dir $configuration
    Run-Tests $test_assemblies
    Report-On-Test-Results
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
    return "$base\src\$name.Tests\bin\$configuration\$name.Tests.dll"
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

function Ensure-Release-Folder()
{
    $exists = Test-Path $release_path
    if ($exists -eq $false)
    {
        md $release_path | Out-Null
    }
}