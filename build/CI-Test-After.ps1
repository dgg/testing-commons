$configuration = Get-ChildItem Env:CONFIGURATION
Run-Core-Tests . $configuration.Value

Generate-Packages .