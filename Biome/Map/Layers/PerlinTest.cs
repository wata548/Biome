using System.Drawing;
using ColorMine.ColorSpaces;

namespace Biome;

public class PerlinTest(int pSeed, int pRange) {

    private readonly int _seed = pSeed;
    private readonly int _range = pRange;
    
    public LayerOutput<Hsb> Get(TileType[] pInput, Coord pSize, Coord pPos) {
        
        var result = new Hsb[pSize.X * pSize.Y];
        var perlin = new PerLinNoise(pSeed);
        for (int x = 0; x < pSize.X; x++) {
            for (int y = 0; y < pSize.Y; y++) {
                var min = 0;
                var max = 0;
                var px = _range * x / (float)pSize.X;
                var py = _range * y / (float)pSize.Y;
                var randomValue = (perlin.Get(px, py, 3) + 1) / 2f;

                var cnt = 0;
                for (int dx = -1; dx <= 1; dx++) {
                    var cx = dx + x;
                    if(cx < 0 || cx >= pSize.X)
                        continue;
                    for (int dy = -1; dy <= 1; dy++) {
                        var cy = dy + y;
                        if(cy < 0 || cy >= pSize.Y)
                            continue;
                        cnt++;
                        var biome =  pInput[cx + pSize.X * cy];
                        min += biome.ToMin();
                        max += biome.ToMax();
                    }
                }

                min /= cnt;
                max /= cnt;
                
                var value = (min + (max - min) * randomValue) / 75f;
                result[x + pSize.X * y] = new Hsb() {
                    H = 360 - value * 360,
                    B = 1,
                    S = 1
                };
            }   
        }

        return new(pSize, result);
    }
}