function push-zip-artifact($packageFragment, $artifactName)
{
	$pkg = Get-ChildItem -File ".\release\$packageFragment*.nupkg" |
		? { $_.Name -match "$packageFragment\.(\d(?:\.\d){3})" }
	Push-AppveyorArtifact $pkg -DeploymentName $artifactName
}

push-zip-artifact 'Testing.Commons' 'testing_commons'

push-zip-artifact 'Testing.Commons.NUnit' 'testing_commons_nunit'

push-zip-artifact 'Testing.Commons.ServiceStack' 'testing_commons_service_stack'