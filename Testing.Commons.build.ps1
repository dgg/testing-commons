properties {
    $project = 'All'
	$configuration = 'Debug'
	$base_dir  = resolve-path .	
    $release_path = "$base_dir\release"
}

task default -depends Compile

task Release -depends Compile, Test

task Compile { 
    msbuild .\Testing.Commons.sln
}

task Test {
    $test_assemblies = Calculate-Test-Assemblies $project $base_dir $configuration
    Run-Tests $test_assemblies
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

	Exec { & $nunit_console $test_assemblies /nologo /nodots }
}