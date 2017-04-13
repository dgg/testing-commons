Framework "4.6"

properties {
	$configuration = 'Debug'
	$base_dir  = resolve-path ..
	$release_dir = "$base_dir\release"
}

task default -depends Clean, Compile, Test, CopyArtifacts, BuildArtifacts

task Clean -depends importModules {
	$msbuild = find_msbuild

	exec { & $msbuild "$base_dir\Testing.Commons.sln" /t:clean /p:configuration=$configuration /m /v:m }
	Remove-Item $release_dir -Recurse -Force -ErrorAction SilentlyContinue | Out-Null
}

task Compile -depends importModules {
	Restore-Packages $base_dir

	$msbuild = find_msbuild
	exec { & $msbuild "$base_dir\Testing.Commons.sln" /p:configuration=$configuration /m /v:m }
}

task ensureRelease -depends importModules {
	Ensure-Release-Folders $base_dir
}

task importModules {
	Remove-Module [T]esting.Commons
	Import-Module "$base_dir\build\Testing.Commons.psm1" -DisableNameChecking

	Remove-Module [V]SSetup
	$vssetup_dir = find_versioned_dir -base $base_dir\tools -beginning 'VSSetup'
	Import-Module  $vssetup_dir\VSSetup.psd1 -DisableNameChecking
}

task Test -depends ensureRelease {
	$commons = get-test-assembly-name $base_dir $configuration 'Testing.Commons'
	$nunit = get-test-assembly-name $base_dir $configuration 'Testing.Commons.NUnit'
	$serviceStack = get-test-assembly-name $base_dir $configuration 'Testing.Commons.ServiceStack'
	
	run_tests $base_dir $release_dir ($commons, $nunit, $serviceStack)
	
	$commons = get-test-assembly-name $base_dir $configuration 'Testing.Commons' -target 'netcoreapp1.0'
	$nunit = get-test-assembly-name $base_dir $configuration 'Testing.Commons.NUnit' -target 'netcoreapp1.0'
	exec { dotnet $commons --result:"$release_dir\Testing.Commons.TestResult.core.xml" --noheader }
	exec { dotnet $nunit --result:"$release_dir\Testing.Commons.NUnit.TestResult.core.xml" --noheader}

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

function get-test-assembly-name($base, $config, $name, $target = '')
{
	$assembly_name = "$base\src\$name.Tests\bin\$config\"
	$assembly_name = Join-Path $assembly_name $target
	$assembly_name = Join-Path $assembly_name "$name.Tests.dll"
	return $assembly_name
}

function run_tests($base, $release, $test_assemblies){
	$console_dir = find_versioned_dir -base $base\tools -beginning 'NUnit.ConsoleRunner'
	$nunit_console = Join-Path $console_dir tools\nunit3-console.exe
	exec { & $nunit_console $test_assemblies --result:"$release\TestResult.xml" --noheader  }
}

function report-on-test-results($base, $release)
{
	$nunit_orange = Join-Path $base tools\NUnitOrange\NUnitOrange.exe

	('TestResult', 'Testing.Commons.TestResult.core', 'Testing.Commons.NUnit.TestResult.core') |
	% { 
		$input_xml = Join-Path $release "$_.xml"
		$output_html = Join-Path $release "$_.html"
		exec { & $nunit_orange $input_xml $output_html } 
	}
}

function find_versioned_dir($base, $beginning)
{
	$dir = Get-ChildItem $base -Directory | where { $_.Name.StartsWith($beginning) }
	return Join-Path $base $dir[0]
}

function find_msbuild()
{
	$vs_dir = Get-VSSetupInstance | Select-Object -ExpandProperty InstallationPath
	$msbuild = Join-Path $vs_dir MSBuild\15.0\Bin\MSBuild.exe
	return $msbuild;
}