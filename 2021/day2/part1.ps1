$data = Get-Content -Path "$PSScriptRoot\data.txt"

$h_pos = 0
$d_pos = 0

foreach($d in $data){
    $step = $d.split(" ")
    if($step[0] -eq "forward"){
        $h_pos += $step[1]
    } elseif($step[0] -eq "up"){
        $d_pos -= $step[1]
    } else {
        $d_pos += $step[1]
    }
}

Write-Host "Position: $($h_pos * $d_pos)"
