namespace MarsMission;

// Campo retangular. Canto inferior esquerdo fixo em (0,0); o superior direito
// é definido pela largura e altura informadas.
public sealed class LandingField
{
    public int Width { get; }
    public int Height { get; }

    public LandingField(int width, int height)
    {
        if (width < 0 || height < 0)
            throw new ArgumentException("O tamanho do campo não pode ser negativo.");

        Width = width;
        Height = height;
    }

    // Verdadeiro se a coordenada está dentro dos limites do campo.
    public bool Contains(Coordinate coordinate)
    {
        bool withinWidth = coordinate.X >= 0 && coordinate.X <= Width;
        bool withinHeight = coordinate.Y >= 0 && coordinate.Y <= Height;
        return withinWidth && withinHeight;
    }
}
