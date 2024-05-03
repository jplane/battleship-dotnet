
namespace CommonLib;

internal abstract class Ship
{
    private readonly int _size;
    private int _hits = 0;

    protected Ship(int size)
    {
        _size = size;
    }

    public bool Hit()
    {
        _hits++;
        return this.Sunk;
    }

    public bool Sunk => _hits >= _size;
}

internal class AircraftCarrier : Ship
{
    public AircraftCarrier() : base(5)
    {
    }
}

internal class Battleship : Ship
{
    public Battleship() : base(4)
    {
    }
}

internal class Sub : Ship
{
    public Sub() : base(3)
    {
    }
}

internal class Destroyer : Ship
{
    public Destroyer() : base(2)
    {
    }
}

internal class Cruiser : Ship
{
    public Cruiser() : base(3)
    {
    }
}