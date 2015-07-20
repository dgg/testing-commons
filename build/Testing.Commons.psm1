function Throw-If-Error
{
	[CmdletBinding()]
	param(
		[Parameter(Position=0,Mandatory=0)][string]$errorMessage = ('Error executing command {0}' -f $cmd)
	)
	if ($global:lastexitcode -ne 0) {
		throw ("Exec: " + $errorMessage)
	}
}

function Ensure-Release-Folders($base)
{
	("$base\release\lib\net40", "$base\release\content\Support") |
		% { New-Item -Type directory $_ -Force | Out-Null }
}

function Copy-Artifacts($base, $configuration)
{
	CopyBinaries $base $configuration
	CopySources $base $configuration
	CopyPackageManifests $base
}

function CopyBinaries($base, $configuration)
{
	$release_bin_dir = Join-Path $base release\lib\Net40
	
	<#Copy-Item $base\src\Testing.Commons\bin\$configuration\Testing.Commons.dll $release_bin_dir
	Copy-Item $base\src\Testing.Commons\bin\$configuration\Testing.Commons.XML $release_bin_dir
	Copy-Item $base\src\Testing.Commons.NUnit\bin\$configuration\Testing.Commons.NUnit.dll $release_bin_dir
	Copy-Item $base\src\Testing.Commons.NUnit\bin\$configuration\Testing.Commons.NUnit.XML $release_bin_dir#>
	
	$commons = Join-Path $base \src\Testing.Commons\bin\$configuration\
	$nunit = Join-Path $base \src\Testing.Commons.NUnit\bin\$configuration\
	Get-ChildItem -Path ($commons, $nunit) -filter 'Testing.*' |
		? {$_.Name -match '.dll|.XML'} |
		Copy-Item -Destination $release_bin_dir
	
}

function CopyPackageManifests($base){
    $release_dir = Join-Path $base release
	
	Get-ChildItem $base -Filter '*.nuspec' |
		Copy-Item -Destination $release_dir
}

function CopySources()
{
	$src = Join-Path $base src\Testing.Commons.ServiceStack\v3
	$release_src_dir = Join-Path $base release\content\Support

	Get-ChildItem -Path $src -Filter "*.cs" |
		Copy-Item -Destination $release_src_dir
}

function Generate-Packages($base)
{
	$nuget = Join-Path $base tools\nuget\nuget.exe
	$release_dir = Join-Path $base release

	Get-ChildItem -File -Filter '*.nuspec' -Path $release_dir  | 
		% { 
			& $nuget pack $_.FullName /o $release_dir /verbosity quiet
			Throw-If-Error
		}
}

function Generate-Zip-Files($base)
{
	$version = GetVersionFromPackage $base 'Testing.Commons'
	('Testing.Commons.dll', 'Testing.Commons.XML') |
		% { ZipBin $base $version 'Testing.Commons' $_ | Out-Null }
		
	$version = GetVersionFromPackage $base 'Testing.Commons.NUnit'
	('Testing.Commons.NUnit.dll', 'Testing.Commons.NUnit.XML') |
		% { ZipBin $base $version 'Testing.Commons.NUnit' $_ | Out-Null }
}

function GetVersionFromPackage($base, $packageFragment)
{
	$pkgVersion = Get-ChildItem -File "$base\release\$packageFragment*.nupkg" |
		? { $_.Name -match "$packageFragment\.(\d(?:\.\d){3})" } |
		select -First 1 -Property @{ Name = "value"; Expression = {$matches[1]} }
		
	return $pkgVersion.value
}

export-modulemember -function Throw-If-Error, Ensure-Release-Folders, Copy-Artifacts, Generate-Packages