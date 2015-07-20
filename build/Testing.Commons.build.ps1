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

task EnsureRelease -depends ImportModule {
	Ensure-Release-Folders $base_dir
}

task ImportModule {
	Remove-Module [T]esting.Commons
	Import-Module "$base_dir\build\Testing.Commons.psm1" -DisableNameChecking
}

task Test -depends EnsureRelease {
	$commons = Test-Assembly $base_dir $configuration 'Testing.Commons'
	$nunit = Test-Assembly $base_dir $configuration 'Testing.Commons.NUnit'
	$serviceStack = Test-Assembly $base_dir $configuration 'Testing.Commons.ServiceStack'
	
	Run-Tests $base_dir $release_dir ($commons, $nunit, $serviceStack)
	Report-On-Test-Results $base_dir $release_dir
}

task CopyArtifacts -depends EnsureRelease {
	Copy-Artifacts $base_dir $configuration
}

task BuildArtifacts -depends EnsureRelease {
	Generate-Packages $base_dir
}

task ? -Description "Helper to display task info" {
	Write-Documentation
}

function Test-Assembly($base, $config, $name)
{
	return "$base\src\$name.Tests\bin\$config\$name.Tests.dll"
}

function Run-Tests($base, $release, $test_assemblies){
	$nunit_console = "$base\tools\NUnit.Runners.lite\nunit-console.exe"

	exec { & $nunit_console $test_assemblies /nologo /nodots /result="$release\TestResult.xml"  }
}

function Report-On-Test-Results($base, $release)
{
	$nunit_summary_path = "$base\tools\NUnitSummary"
	$nunit_summary = Join-Path $nunit_summary_path "nunit-summary.exe"

	$alternative_details = Join-Path $nunit_summary_path "AlternativeNUnitDetails.xsl"
	$alternative_details = "-xsl=" + $alternative_details

	exec { & $nunit_summary $release_dir\TestResult.xml -html -o="$release\TestSummary.htm" }
	exec { & $nunit_summary $release_dir\TestResult.xml -html -o="$release\TestDetails.htm" $alternative_details -noheader }
}