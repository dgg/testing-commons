Framework 4.5.2

properties {
	$configuration = 'Debug'
	$base_dir  = resolve-path ..
	$release_dir = "$base_dir\release"
}

task default -depends Clean, Compile, Test, CopyArtifacts, BuildArtifacts

task Clean {
	exec { msbuild "$base_dir\Testing.Commons.sln" /t:clean /p:configuration=$configuration /m }
	Remove-Item $release_dir -Recurse -Force -ErrorAction SilentlyContinue | Out-Null
}

task Compile {
	exec { msbuild "$base_dir\Testing.Commons.sln" /p:configuration=$configuration /m }
}

task ensureRelease -depends importModule {
	Ensure-Release-Folders $base_dir
}

task importModule {
	Remove-Module [T]esting.Commons
	Import-Module "$base_dir\build\Testing.Commons.psm1" -DisableNameChecking
}

task Test -depends ensureRelease {
	$commons = get-test-assembly-name $base_dir $configuration 'Testing.Commons'
	$nunit = get-test-assembly-name $base_dir $configuration 'Testing.Commons.NUnit'
	$serviceStack = get-test-assembly-name $base_dir $configuration 'Testing.Commons.ServiceStack'
	
	run_tests $base_dir $release_dir ($commons, $nunit, $serviceStack)

	report-on-test-results $base_dir $release_dir
}

task CopyArtifacts -depends ensureRelease {
	Copy-Artifacts $base_dir $configuration
}

task BuildArtifacts -depends ensureRelease {
	Generate-Packages $base_dir
}

task ? -Description "Helper to display task info" {
	Write-Documentation
}

function get-test-assembly-name($base, $config, $name)
{
	return "$base\src\$name.Tests\bin\$config\$name.Tests.dll"
}

function run_tests($base, $release, $test_assemblies){
	$console_dir = Get-ChildItem $base\tools\* -Directory | where {$_.Name.StartsWith('NUnit.ConsoleRunner')}
    # get first directory
    $console_dir = $console_dir[0]

	$nunit_console = Join-Path $console_dir tools\nunit3-console.exe
	exec { & $nunit_console $test_assemblies --result:"$release\TestResult.xml" --noheader  }
}

function report-on-test-results($base, $release)
{
	$nunit_orange = Join-Path $base tools\NUnitOrange\NUnitOrange.exe
	$input_xml = Join-Path $release TestResult.xml
	$output_html = Join-Path $release TestResult.html
	
	exec { & $nunit_orange $input_xml $output_html }
}