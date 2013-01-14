Properties {
    $basedir = Get-Location
    $outputdir = Join-Path $basedir 'Build'
    $quotedoutputdir = '"' + $outputdir + '"'
    $framework = 4.0
    $v4_net_version = (ls "$env:windir\Microsoft.NET\Framework64\v$framework*").Name
    $msbuildpath = "$env:windir\Microsoft.NET\Framework\$v4_net_version\MsBuild.exe"
    
}

Task Default -depends Test

Task Build -depends Clean {
    if(!$solution)
    {
        $solution = Get-Item -Path $basedir -Include *.sln
    }
    
    Exec { 
       & $msbuildpath $solution "/p:OutDir=$quotedoutputdir"
    }
}

Task Clean {
    if(Test-Path $outputdir)
    {
        Remove-Item -Path $outputdir -Recurse -Force
    }
}

Task Test -depends Build {
    
    $nunit = (Get-ChildItem 'nunit-console.exe' -Path $basedir -Recurse).FullName
    $assemblies = @(Get-ChildItem *.Tests.dll -Path $outputdir | %{ $_.FullName} )
    if($assemblies)    
    {
        $linearAsms = [System.String]::Join(" ", $assemblies)
        Exec {
           & $nunit $linearAsms /domain:single
        }
    }
    
}