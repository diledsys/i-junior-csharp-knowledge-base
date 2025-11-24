param(
  [string]$Path = ".",
  [switch]$IncludeReturn,     # добавлять пустую строку и перед 'return'
  [switch]$WhatIf,            # только показать изменения (ничего не пишет)
  [switch]$NoBackup,          # не создавать .bak
  [string[]]$Extensions = @("*.cs"),
  [switch]$Recurse = $true,
  [string[]]$ExcludeDirs = @("bin","obj",".git",".vs"),
  [switch]$NoFinalNewline,    # убрать финальный перевод строки
  [switch]$NoLog              # ОТКЛЮЧИТЬ логи (по умолчанию они включены)
)

# Лог по умолчанию включён; -NoLog выключает
$script:LogEnabled = -not $NoLog.IsPresent
function Write-LogMsg([string]$msg, [string]$color = 'Gray') {
  if ($script:LogEnabled) { Write-Host $msg -ForegroundColor $color }
}

Write-Host ('Fix-BlankLines: path={0}  recurse={1}  includeReturn={2}  whatIf={3}  noFinalNL={4}  log={5}' -f $Path,$Recurse,$IncludeReturn,$WhatIf,$NoFinalNewline,$script:LogEnabled) -ForegroundColor Cyan

$keywords = @('for','foreach','if','while','switch','try','catch','finally')
if ($IncludeReturn) { $keywords += 'return' }

function NeedsBlankBefore([string]$line) {
  $t = $line.TrimStart()
  foreach ($kw in $keywords) {
    if ($t.StartsWith($kw + ' ') -or $t.StartsWith($kw + '(')) { return $true }
  }
  return $false
}

function IsElseLike([string]$line) {
  $t = $line.TrimStart()
  if ($t.StartsWith('else') -or $t.StartsWith('catch') -or $t.StartsWith('finally') -or $t.StartsWith('while ')) { return $true }
  return $false
}

function NextNonEmptyIndex($arr, [int]$from) {
  for ($j = $from; $j -lt $arr.Count; $j++) {
    if ($arr[$j].Trim() -ne '') { return $j }
  }
  return -1
}

function Fix-OneFile([string]$file) {
  $raw = Get-Content -LiteralPath $file -Raw
  if ($null -eq $raw) { return }

  $fileName = [System.IO.Path]::GetFileName($file)
  $NL = "`n"; if ($raw -match "`r`n") { $NL = "`r`n" }

  # читаем построчно (без переводов строк)
  $lines = [System.IO.File]::ReadAllLines($file)
  Write-LogMsg ("[$fileName] --- start ---") 'White'

  # ----- PASS 1: чистка и вставки до/после блоков -----------------------
  $stage1 = New-Object System.Collections.Generic.List[string]
  for ($i = 0; $i -lt $lines.Count; $i++) {
    $line = $lines[$i]; $trim = $line.Trim()

    # ❌ пустая строка перед '}'
    if ($trim -eq '' -and $i -lt ($lines.Count - 1) -and $lines[$i+1].Trim() -eq '}') {
      Write-LogMsg ("[$fileName] remove blank BEFORE '}' at line $([int]($i+1))") 'Yellow'
      continue
    }

    # ❌ пустая строка сразу после '{'
    if ($trim -eq '' -and $stage1.Count -gt 0 -and $stage1[$stage1.Count-1].Trim().EndsWith('{')) {
      Write-LogMsg ("[$fileName] remove blank AFTER '{' at line $([int]($i+1))") 'Yellow'
      continue
    }

    # если текущая '}', уберём уже добавленную пустую перед ней
    if ($trim -eq '}' -and $stage1.Count -gt 0 -and $stage1[$stage1.Count-1].Trim() -eq '') {
      $stage1.RemoveAt($stage1.Count-1)
      Write-LogMsg ("[$fileName] drop blank directly before '}' (before line $([int]($i+1)))") 'Yellow'
    }

    # ✅ пустая строка ПЕРЕД управляющим блоком
    if (NeedsBlankBefore $line) {
      $prev = ''; if ($stage1.Count -gt 0) { $prev = $stage1[$stage1.Count - 1] }
      if ($prev.Trim() -ne '' -and $prev.Trim() -ne '{') {
        $stage1.Add('')
        Write-LogMsg ("[$fileName] add blank BEFORE control on line $([int]($i+1))") 'Green'
      }
    }

    # добавляем строку без хвостовых пробелов
    $trimmed = ($line -replace '[ \t]+$','')
    if ($trimmed -ne $line) { Write-LogMsg ("[$fileName] trim trailing ws at line $([int]($i+1))") 'DarkCyan' }
    $stage1.Add($trimmed)
  }

  # ----- PASS 2: ровно одна пустая ПОСЛЕ '}' если далее инструкция -------
  $stage2 = New-Object System.Collections.Generic.List[string]
  for ($i = 0; $i -lt $stage1.Count; $i++) {
    $curr = $stage1[$i]
    $stage2.Add($curr)

    if ($curr.Trim() -eq '}') {
      $j = NextNonEmptyIndex $stage1 ($i+1)
      if ($j -gt -1) {
        $nextLine = $stage1[$j]; $nt = $nextLine.Trim()
        if ($nt -ne '}' -and -not (IsElseLike $nextLine)) {
          if ($i -lt ($stage1.Count - 1) -and $stage1[$i+1].Trim() -ne '') {
            $stage2.Add('')
            Write-LogMsg ("[$fileName] add blank AFTER '}' between lines $([int]($i+1)) and $([int]($i+2))") 'Green'
          }
        }
      }
    }
  }

  # ----- Collapse: 3+ пустых -> 1 ---------------------------------------
  $collapsed = New-Object System.Collections.Generic.List[string]
  $empty = 0
  for ($i = 0; $i -lt $stage2.Count; $i++) {
    $l = $stage2[$i]
    if ($l.Trim() -eq '') {
      $empty++
      if ($empty -eq 2) { $start = $i }
      if ($empty -le 1) { $collapsed.Add('') }
    } else {
      if ($empty -ge 2) { Write-LogMsg ("[$fileName] collapse blanks $([int]($start))-$([int]($i))") 'DarkYellow' }
      $empty = 0
      $collapsed.Add($l)
    }
  }
  if ($empty -ge 2) { Write-LogMsg ("[$fileName] collapse trailing blanks from $([int]($start))") 'DarkYellow' }

  # ----- убрать пустые строки в самом конце ------------------------------
  $removedTail = 0
  while ($collapsed.Count -gt 0 -and $collapsed[$collapsed.Count-1].Trim() -eq '') {
    $collapsed.RemoveAt($collapsed.Count-1); $removedTail++
  }
  if ($removedTail -gt 0) { Write-LogMsg ("[$fileName] remove $removedTail blank(s) at EOF") 'Yellow' }

  # ----- GUARD: никогда не терять '}' -----------------------------------
  $srcClose = ([regex]::Matches($raw, '\}')).Count
  $newClose = 0; foreach ($l in $collapsed) { $newClose += ([regex]::Matches($l, '\}')).Count }
  if ($newClose -lt $srcClose) {
    $missing = $srcClose - $newClose
    for ($m = 0; $m -lt $missing; $m++) { $collapsed.Add('}') }
    Write-LogMsg ("[$fileName] GUARD: restored missing '}' (+$missing)") 'Red'
  }

  # ----- собрать результат ----------------------------------------------
  $new = [string]::Join($NL, $collapsed)
  if (-not $NoFinalNewline) { $new = $new + $NL }

  if ($new -ne $raw) {
    if ($WhatIf) {
      Write-LogMsg ("[$fileName] would change") 'Yellow'
      Write-Host ("would change: $file") -ForegroundColor Yellow
    } else {
      if (-not $NoBackup) { Copy-Item -LiteralPath $file -Destination ($file + '.bak') -Force }
      Set-Content -LiteralPath $file -Value $new -Encoding utf8
      Write-LogMsg ("[$fileName] changed") 'Green'
      Write-Host ("changed: $file") -ForegroundColor Green
    }
  } else {
    Write-LogMsg ("[$fileName] no changes") 'DarkGray'
  }
}

# собрать список файлов, исключая служебные каталоги
$files = @()
foreach ($ext in $Extensions) {
  $files += Get-ChildItem -LiteralPath $Path -Recurse:$Recurse -Filter $ext -File
}
$excludePattern = '([\\/])(' + (($ExcludeDirs -join '|')) + ')([\\/])'
$files = $files | Where-Object { $_.FullName -notmatch $excludePattern }

foreach ($f in $files) { Fix-OneFile $f.FullName }

Write-Host 'Done.' -ForegroundColor Cyan
