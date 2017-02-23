$myArray = New-Object System.Collections.ArrayList

foreach ($arg in $args)
{
  $myArray.Add($arg)
}

$webConfig = $PSScriptRoot + '\App.config'
$doc = (Get-Content $webConfig) -as [Xml]

for($i=0;  $i -lt $myArray.Count; $i += 2)
{
    $obj = $doc.configuration.MagenicMaqs.add | where {$_.Key -eq $myArray[$i]}
    $obj.value = $myArray[$i+1]
}

$doc.Save($webConfig)