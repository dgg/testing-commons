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
	
	run_v2_tests $base_dir $release_dir ($commons, $serviceStack)
	report-on-test-results $base_dir $release_dir

	run_v3_tests $base_dir $release_dir ($nunit)
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

function run_v2_tests($base, $release, $test_assemblies){
	$nunit_console = "$base\tools\NUnit.Runners.lite\nunit-console.exe"

	exec { & $nunit_console $test_assemblies /nologo /nodots /result="$release\TestResult_v2.xml"  }
}

function run_v3_tests($base, $release, $test_assemblies){
	$console_dir = Get-ChildItem $base\tools\* -Directory | where {$_.Name.StartsWith('NUnit.ConsoleRunner')}
    # get first directory
    $console_dir = $console_dir[0]

	$nunit_console = Join-Path $console_dir tools\nunit3-console.exe
	exec { & $nunit_console $test_assemblies --result:"$release\TestResult_v3.xml" --noheader  }
}

function report-on-test-results($base, $release)
{
	$nunit_summary_path = "$base\tools\NUnitSummary"
	$nunit_summary = Join-Path $nunit_summary_path "nunit-summary.exe"

	$alternative_details = Join-Path $nunit_summary_path "AlternativeNUnitDetails.xsl"
	$alternative_details = "-xsl=" + $alternative_details

	exec { & $nunit_summary $release\TestResult.xml -html -o="$release\TestSummary.htm" }
	exec { & $nunit_summary $release\TestResult.xml -html -o="$release\TestDetails.htm" $alternative_details -noheader }
}