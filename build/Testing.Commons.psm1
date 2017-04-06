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
	copy-binaries $base $configuration
	copy-sources $base $configuration
}

function copy-binaries($base, $configuration)
{
	$release_bin_dir = Join-Path $base release\lib\Net40
	
	$commons = Join-Path $base \src\Testing.Commons\bin\$configuration\
	$nunit = Join-Path $base \src\Testing.Commons.NUnit\bin\$configuration\
	Get-ChildItem -Path ($commons, $nunit) -filter 'Testing.*' |
		? { $_.Name -match '.dll|.XML' } |
		Copy-Item -Destination $release_bin_dir
	
}

function copy-sources()
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

	Get-ChildItem -File -Filter '*.nuspec' -Path $base  | 
		% { 
			& $nuget pack $_.FullName -OutputDirectory $release_dir -BasePath $release_dir /verbosity quiet
			Throw-If-Error
		}
}

function get-version-from-package($base, $packageFragment)
{
	$pkgVersion = Get-ChildItem -File "$base\release\$packageFragment*.nupkg" |
		? { $_.Name -match "$packageFragment\.(\d(?:\.\d){3})" } |
		select -First 1 -Property @{ Name = "value"; Expression = {$matches[1]} }
		
	return $pkgVersion.value
}

export-modulemember -function Throw-If-Error, Ensure-Release-Folders, Copy-Artifacts, Generate-Packages