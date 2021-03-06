properties {
	$pwd = Split-Path $psake.build_script_file	
	$build_directory  = "$pwd\output\condep-samples"
	$configuration = "Release"
	$nunitPath = "$pwd\..\src\packages\NUnit.Runners.2.6.3\tools"
	$nuget = "$pwd\..\tools\nuget.exe"
}
 
include .\..\tools\psake_ext.ps1

task default -depends Build-All, Pack-All #, Test-All, Pack-All

task Build-All -depends Clean, RestoreNugetPackages, Build
task Test-All -depends Test
task Pack-All -depends Make-DeploymentArtifact

task RestoreNugetPackages {
	Exec { & $nuget restore "$pwd\..\src\condep-samples.sln" }
}

task Build {
	Exec { msbuild "$pwd\..\src\condep-samples.sln" /t:Build /p:Configuration=$configuration /p:OutDir=$build_directory /p:GenerateProjectSpecificOutputFolder=true}
}

task Test {
	Exec { & $nunitPath\nunit-console.exe $build_directory\ConDep.Samples.Tests\ConDep.Samples.Tests.dll /nologo /nodots /xml=$build_directory\TestResult.xml }
}

task Clean {
	Write-Host "Cleaning Build output"  -ForegroundColor Green
	Remove-Item $build_directory -Force -Recurse -ErrorAction SilentlyContinue
    Remove-Item .\output\artifacts\ -Force -Recurse -ErrorAction SilentlyContinue
}

task Make-DeploymentArtifact {
    New-Item .\output\artifacts -ItemType Directory

    Copy-Item .\output\condep-samples\ConDep.Samples.Deployment\* .\output\artifacts\

    New-Item .\output\artifacts\_PublishedServices -ItemType Directory    
    Copy-Item .\output\condep-samples\ConDep.Samples.WindowsService .\output\artifacts\_PublishedServices\ConDep.Samples.WindowsService -Recurse

    New-Item .\output\artifacts\_PublishedWebsites -ItemType Directory    
    Copy-Item .\output\condep-samples\ConDep.Samples.WebApi\_PublishedWebsites\ConDep.Samples.WebApi .\output\artifacts\_PublishedWebsites\ConDep.Samples.WebApi -Recurse
    Copy-Item .\output\condep-samples\ConDep.Samples.WwwApplication\_PublishedWebsites\ConDep.Samples.WwwApplication .\output\artifacts\_PublishedWebsites\ConDep.Samples.WwwApplication -Recurse
}