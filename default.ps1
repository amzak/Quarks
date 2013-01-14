Properties {
    $basedir = Get-Location
    $outputdir = '"' +  (Join-Path $basedir 'Build') + '"'
    $framework = 4.0
    $v4_net_version = (ls "$env:windir\Microsoft.NET\Framework64\v$framework*").Name
    $msbuildpath = "$env:windir\Microsoft.NET\Framework\$v4_net_version\MsBuild.exe"
}

Task Default -depends Build

Task Build -depends Clean {
    if(!$solution)
    {
        $solution = Get-Item -Path $basedir -Include *.sln
    }

    Exec { 
       & $msbuildpath $solution "/p:OutDir=$outputdir"
    }
}

Task Clean {
    if(Test-Path $outputdir)
    {
        Remove-Item -Path $outputdir -Recurse -Force
    }
}

