namespace MarsMission;

// Coordenada + orientação do veículo. Imutável: girar e avançar retornam uma
// nova Position, o que evita efeitos colaterais e simplifica o raciocínio.
public sealed class Position
{
    public Coordinate Coordinate { get; }
    public Orientation Orientation { get; }

    public Position(Coordinate coordinate, Orientation orientation)
    {
        Coordinate = coordinate;
        Orientation = orientation;
    }

    public Position TurnLeft()
    {
        return new Position(Coordinate, Orientation.TurnLeft());
    }

    public Position TurnRight()
    {
        return new Position(Coordinate, Orientation.TurnRight());
    }

    public Position MoveForward()
    {
        var nextCoordinate = Coordinate.Offset(Orientation.MoveX, Orientation.MoveY);
        return new Position(nextCoordinate, Orientation);
    }

    // Formato de saída definido no enunciado: "1 3 N".
    public override string ToString()
    {
        return $"{Coordinate.X} {Coordinate.Y} {Orientation.Code}";
    }
}
