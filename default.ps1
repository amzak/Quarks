Properties {
    $basedir = Get-Location
    $outputdir = Join-Path $basedir 'Build'
	$nugetdir = Join-Path $basedir 'Nuget'
    $quotedoutputdir = '"' + $outputdir + '"'
    $frameworks = '4.0', '4.5'
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
	
	$projects = @(Get-ChildItem -Include *.csproj -Exclude *.Tests.csproj -Recurse)
	foreach($project in $projects)
	{
		foreach($framework in $frameworks)
		{
			$projectname = $project.Name.Replace(".csproj", [System.String]::Empty)
			$dir = "$nugetdir\$projectname\lib\net$framework" + '\'
			$config = "Release"
			Exec {
				msbuild $project.FullName /p:OutDir=$dir;Configuration=$config;TargetFrameworkVersion=V$framework;CustomAfterMicrosoftCommonTargets=$basedir\SkipCopyLocal.targets /verbosity:minimal
			}
		}
	}
}