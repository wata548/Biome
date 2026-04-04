public class Coord(int pX, int pY) {
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