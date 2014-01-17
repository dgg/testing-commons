[CmdletBinding()]
Param(
	[Parameter(Mandatory=$False)]
	[ValidateSet('All', 'Commons', 'NUnit', 'ServiceStack')]
	[string]$project = 'All'
)

function Main () {
    $script_directory = Split-Path -parent $PSCommandPath   
    $base_dir  = resolve-path $script_directory

	$psake_dir = Get-ChildItem $base_dir\tools\* -Directory | where {$_.Name.StartsWith('psake')}
    # get first directory
    $psake_dir = $psake_dir[0]

    & $psake_dir\tools\psake.ps1 $base_dir\Testing.Commons.build.ps1 Release -properties @{"project"=$project}
}
Main