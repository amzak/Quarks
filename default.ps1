$scriptPath = Split-Path $MyInvocation.InvocationName
$psakeModule = Get-ChildItem psake.psm1 -Path $scriptPath -Recurse

Import-Module $psakeModule.FullName -force

Invoke-psake -framework '4.0' -buildFile build.ps1  