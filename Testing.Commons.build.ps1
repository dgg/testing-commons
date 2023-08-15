$here = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$psakePath = Join-Path $here -Child "tools/psake.4.9.0/psake.psm1"
Import-Module $psakePath

invoke-psake "$here/build/Testing.Commons.build.ps1" -Task Clean -Properties @{ 'config'='Release'; }
