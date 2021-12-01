$data = [System.Int32[]] (Get-Content -Path "$PSScriptRoot\data.txt")

$increases = 0
for($i = 2; $i -lt $data.length - 1; $i++){
    $window1 = $data[$i - 2] + $data[$i - 1] + $data[$i]
    $window2 = $data[$i - 1] + $data[$i] + $data[$i + 1]
    if($window1 -lt $window2) {
        $increases++
        Write-Host "$($window2) (increased)"
    } else {
        Write-Host "$($window2) (decreased/no change)"
    }
}
$increases