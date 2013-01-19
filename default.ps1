﻿Properties {
    $basedir = Get-Location
    $outputdir = Join-Path $basedir 'Build'
	$nugetdir = Join-Path $basedir 'Nuget'
    $quotedoutputdir = '"' + $outputdir + '"'
    $frameworks = '4.0','4.5'

    $msbuildpath = "$env:windir\Microsoft.NET\Framework\$v4_net_version\MsBuild.exe"
}

include .\utils.ps1

Task Default -depends PrepareNupack

Task Clean {
    if(Test-Path $outputdir)
    {
        Remove-Item -Path $outputdir -Recurse -Force
    }
	
	if(Test-Path $nugetdir)
	{
		Remove-Item -Path $nugetdir -Recurse -Force
	}
}

Task SetVersion {
	$version = Get-Version
	$SolutionVersion = Generate-Assembly-Info $version["version"] $version["commit"] $version["dirty"]
	$SolutionVersion > SolutionVersion.cs 
}

Task Build -depends Clean, SetVersion {
    if(!$solution)
    {
        $solution = Get-Item -Path .\ -Include *.sln
    }
    
    Exec { 
		msbuild $solution /p:OutDir=$quotedoutputdir\ /verbosity:minimal 		
    }
}

Task Test -depends Build {
    
    $nunit = (Get-ChildItem 'nunit-console.exe' -Path $basedir -Recurse).FullName
    $assemblies = @(Get-ChildItem *.Tests.dll -Path $outputdir | %{ $_.FullName} )
    
	if($assemblies)    
    {
        $linearAsms = [System.String]::Join(" ", $assemblies)
		Write-Warning $linearAsms
		Write-Warning $nunit
        Exec {
           & $nunit $linearAsms /domain:single
        }
    }
}

Task PrepareNupack -depends Test {

    $v4_net_version = (ls "$env:windir\Microsoft.NET\Framework64\v$framework*").Name	
    $msbuild = "$env:windir\Microsoft.NET\Framework\$v4_net_version\MsBuild.exe"
    
    $projects = @(Get-ChildItem -Include *.csproj -Exclude *.Tests.csproj -Recurse)
	foreach($project in $projects)
	{
		foreach($framework in $frameworks)
		{
            $projectname = $project.Name.Replace(".csproj", [System.String]::Empty)
            $projectfile = $project.FullName
            $dotlessFramework = $framework.Replace('.','')
			$dir = "$nugetdir\$projectname\lib\net$dotlessFramework" + '\'
			$config = "Release"
			
            $props = "/p:TargetFrameworkVersion=$framework;Configuration=$config;OutDir=$dir;CustomAfterMicrosoftCommonTargets=$basedir\SkipCopyLocal.targets"
            
            Exec {
                msbuild $projectfile  /t:Rebuild $props /verbosity:minimal /nologo
            }
		}
	}
}