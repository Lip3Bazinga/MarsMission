namespace MarsMission;

// Leitura via teclado. Cada campo é relido enquanto a entrada for inválida.
public sealed class ConsoleInput : IUserInput
{
    public LandingField ReadField()
    {
        WriteTitle("Tamanho do campo de pouso");
        WriteHint("Informe o canto superior direito. O inferior esquerdo é sempre 0 0.");
        Console.WriteLine();

        int width = ReadNonNegativeInt("  Largura (X máximo): ");
        int height = ReadNonNegativeInt("  Altura  (Y máximo): ");
        Console.WriteLine();

        return new LandingField(width, height);
    }

    public Position ReadStartingPosition(LandingField field, int vehicleNumber)
    {
        WriteTitle($"Posição inicial do veículo {vehicleNumber}");
        WriteHint("Formato: X Y Orientação   (ex.: 1 2 N)");
        WriteHint("Orientações: N (Norte)  L (Leste)  S (Sul)  O (Oeste)");
        Console.WriteLine();

        while (true)
        {
            Console.Write("  Posição: ");
            var parts = ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 3)
            {
                Warn("Informe exatamente: X Y Orientação.");
                continue;
            }

            if (!int.TryParse(parts[0], out int x) || !int.TryParse(parts[1], out int y))
            {
                Warn("X e Y devem ser números inteiros.");
                continue;
            }

            if (parts[2].Length != 1 || InputParser.ParseOrientation(parts[2][0]) is not { } orientation)
            {
                Warn("Orientação inválida. Use N, L, S ou O.");
                continue;
            }

            var position = new Position(new Coordinate(x, y), orientation);
            if (!field.Contains(position.Coordinate))
            {
                Warn($"Posição fora do campo (0..{field.Width}, 0..{field.Height}).");
                continue;
            }

            Console.WriteLine();
            return position;
        }
    }

    public IReadOnlyList<Command> ReadCommands(int vehicleNumber)
    {
        WriteTitle($"Comandos do veículo {vehicleNumber}");
        WriteHint("E = girar esquerda   D = girar direita   A = avançar");
        Console.WriteLine();

        while (true)
        {
            Console.Write("  Comandos: ");
            var input = ReadLine().Trim();

            if (input.Length == 0)
            {
                Warn("Informe ao menos um comando.");
                continue;
            }

            if (InputParser.ParseCommands(input) is { } commands)
            {
                Console.WriteLine();
                return commands;
            }

            Warn("Use apenas as letras E, D e A.");
        }
    }

    public bool WantsToAddVehicle()
    {
        Console.Write("  Adicionar outro veículo? (S/N): ");
        var answer = ReadLine().Trim().ToUpperInvariant();
        Console.WriteLine();
        return answer == "S";
    }

    private static int ReadNonNegativeInt(string label)
    {
        while (true)
        {
            Console.Write(label);
            if (int.TryParse(ReadLine(), out int value) && value >= 0)
                return value;
            Warn("Digite um número inteiro maior ou igual a zero.");
        }
    }

    // Normaliza o fim de entrada (ReadLine null) para string vazia.
    private static string ReadLine() => Console.ReadLine() ?? "";

    // Título de seção (ciano), ajuda (cinza) e erro (amarelo).
    private static void WriteTitle(string text) => WriteLine($"  ── {text} ──", ConsoleColor.Cyan);
    private static void WriteHint(string text) => WriteLine($"  {text}", ConsoleColor.DarkGray);
    private static void Warn(string message) => WriteLine($"  ⚠ {message} Tente novamente.", ConsoleColor.Yellow);

    // Escreve uma linha na cor informada, restaurando a cor anterior.
    private static void WriteLine(string text, ConsoleColor color)
    {
        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = previousColor;
    }
}
