using System.Drawing;
using ColorMine.ColorSpaces;

namespace Biome;

public class PerlinTest(int pRange) {
    private readonly int _range = pRange;
    
    public LayerOutput<Hsb> Get(LayerOutput<TileType> pArgs, Coord pPos, int pSeed) {
        var size = pArgs.Size;
        
        var result = new Hsb[size.X * size.Y];
        var perlin = new PerLinNoise(pSeed);
        var input = pArgs.Output;
        for (int x = 0; x < size.X; x++) {
            for (int y = 0; y < size.Y; y++) {
                var min = 0;
                var max = 0;
                var px = _range * x / (float)size.X;
                var py = _range * y / (float)size.Y;
                var randomValue = (perlin.Get(
                    pArgs.StartPos.X + x * pArgs.PixelPerSize.X,
                    pArgs.StartPos.Y + y * pArgs.PixelPerSize.Y,
                    3
                ) + 1) / 2f;

                var cnt = 0;
                for (int dx = -1; dx <= 1; dx++) {
                    var cx = dx + x;
                    if(cx < 0 || cx >= size.X)
                        continue;
                    for (int dy = -1; dy <= 1; dy++) {
                        var cy = dy + y;
                        if(cy < 0 || cy >= size.Y)
                            continue;
                        cnt++;
                        var biome =  input[cx + size.X * cy];
                        min += biome.ToMin();
                        max += biome.ToMax();
                    }
                }

                min /= cnt;
                max /= cnt;
                
                var value = (min + (max - min) * randomValue) / 75f;
                result[x + size.X * y] = new Hsb() {
                    H = 360 - value * 360,
                    B = 1,
                    S = 1
                };
            }   
        }

        return new(pArgs.StartPos, pArgs.PixelPerSize, pArgs.Size, result, pArgs.Depth + 1);
    }
}