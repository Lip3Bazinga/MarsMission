namespace MarsMission;

// Converte o texto digitado (letras de orientação e de comando) para os tipos
// do domínio. Centraliza aqui o mapeamento das letras em português.
public static class InputParser
{
    // Orientação: N=Norte, L=Leste, S=Sul, O=Oeste.
    // Busca por Code para não duplicar a tabela já definida em cada Orientation.
    public static Orientation? ParseOrientation(char code)
    {
        char upperCode = char.ToUpperInvariant(code);
        foreach (var orientation in Orientation.All)
        {
            if (orientation.Code == upperCode)
                return orientation;
        }
        return null;
    }

    // Comando: E=esquerda, D=direita, A=avança.
    public static Command? ParseCommand(char letter) => char.ToUpperInvariant(letter) switch
    {
        'E' => Command.TurnLeft,
        'D' => Command.TurnRight,
        'A' => Command.Move,
        _   => null
    };

    // Converte a sequência inteira. Retorna null se qualquer letra for inválida,
    // deixando o chamador decidir o que fazer (ex.: pedir a entrada novamente).
    public static IReadOnlyList<Command>? ParseCommands(string input)
    {
        var commands = new List<Command>(input.Length);
        foreach (var letter in input)
        {
            if (ParseCommand(letter) is not { } command)
                return null;
            commands.Add(command);
        }
        return commands;
    }
}
