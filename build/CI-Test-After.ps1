$configuration = Get-ChildItem Env:CONFIGURATION
Run-Core-Tests . $configuration

Generate-Packages .