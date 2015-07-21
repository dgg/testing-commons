$script_directory = Split-Path -parent $PSCommandPath
$base_dir = Resolve-Path $script_directory\..

Remove-Module [T]esting.Commons
Import-Module "$script_directory\Testing.Commons.psm1" -DisableNameChecking

function Push-Package($package)
{
	$nuget = Join-Path $base_dir tools\nuget\nuget.exe
	$release_dir = Join-Path $base_dir release

	$pkg = Get-ChildItem -File "$release_dir\$package*.nupkg" |
		? { $_.Name -match "$package\.(\d(?:\.\d){3})" }  | 
		% { 
			& $nuget push $_.FullName
			Throw-If-Error
		}
}

$title = "Push Packages"
$yes = New-Object System.Management.Automation.Host.ChoiceDescription "&Yes", `
    "Push the package to nuget.org."

$no = New-Object System.Management.Automation.Host.ChoiceDescription "&No", `
    "Does not push the package to nuget.org."

$options = [System.Management.Automation.Host.ChoiceDescription[]]($yes, $no)

('Testing.Commons', 'Testing.Commons.NUnit', 'Testing.Commons.ServiceStack') |
	% {
	$package = $_
	$message = "Do you want to push $package.*.nupkg to NuGet?"
	$result = $host.ui.PromptForChoice($title, $message, $options, 1)
	switch ($result)
	{
		0 {
			"Pushing $package package..."
			Push-Package $package
		}
		1 {"No-op."}
	}
}