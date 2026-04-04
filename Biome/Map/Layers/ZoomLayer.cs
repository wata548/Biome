using System.Diagnostics;
using System.Net.Http.Headers;

namespace Biome;

public class ZoomLayer<T>(int pSize, int pScale, bool pSpread = false): MiddleLayer<T> where T : notnull {
    private int _partDenominator = pSize;
    private int _scale = pScale;
    private bool _spread = pSpread;
    private Random _random = new(); 
    
    public override LayerOutput<T> Get(T[] pInput, Coord pSize, Coord pPos) {
        var size = pSize / _partDenominator;
        var result = new T[size.X * size.Y * _scale * _scale];
        
        for (int x = 0; x < size.X; x++) {
            for (int y = 0; y < size.Y; y++) {
                var originIdx = x + pSize.X * y;
                var idx = (x + size.X * _scale * y) * _scale;
                
                for (int dx = 0; dx < _scale; dx++) {
                    for (int dy = 0; dy < _scale; dy++) {
                        var value = pInput[originIdx];
                        if (!_spread) {
                            result[idx + dx + dy * size.X * _scale] = value;
                            continue;
                        }
                        
                        var outX = dx >= _scale / 2;
                        var outY = dy >= _scale / 2;
                        
                        if (x == size.X - 1 && outX)
                            outX = false;
                        if (y == size.Y - 1 && outY)
                            outY = false;
                        
                        if (outX && outY) {
                            var r = _random.Next() % 4;
                            var delta = 0;
                            if ((r & 1) != 0) delta++;
                            if ((r & 2) != 0) delta+= pSize.X;
                            value = pInput[originIdx + delta];
                        }
                        else {
                            var delta = 0;
                            if (outX) {
                                delta++;
                            }
                            if (outY) {
                                delta = pSize.X;
                            }

                            value = pInput[originIdx + ((_random.Next() & 1) == 0 ? delta : 0)];
                        }
                        result[idx + dx + dy * size.X * _scale] = value;
                    }
                }
            }
        }

        return new(size * _scale, result);
    }
}