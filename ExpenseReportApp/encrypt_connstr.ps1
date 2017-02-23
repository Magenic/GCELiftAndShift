$username = "Magenicons.net\gcpadmin"
$password = "M@genicons"
$secstr = New-Object -TypeName System.Security.SecureString
$password.ToCharArray() | ForEach-Object {$secstr.AppendChar($_)}
$cred = new-object -typename System.Management.Automation.PSCredential -argumentlist $username,
$secstr
Invoke-Command -ScriptBlock {c:\windows\microsoft.net\framework64\v4.0.30319\aspnet_regiis -pef "connectionStrings" "c:\inetpub\wwwroot" -prov "DataProtectionConfigurationProvider"} -ComputerName 130.211.216.234  -Credential $cred
