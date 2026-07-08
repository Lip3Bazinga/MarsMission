namespace MarsMission;

// Ponto (X, Y) no grid. record struct para ter igualdade por valor.
public readonly record struct Coordinate(int X, int Y)
{
    public Coordinate Offset(int moveX, int moveY) => new(X + moveX, Y + moveY);
}
