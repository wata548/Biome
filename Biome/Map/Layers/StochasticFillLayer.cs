using System.Diagnostics.CodeAnalysis;

namespace Biome;

public class StochasticFillLayer<T>(T pTarget, T pAfter, float pProbability): MiddleLayer<T> where T : notnull {

    private T _target = pTarget;
    private T _affter = pAfter;
    private float _probability = pProbability;
    private Random _random = new();
    
    public override LayerOutput<T> Get(T[] pInput, Coord pSize, Coord pPos) {
        var result = (pInput.Clone() as T[])!;
        for (int x = 0; x < pSize.X; x++) {
            for (int y = 0; y < pSize.Y; y++) {
                if(!pInput[x + y * pSize.X]!.Equals(_target))
                    continue;
                var changeable = Coord.Directions.All(d => {
                    var cx = d.X + x;
                    var cy = d.Y + y;
                    if (cx < 0 || cx >= pSize.X || cy < 0 || cy >= pSize.Y)
                        return true;
                    return pInput[cx + cy * pSize.X]!.Equals(_target);
                });
                if(!changeable)
                    continue;
                var value = _random.NextSingle();
                if (value >= _probability)
                    result[x + y * pSize.X] = _affter;
            }   
        }

        return new(pSize, result);
    }
}