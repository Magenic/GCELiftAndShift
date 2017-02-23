
param([string]$trxFolder, [string]$suiteIds, [string]$configId, [string]$collection, [string]$project, [string]$auth,  [string]$user)

[string[]] $regKeys = "hklm:\HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\VisualStudio\14.0", 
"hklm:\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\14.0", 
"hklm:\HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\VisualStudio\13.0",
"hklm:\HKEY_LOCAL_MACHINE\SOFTWARE\\Microsoft\VisualStudio\13.0", 
"hklm:\HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\VisualStudio\12.0",
"hklm:\HKEY_LOCAL_MACHINE\SOFTWARE\\Microsoft\VisualStudio\12.0", 
"hklm:\HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\VisualStudio\11.0",
"hklm:\HKEY_LOCAL_MACHINE\SOFTWARE\\Microsoft\VisualStudio\11.0"

$tcmPath

foreach ($key in $regKeys) 
{ 
    if ((Test-Path $key) -eq $True -AND (Get-ItemProperty $key).InstallDir -ne $null)
    {
        $tcmPath = (Get-ItemProperty $key).InstallDir
        $tcmPath = [io.path]::combine($tcmPath, "tcm.exe");

        break
    }
}


foreach ($file in Get-ChildItem $trxFolder -Filter *.trx -Recurse) 
{
    foreach ($suiteId in $suiteIds.split(",")) 
    {
        $newName = $file.FullName + ".trx"

        $doc = (Get-Content $file.FullName) -as [Xml]
        $doc.TestRun.id = [guid]::newguid().ToString().ToLower()
        $doc.Save($newName)
        
        $Parms= "run /publish /suiteid:{0} /configid:{1} /resultsfile:""{2}"" /collection:{3} /teamproject:{4} /login:{5} /allowalternatecredentials /resultowner:""{6}""" -f $suiteId,$configId,$newName,$collection,$project,$auth,$user
        $Prms = $Parms.Split(" ")
        & "$tcmPath" $Prms 

       Remove-Item "$newName" -Confirm:$false

    }
}

   

