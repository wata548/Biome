namespace Biome;

public class ZoomLayer<T>(bool pSpread = false): MiddleLayer<T> where T : notnull {
    private bool _spread = pSpread;
    
    public override LayerOutput<T> Get(LayerOutput<T> pArgs, Coord pPos, int pSeed) {
        
        var checkRange = pArgs.Size / 2;
        var size = pArgs.Size;
        var input = pArgs.Output;
        var result = new T[4 * checkRange.X * checkRange.Y];
        var seed = pSeed + pArgs.Depth;
        var startDelta = new Coord(
            pArgs.PixelPerSize.X * checkRange.X + pArgs.StartPos.X <= pPos.X ? checkRange.X : 0,
            pArgs.PixelPerSize.Y * checkRange.Y + pArgs.StartPos.Y <= pPos.Y ? checkRange.Y : 0
        );
        
        for (int x = 0; x < checkRange.X; x++) {
            for (int y = 0; y < checkRange.Y; y++) {
                var originIdx = (x + startDelta.X) + size.X * (y + startDelta.Y);
                var idx = 2 * x + 4 * checkRange.X * y;
                
                for (int dx = 0; dx < 2; dx++) {
                    for (int dy = 0; dy < 2; dy++) {
                        var value = input[originIdx];
                        if (!_spread) {
                            result[idx + dx + 2 * dy * checkRange.X] = value;
                            continue;
                        }
                        
                        var outX = dx >= 1;
                        var outY = dy >= 1;
                        
                        if (x == checkRange.X - 1 && outX)
                            outX = false;
                        if (y == checkRange.Y - 1 && outY)
                            outY = false;
                        
                        if (outX && outY) {
                            var r = Random(seed,
                                (dx + x) * pArgs.PixelPerSize.X + pArgs.StartPos.X,
                                (dy + y) * pArgs.PixelPerSize.Y + pArgs.StartPos.Y,
                                4
                            );
                            var delta = 0;
                            if ((r & 1) != 0) delta++;
                            if ((r & 2) != 0) delta+= size.X;
                            value = input[originIdx + delta];
                        }
                        else {
                            var delta = 0;
                            if (outX) {
                                delta++;
                            }
                            if (outY) {
                                delta = size.X;
                            }

                            var r = Random(seed,
                                (dx + x) * pArgs.PixelPerSize.X + pArgs.StartPos.X,
                                (dy + y) * pArgs.PixelPerSize.Y + pArgs.StartPos.Y,
                                2
                            );
                            value = input[originIdx + (r == 0 ? delta : 0)];
                        }
                        result[idx + dx + 2 * dy * checkRange.X] = value;
                    }
                }
            }
        }

        return new(
            pArgs.StartPos + startDelta * pArgs.PixelPerSize,
            pArgs.PixelPerSize / 2,
            size,
            result,
            pArgs.Depth + 1
        );
        int Random(int pSeed, int pX, int pY, int pMaxValue) {
            var v = pSeed;
            v ^= pX * 0x4A7CE958;
            v ^= pY * 0x2A673C11;
            v ^= v >> 12;
            v *= 0x1763CEA7;
            v %= pMaxValue;
            return v > 0 ? v : -v;
        }
    }
}