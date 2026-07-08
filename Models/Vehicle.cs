namespace MarsMission;

// Rover: mantém a posição atual e executa comandos respeitando os limites do campo.
public sealed class Vehicle
{
    private readonly LandingField _field;

    public Position Position { get; private set; }

    public Vehicle(Position startPosition, LandingField field)
    {
        if (!field.Contains(startPosition.Coordinate))
            throw new ArgumentException("A posição inicial está fora do campo.");

        _field = field;
        Position = startPosition;
    }

    public void Execute(IEnumerable<Command> commands)
    {
        foreach (var command in commands)
            Execute(command);
    }

    public void Execute(Command command)
    {
        Position = command switch
        {
            Command.TurnLeft  => Position.TurnLeft(),
            Command.TurnRight => Position.TurnRight(),
            Command.Move      => Move(),
            _ => Position
        };
    }

    private Position Move()
    {
        var nextPosition = Position.MoveForward();

        // Movimento que sairia do campo é ignorado; o veículo permanece parado.
        return _field.Contains(nextPosition.Coordinate) ? nextPosition : Position;
    }
}
