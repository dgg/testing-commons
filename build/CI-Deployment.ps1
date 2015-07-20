function PushPackageArtifact($packageFragment, $artifactName)
{
	$pkg = Get-ChildItem -File ".\release\$packageFragment*.nupkg" |
		? { $_.Name -match "$packageFragment\.(\d(?:\.\d){3})" }
	Push-AppveyorArtifact $pkg -DeploymentName $artifactName
}

PushPackageArtifact 'Testing.Commons' 'testing_commons'

PushPackageArtifact 'Testing.Commons.NUnit' 'testing_commons_nunit'

PushPackageArtifact 'Testing.Commons.ServiceStack' 'testing_commons_service_stack'