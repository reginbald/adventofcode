$data = [System.Int32[]] (Get-Content -Path "$PSScriptRoot\data.txt")

$increases = 0
for($i = 1; $i -lt $data.length; $i++){
    if($data[$i - 1] -lt $data[$i]) {
        $increases++
        Write-Host "$($data[$i]) (increased)"
    } else {
        Write-Host "$($data[$i]) (decreased)"
    }
}
$increases