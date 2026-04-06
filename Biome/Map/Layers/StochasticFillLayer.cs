using System.Diagnostics.CodeAnalysis;

namespace Biome;

public class StochasticFillLayer<T>(T pTarget, T pAfter, float pProbability): MiddleLayer<T> where T : notnull {

    private T _target = pTarget;
    private T _affter = pAfter;
    private float _probability = pProbability;
    
    public override LayerOutput<T> Get(LayerOutput<T> pArgs, Coord pPos, int pSeed) {
        var input = pArgs.Output;
        var size = pArgs.Size;
        var result = (input.Clone() as T[])!;
        var seed = pSeed + pArgs.Depth;
        for (int x = 0; x < size.X; x++) {
            for (int y = 0; y < size.Y; y++) {
                if(!input[x + y * size.X]!.Equals(_target))
                    continue;
                var changeable = Coord.Directions.All(d => {
                    var cx = d.X + x;
                    var cy = d.Y + y;
                    if (cx < 0 || cx >= size.X || cy < 0 || cy >= size.Y)
                        return true;
                    return input[cx + cy * size.X]!.Equals(_target);
                });
                if(!changeable)
                    continue;
                var value = Random(seed,
                    pArgs.StartPos.X + x * pArgs.PixelPerSize.X,
                    pArgs.StartPos.Y + y * pArgs.PixelPerSize.Y,
                    1000) / 1000f;
                if (value >= _probability)
                    result[x + y * size.X] = _affter;
            }   
        }

        return pArgs with{Output = result, Depth = pArgs.Depth + 1};
        
        int Random(int pSeed, int pX, int pY, int pMaxValue) {
            var v = pSeed;
            v ^= pX * 0x4A7CE958;
            v ^= pY * 0x598D664C;
            v ^= v >> 5;
            v *= 0x32525224;
            v %= pMaxValue;
            return v > 0 ? v : -v;
        }
    }
}