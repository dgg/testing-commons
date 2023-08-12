[CmdletBinding()]
Param(
	[ValidateSet('Debug', 'Release')]
	[string]$configuration = 'Release',
	[string]$verbosity = 'q',
	[string]$task = "Build"
)


function Clean() {
	& dotnet clean -c $configuration -v $verbosity --nologo
	$release_dir = Join-Path $base_dir release
	Remove-Item $release_dir -Recurse -Force -ErrorAction SilentlyContinue | Out-Null
}

function Restore() {
	& dotnet restore -v $verbosity
}

function Compile() {
	& dotnet build --no-restore -c $configuration -v $verbosity --nologo
}

function Test() {
	$release_dir = Join-Path $base_dir release

	$trx = Join-Path TestResults Testing.Commons.Tests.trx
	$html = Join-Path TestResults Testing.Commons.Tests.html

	& dotnet test --no-build -c $configuration --nologo -v $verbosity `
		--results-directory $release_dir `
		-l:"console;verbosity=minimal;NoSummary=true" `
		-l:"trx;LogFileName=$trx" `
		-l:"html;LogFileName=$html" `
		-- NUnit.TestOutputXml=TestResults NUnit.OutputXmlFolderMode=RelativeToResultDirectory

	# TODO: generate extra HTML reports
}

function Pack() {
	& dotnet pack --no-build -c $configuration --nologo -v $verbosity
}

function CopyArtifacts() {
	$release_dir = Join-Path $base_dir release

	Join-Path $base_dir src Testing.Commons bin $configuration |
	Get-ChildItem -Recurse |
	Where-Object { $_.Name -match '.dll|.xml|.pdb|.nupkg' } |
	Copy-Item -Destination $release_dir
}

function Publish() {
	Join-Path $base_dir src Testing.Commons bin $configuration |
	Get-ChildItem -Recurse |
	Where-Object { $_.Name -match '.nupkg' } |
	push
}

function push {
	param (
		[Parameter(ValueFromPipeline = $true)][System.IO.FileInfo]$nupkg
	)
	PROCESS {
		& dotnet nuget push -s nuget.org $nupkg.FullName
	}
}

function Build () {
	& Clean
	& Restore
	& Compile
	& Test
	& Pack
	& CopyArtifacts
}

$script_directory = Split-Path -parent $PSCommandPath
$base_dir = resolve-path $script_directory

& $task
exit $LASTEXITCODE;
