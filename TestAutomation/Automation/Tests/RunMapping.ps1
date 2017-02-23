param([string]$trxDir)

    $path = $PSScriptRoot + '\Mapping.ps1'

    ForEach ($file in Get-ChildItem $trxDir -Filter *.trx -Recurse) 
    {
        try
        {
            Write-Host -ForegroundColor Green "Found: " $file.FullName

            $fullPath = $file.FullName
            $argumentList = @()
            $argumentList += ("-trxFile", "'$fullPath'")

            $tests = Invoke-Expression "& `"$path`"$argumentList"
            Write-Host -ForegroundColor Green "Ran: " $file.fullName
        }
        catch
        {
            Write-Host -ForegroundColor Yellow "Did not run: " $file.fullName
                    Write-Host -ForegroundColor Red $_.Exception|format-list -force
        Write-Host -ForegroundColor Red $_.ScriptStackTrace
        }
    }