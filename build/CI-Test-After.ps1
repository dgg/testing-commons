$configuration = Get-ChildItem Env:CONFIGURATION
Run-Core-Tests . $configuration.Value

$wc = New-Object 'System.Net.WebClient'
$wc.UploadFile("https://ci.appveyor.com/api/testresults/nunit3/$($env:APPVEYOR_JOB_ID)", (Resolve-Path .\release\Testing.Commons.TestResult.core.xml))
$wc.UploadFile("https://ci.appveyor.com/api/testresults/nunit3/$($env:APPVEYOR_JOB_ID)", (Resolve-Path .\release\Testing.Commons.NUnit.TestResult.core.xml))

Generate-Packages .