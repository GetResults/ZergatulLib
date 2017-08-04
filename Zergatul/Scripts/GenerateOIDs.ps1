param([String]$WorkingDir)

Add-Type -AssemblyName 'System.Xml.Linq'

function ProcessOIDElement($XElement, $SB, $Depth, $TotalValue)
{
    $subClasses = @()
    $subMembers = @()
    $firstSubClass = $true

    $spaces = ' ' * $Depth

    foreach ($e in $XElement.Elements('oid'))
    {
        if ($e.Attribute('class') -eq $null)
        {
            continue;
        }

        $value = $e.Attribute('value').Value
        $id = $e.Attribute('id').Value
        $class = $e.Attribute('class').Value
        $desc = $e.Attribute('desc').Value
        $short = $null
        if ($e.Attribute('short') -ne $null)
        {
            $short = $e.Attribute('short').Value
        }

        if ($e.HasElements)
        {
            if (-not $firstSubClass)
            {
                [void]$SB.AppendLine()
            }

            $newval = ($TotalValue + '.' + $value).TrimStart('.')

            [void]$SB.AppendLine($spaces + '/// <summary>');
            [void]$SB.AppendLine($spaces + '/// ' + $newval)
            [void]$SB.AppendLine($spaces + '/// </summary>');
            [void]$SB.AppendLine($spaces + 'public static class ' + $class);
            [void]$SB.AppendLine($spaces + '{')
            
            ProcessOIDElement -XElement $e -SB $SB -Depth ($Depth + 4) -TotalValue $newval

            [void]$SB.AppendLine($spaces + '}')

            $subClasses += $class

            if ($firstSubClass)
            {
                $firstSubClass = $false
            }
        }
        else
        {
            $ending = ''
            if ($short -ne $null)
            {
                $ending = ', "' + $short + '"'
            }
            [void]$SB.AppendFormat($spaces + 'public static OID {0} = new OID("{1}", "{2}"{3});', $class, $TotalValue + '.' + $value, $id, $ending)
            [void]$SB.AppendLine()

            $subMembers += $class
        }
    }

    [void]$SB.AppendLine()

    if (($subClasses.Length -gt 0) -and ($subMembers.Length -gt 0))
    {
        Write-Host $XElement
        throw "Not implemented"
    }

    [void]$SB.Append($spaces + 'public static IEnumerable<OID> All => ')

    if ($subClasses.Length -gt 0)
    {
        $a = $subClasses | ForEach-Object { $_ + '.All' }
        [void]$SB.Append('(' + [System.String]::Join(').Concat(', $a) + ');');
    }

    if ($subMembers.Length -gt 0)
    {
        $a = $subMembers | ForEach-Object { $spaces + '    ' + $_ + ',' }
        [void]$SB.AppendLine('new OID[]')
        [void]$SB.AppendLine($spaces + '{')
        foreach ($m in $a)
        {
            [void]$SB.AppendLine($m)
        }
        [void]$SB.AppendLine($spaces + '};')
    }

    [void]$SB.AppendLine()
    
}

$xdoc = [System.Xml.Linq.XDocument]::Load((Join-Path $WorkingDir 'Network/OIDs.xml'))

$sb = New-Object System.Text.StringBuilder

[void]$sb.AppendLine('// ********************************************************************')
[void]$sb.AppendLine('// This is autogenerated file. Change OIDs.xml/GenerateOIDs.ps1 instead')
[void]$sb.AppendLine('// ********************************************************************')
[void]$sb.AppendLine()
[void]$sb.AppendLine('using System.Collections.Generic;')
[void]$sb.AppendLine('using System.Linq;')
[void]$sb.AppendLine()
[void]$sb.AppendLine('namespace Zergatul.Network')
[void]$sb.AppendLine('{')
[void]$sb.AppendLine('    public partial class OID')
[void]$sb.AppendLine('    {')

ProcessOIDElement -XElement $xdoc.Root -SB $sb -Depth 8 -TotalValue ''

[void]$sb.AppendLine('    }')
[void]$sb.Append('}')

Set-Content -Path (Join-Path $WorkingDir 'Network/OIDs.cs') -Value $sb.ToString() -Force