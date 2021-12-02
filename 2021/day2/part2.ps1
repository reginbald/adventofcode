$data = Get-Content -Path "$PSScriptRoot\data.txt"

$h_pos = 0
$d_pos = 0
$aim = 0

foreach($d in $data){
    $step = $d.split(" ")
    if($step[0] -eq "forward"){
        $h_pos += $step[1]
        $d_pos += $aim * $step[1]
    } elseif($step[0] -eq "up"){
        $aim -= $step[1]
    } else {
        $aim += $step[1]
    }
}

Write-Host "Position: $($h_pos * $d_pos)"
