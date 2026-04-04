public class Coord(int pX, int pY) {
    public static readonly Coord Left = new(-1, 0);
    public static readonly Coord Right = new(1, 0);
    public static readonly Coord Up = new(0, 1);
    public static readonly Coord Down = new(0, -1);
    public static readonly IReadOnlyCollection<Coord> Directions = [Coord.Up, Coord.Down, Coord.Left, Coord.Right];
    
    public int X = pX;
    public int Y = pY;

    #region Operators
    
    public static Coord operator *(Coord lhs, int rhs) =>
        new(lhs.X * rhs, lhs.Y * rhs);
    public static Coord operator *(Coord lhs, Coord rhs) =>
        new(lhs.X * rhs.X, lhs.Y * rhs.Y);
    public static Coord operator /(Coord lhs, int rhs) =>
        new(lhs.X / rhs, lhs.Y / rhs);
    public static Coord operator /(Coord lhs, Coord rhs) =>
        new(lhs.X / rhs.X, lhs.Y / rhs.Y);
    public static Coord operator %(Coord lhs, int rhs) =>
        new(lhs.X % rhs, lhs.Y / rhs);
    public static Coord operator %(Coord lhs, Coord rhs) =>
        new(lhs.X % rhs.X, lhs.Y % rhs.Y);
    public static Coord operator +(Coord lhs, int rhs) =>
        new(lhs.X + rhs, lhs.Y + rhs);
    public static Coord operator +(Coord lhs, Coord rhs) =>
        new(lhs.X + rhs.X, lhs.Y + rhs.Y);
    public static Coord operator -(Coord lhs, int rhs) =>
        new(lhs.X - rhs, lhs.Y - rhs);
    public static Coord operator -(Coord lhs, Coord rhs) =>
        new(lhs.X - rhs.X, lhs.Y - rhs.Y);
    
    #endregion
}