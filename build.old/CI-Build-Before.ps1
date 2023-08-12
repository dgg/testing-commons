Remove-Module [T]esting.Commons
Import-Module .\build\Testing.Commons.psm1 -DisableNameChecking

Ensure-Release-Folders .

Restore-Packages .