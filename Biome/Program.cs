using System.Diagnostics;
using System.Drawing;
using Biome;

public class Program {
    public static void Main(string[] args) {
        var pixelPerSize = Coord.One * 2048;
        var size = Coord.One * 16;
        var range = pixelPerSize * size;
        
        var input = new LayerOutput<TileType>(Coord.Zero, pixelPerSize, size, null);
        var targetPos = new Coord(
            (new Random()).Next(range.X),
            (new Random()).Next(range.Y)
        );
        const int seed = 77;
        var result = new MapGenerator(true).Get(input, targetPos, seed);
        
        var colorMap = new PerlinTest(10).Get(result, targetPos, 12354);
        colorMap.ToImage("DeepthMap.png", hsb => {
            var rgb = hsb.ToRgb();
            int r = (int)rgb.R;
            int g = (int)rgb.G;
            int b = (int)rgb.B;
            return Color.FromArgb(r, g, b);
        });
        Console.WriteLine(result.PixelPerSize);
        Console.WriteLine($"{result.StartPos} + {result.Size}: {targetPos}");
    }
}