$invname = $MyInvocation.InvocationName
write "Path to script $invname"
$scriptPath = Split-Path $MyInvocation.InvocationName

if($scriptPath)
{
    Set-Location $scriptPath    
}

$psakeModule = Get-ChildItem psake.psm1 -Path $scriptPath -Recurse

Import-Module $psakeModule.FullName -force

Invoke-psake -framework '4.0' -buildFile default.ps1