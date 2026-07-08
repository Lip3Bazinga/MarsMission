using System.Text;

namespace MarsMission;

// Saída no console. Cada veículo recebe uma cor da paleta, mantida entre o grid
// e o resultado final, facilitando associar cada seta ao respectivo rover.
public sealed class ConsoleOutput : IVisualOutput
{
    private const int CellWidth = 4;

    // Índices além do tamanho da paleta reutilizam as cores (módulo).
    private static readonly ConsoleColor[] Palette =
    {
        ConsoleColor.Green,
        ConsoleColor.Cyan,
        ConsoleColor.Yellow,
        ConsoleColor.Magenta,
        ConsoleColor.Red,
        ConsoleColor.Blue
    };

    public void ShowHeader()
    {
        Console.OutputEncoding = Encoding.UTF8; // garante acentos e setas corretos
        Console.WriteLine();
        WriteLine("  ╔═══════════════════════════════════════════════╗", ConsoleColor.DarkCyan);
        WriteLine("  ║                                               ║", ConsoleColor.DarkCyan);
        WriteColored("  ║", ConsoleColor.DarkCyan);
        WriteColored("                 MISSÃO MARTE                  ", ConsoleColor.Red);
        WriteLine("║", ConsoleColor.DarkCyan);
        WriteColored("  ║", ConsoleColor.DarkCyan);
        WriteColored("     Controle de veículos na superfície        ", ConsoleColor.White);
        WriteLine("║", ConsoleColor.DarkCyan);
        WriteLine("  ║                                               ║", ConsoleColor.DarkCyan);
        WriteLine("  ╚═══════════════════════════════════════════════╝", ConsoleColor.DarkCyan);
        Console.WriteLine();
    }

    public void ShowMessage(string message)
    {
        // Linha vazia passa direto (usada só para dar respiro entre veículos).
        if (message.Length == 0)
        {
            Console.WriteLine();
            return;
        }

        // Separadores de fase ("--- Campo ANTES/DEPOIS ---") ganham destaque.
        if (message.StartsWith("---"))
        {
            Console.WriteLine();
            WriteLine($"  {message}", ConsoleColor.DarkCyan);
            return;
        }

        // Demais mensagens (ex.: posição final) seguem a margem padrão.
        Console.WriteLine($"  {message}");
    }

    public void DrawField(LandingField field, IReadOnlyList<Vehicle> vehicles)
    {
        Console.WriteLine();

        // Y decresce no laço para que o Norte apareça no topo.
        for (int y = field.Height; y >= 0; y--)
        {
            WriteColored($"  {y,2} │ ", ConsoleColor.DarkGray);
            for (int x = 0; x <= field.Width; x++)
                DrawCell(new Coordinate(x, y), vehicles);
            Console.WriteLine();
        }

        DrawXAxis(field.Width);
        Console.WriteLine();
        DrawLegend();
        Console.WriteLine();
    }

    public void ShowFinalResult(IReadOnlyList<Vehicle> vehicles)
    {
        Console.WriteLine();
        WriteLine("  ═══════════════════════════════════════════════", ConsoleColor.DarkCyan);
        WriteLine("                  RESULTADO FINAL", ConsoleColor.White);
        WriteLine("  ═══════════════════════════════════════════════", ConsoleColor.DarkCyan);
        Console.WriteLine();

        for (int vehicleIndex = 0; vehicleIndex < vehicles.Count; vehicleIndex++)
        {
            WriteColored($"    Veículo {vehicleIndex + 1}", ConsoleColor.Gray);
            WriteColored("  →  ", ConsoleColor.DarkGray);
            WriteLine(vehicles[vehicleIndex].Position.ToString(), ColorOf(vehicleIndex));
        }

        Console.WriteLine();
    }

    // Desenha a linha de base e os rótulos do eixo X.
    private void DrawXAxis(int width)
    {
        var baseline = "     └" + new string('─', (width + 1) * CellWidth);
        WriteLine(baseline, ConsoleColor.DarkGray);

        WriteColored("       ", ConsoleColor.DarkGray);
        for (int x = 0; x <= width; x++)
            WriteColored(x.ToString().PadRight(CellWidth), ConsoleColor.DarkGray);
        Console.WriteLine();
    }

    private static void DrawCell(Coordinate coordinate, IReadOnlyList<Vehicle> vehicles)
    {
        for (int vehicleIndex = 0; vehicleIndex < vehicles.Count; vehicleIndex++)
        {
            if (vehicles[vehicleIndex].Position.Coordinate == coordinate)
            {
                var arrow = vehicles[vehicleIndex].Position.Orientation.Symbol.ToString();
                WriteColored(arrow.PadRight(CellWidth), ColorOf(vehicleIndex));
                return;
            }
        }
        WriteColored("·".PadRight(CellWidth), ConsoleColor.DarkGray);
    }

    private static readonly (string Symbol, string Label)[] LegendItems =
    {
        ("^", "Norte"), ("v", "Sul"), (">", "Leste"), ("<", "Oeste"), ("·", "vazio")
    };

    private static void DrawLegend()
    {
        WriteColored("     Legenda:  ", ConsoleColor.Gray);
        foreach (var (symbol, label) in LegendItems)
        {
            WriteColored($"{symbol} ", ConsoleColor.White);
            WriteColored($"{label}   ", ConsoleColor.DarkGray);
        }
        Console.WriteLine();
    }

    private static ConsoleColor ColorOf(int vehicleIndex) => Palette[vehicleIndex % Palette.Length];

    // Escreve com a cor informada e restaura a anterior para não afetar as
    // escritas seguintes.
    private static void WriteColored(string text, ConsoleColor color)
    {
        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ForegroundColor = previousColor;
    }

    private static void WriteLine(string text, ConsoleColor color)
    {
        WriteColored(text, color);
        Console.WriteLine();
    }
}
