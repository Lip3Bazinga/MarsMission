namespace MarsMission;

// Direção da bússola. Cada direção é uma subclasse que conhece seu próprio giro
// e vetor de avanço, evitando switch por direção espalhado no Vehicle.
public abstract class Orientation
{
    public static readonly Orientation North = new NorthOrientation();
    public static readonly Orientation East  = new EastOrientation();
    public static readonly Orientation South = new SouthOrientation();
    public static readonly Orientation West  = new WestOrientation();

    // Todas as direções conhecidas. Fonte única para busca por Code (ex.: parsing).
    public static readonly IReadOnlyList<Orientation> All = new[] { North, East, South, West };

    // Code: letra usada na entrada/saída. Symbol: seta desenhada no grid.
    public abstract char Code { get; }
    public abstract char Symbol { get; }

    // Quanto o veículo anda em cada eixo ao avançar uma casa nesta direção.
    public abstract int MoveX { get; }
    public abstract int MoveY { get; }

    public abstract Orientation TurnLeft();
    public abstract Orientation TurnRight();

    public override string ToString() => Code.ToString();

    private sealed class NorthOrientation : Orientation
    {
        public override char Code => 'N';
        public override char Symbol => '^';
        public override int MoveX => 0;
        public override int MoveY => 1;
        public override Orientation TurnLeft() => West;
        public override Orientation TurnRight() => East;
    }

    private sealed class EastOrientation : Orientation
    {
        public override char Code => 'L';
        public override char Symbol => '>';
        public override int MoveX => 1;
        public override int MoveY => 0;
        public override Orientation TurnLeft() => North;
        public override Orientation TurnRight() => South;
    }

    private sealed class SouthOrientation : Orientation
    {
        public override char Code => 'S';
        public override char Symbol => 'v';
        public override int MoveX => 0;
        public override int MoveY => -1;
        public override Orientation TurnLeft() => East;
        public override Orientation TurnRight() => West;
    }

    private sealed class WestOrientation : Orientation
    {
        public override char Code => 'O';
        public override char Symbol => '<';
        public override int MoveX => -1;
        public override int MoveY => 0;
        public override Orientation TurnLeft() => South;
        public override Orientation TurnRight() => North;
    }
}
