param([string]$trxFile)

# Check if we have the full file path, if not assume the file is in the same directory as the script
$trxFile = If ($trxFile -contains "\") {$trxFile} Else {[io.path]::combine($PSScriptRoot, $trxFile)}

# Calculate the GUID for a fully qualified test name
function GetGuid($fullName)
{
    $enc = [system.Text.Encoding]::Unicode
    $sha1 = New-Object System.Security.Cryptography.SHA1CryptoServiceProvider 
    $comp = $sha1.ComputeHash($enc.GetBytes($fullName))
    $bytes = new-object byte[] 16
    [array]::copy($comp, $bytes, 16)
    return New-Object Guid @(,$bytes)
}

# Restructure data driven results
function BreakupDataDriven($trxFile)
{
	$parentEx = @{}
	$mappings = @{}
	$testResults = @{}

	$remove = New-Object System.Collections.ArrayList
	$parentExecution = New-Object System.Collections.ArrayList

	$doc = (Get-Content "$trxFile") -as [Xml]

	# Loop over the test definitions
	 foreach ($node in $doc.TestRun.TestDefinitions.UnitTest) 
	 {
		$name = $node.TestMethod.name.split('\(')[0].trim()
		$fullName = $node.TestMethod.className + "." + $name

		 if($node.TestMethod.name -match '\('){
            Write-Host $name

			 if(-not $parentEx.ContainsKey($fullName))
			 {
				$parentEx.Add($fullName, (New-Object System.Collections.ArrayList))
				$parentEx.Item($fullName).Add($node.Execution.id) 
				$mappings.Add($fullName, (New-Object System.Collections.ArrayList))
				$testResults.Add($fullName, (New-Object System.Collections.ArrayList))

				$node.name = $name
				$node.TestMethod.name = $name
			 }
			 else
			 {
				$remove.Add($node.Execution.id)
				$node.ParentNode.RemoveChild($node)
			 }


			 $mappings.Item($fullName).Add($node.Execution.id)
		}
	}

	foreach ($node in $doc.TestRun.TestEntries.TestEntry) 
	{
		if ($remove.Contains($node.executionId))
		{
			$node.ParentNode.RemoveChild($node)
		}
	}

	foreach ($node in $doc.TestRun.Results.UnitTestResult) 
	{

		foreach ($key in $mappings.Keys)
		{
			if($mappings[$key].Contains($node.executionId))
			{
				$node.SetAttribute("dataRowInfo", $testResults[$key].Count);
				$testResults[$key].Add($node)

				$node.SetAttribute("resultType", "DataDrivenDataRow");
				$node.SetAttribute("parentExecutionId", $parentEx[$key]);
				$node.SetAttribute("testId", ($testResults[$key])[0].testId);
			    $node.ParentNode.RemoveChild($node)
			}
		}
	}

	$extraPass = 0
	$extraFail = 0

	foreach  ($key in $testResults.Keys)
	{

		$firstNode = $testResults[$key][0]
		$lastNode = $testResults[$key][-1]


		$begin = [datetime]$firstNode.startTime
		$end   = [datetime]$lastNode.endTime
		$duration = $end - $begin

		$run = 0
		$failed = 0

		$innerResults = $doc.CreateElement("InnerResults", $doc.DocumentElement.NamespaceURI)

		 foreach($node in $testResults[$key])
		 {
		 $node.RemoveAttribute("xmlns");
			$node.testId = $firstNode.testId
			$node.executionId = [guid]::NewGuid().ToString()
			$innerResults.AppendChild($node)

			$run++

			if($node.outcome  -ne  "Passed")
			{
				$failed++
			}
		 }

		$resultsNode = $doc.TestRun.ChildNodes | where {$_.name -like 'Results'} | Select -First 1
		$parentResult = $doc.CreateElement("UnitTestResult", $doc.DocumentElement.NamespaceURI)
		$parentResult.SetAttribute("executionId", $parentEx[$key])
		$parentResult.SetAttribute("testId", $firstNode.testId)
		$parentResult.SetAttribute("testName",$key.split('.')[-1].trim())
		$parentResult.SetAttribute("computerName", $firstNode.computerName)
		$parentResult.SetAttribute("duration", $duration)
		$parentResult.SetAttribute("startTime", $firstNode.startTime)
		$parentResult.SetAttribute("endTime", $lastNode.endTime)
		$parentResult.SetAttribute("testType", $firstNode.testType)
		$parentResult.SetAttribute("testListId", $firstNode.testListId)
		$parentResult.SetAttribute("relativeResultsDirectory", $firstNode.relativeResultsDirectory)
		$parentResult.SetAttribute("resultType", "DataDrivenTest")
		$counters = $doc.CreateElement("Counters", $doc.DocumentElement.NamespaceURI)
		$counters.SetAttribute("passed", $run - $failed) 
		$counters.SetAttribute("failed", $failed) 
		$counters.SetAttribute("timeout", "0") 
		$counters.SetAttribute("aborted", "0") 
		$counters.SetAttribute("inconclusive", "0")
		$counters.SetAttribute("passedButRunAborted", "0")
		$counters.SetAttribute("notRunnable", "0")
		$counters.SetAttribute("notExecuted", "0")
		$counters.SetAttribute("disconnected", "0") 
		$counters.SetAttribute("warning", "0")
		$counters.SetAttribute("error", "0") 
		$counters.SetAttribute("completed", "0")
		$counters.SetAttribute("inProgress", "0")
		$counters.SetAttribute("pending", "0")

        if($failed -gt 0)
		{
			$parentResult.SetAttribute("outcome", "Failed")
			$extraFail++
		}
		else
		{
			$parentResult.SetAttribute("outcome", "Passed")
			$extraPass++
		}

		$parentResult.AppendChild($counters)
		$parentResult.AppendChild($innerResults)
		$resultsNode.AppendChild($parentResult)
	}

	$doc.TestRun.ResultSummary.Counters.total = [string]([int]$doc.TestRun.ResultSummary.Counters.total + $extraPass + $extraFail)
	$doc.TestRun.ResultSummary.Counters.executed = [string]([int]$doc.TestRun.ResultSummary.Counters.executed + $extraPass + $extraFail)
	$doc.TestRun.ResultSummary.Counters.failed = [string]([int]$doc.TestRun.ResultSummary.Counters.failed + $extraFail)
	$doc.TestRun.ResultSummary.Counters.passed = [string]([int]$doc.TestRun.ResultSummary.Counters.passed + $extraPass)

	$doc.save($trxFile)
}

# Update the trx file and pull out mapping data
function UpdateTrxMappingsAndGetMappings($trxFile)
{
    $dict = @{}
    $tests = @()
    $doc = (Get-Content "$trxFile") -as [Xml]
    
    # Loop over the test definitions
     foreach ($node in $doc.TestRun.TestDefinitions.UnitTest) {

        # Get the test mapping data
        $fullName = $node.TestMethod.className + "." + $node.TestMethod.name
        $possibleTfsId = $node.TestMethod.name
        $possibleTfsId = If ($possibleTfsId -Match "(\d+)$") {$matches[1]} Else {-1}
        $guid = GetGuid($fullName)

        $dict.Add($node.id, $guid.ToString())

        $testInfo = New-Object System.Object
        $testInfo | Add-Member -type NoteProperty -name Guid -Value $guid
        $testInfo | Add-Member -type NoteProperty -name Storage -Value $node.storage.split('\')[-1]
        $testInfo | Add-Member -type NoteProperty -name Name -Value $fullName
        $testInfo | Add-Member -type NoteProperty -name TfsId -Value $possibleTfsId
        $tests+=$testInfo
    }

    # Update the TRX file
    foreach ($guidReplace in $dict.GetEnumerator()) {
        (Get-Content $trxFile) -replace $guidReplace.Name, $guidReplace.Value | Set-Content $trxFile
    }

    # Return the test mappings
    return $tests
}

Write-Host  Breakup
# Make sure data driving is respected
BreakupDataDriven($trxFile)

Write-Host  UpdateMapping
# Update the trx file and get the mappings
$tests = UpdateTrxMappingsAndGetMappings($trxFile)

# Let the user know the process has completed
Write-Host Finished