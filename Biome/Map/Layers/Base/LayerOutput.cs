using System.Drawing;

namespace Biome;

public record LayerOutput<T>(Coord StartPos, Coord PixelPerSize, Coord Size, T[] Output, int Depth = 0) {
    public void ToImage(string pPath, Func<T, Color> pTranslate) => Draw.Generate(pPath, Size, Output, pTranslate);
    public T GetLocal(int pX, int pY) => Output[pX + pY * Size.X];
    public T Get(int pX, int pY) => GetLocal((pX - StartPos.X) / PixelPerSize.X, (pY - StartPos.Y) / PixelPerSize.Y);
}