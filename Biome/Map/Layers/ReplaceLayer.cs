using Biome.Extendsions;

namespace Biome;

public class ReplaceLayer<T>: MiddleLayer<T> where T : notnull {
    //==================================================||Fields 
    private List<RatioValue<T>> pRatio;
    private T _target;
    private Random pRandom = new();

    //==================================================||Constructors
    public ReplaceLayer(T pTarget, RatioValue<T>[] pRatio) {
        var fixedSum = 0;
        _target = pTarget;
        this.pRatio = pRatio.Select(r => r with { Amount = fixedSum += r.Amount })
            .ToList();
    }

    //==================================================||Methods 
    public override LayerOutput<T> Get(LayerOutput<T> pArgs, Coord pPos, int pSeed) {
        var size = pArgs.Size;
        var input = pArgs.Output;
        var seed = pSeed + pArgs.Depth;
        for(int x = 0; x < size.X; x++) {
            for (int y = 0; y < size.Y; y++) {
                if(!input[x + size.X * y].Equals(_target))
                    continue;
                var value = Random(seed,
                    pArgs.StartPos.X + x * pArgs.PixelPerSize.X,
                    pArgs.StartPos.Y + y * pArgs.PixelPerSize.Y,
                    pRatio[^1].Amount
                ) + 1;
                input[x + size.X * y] = pRatio.UpperBound(value, r => r.Amount).Data;   
            }
        }
        return pArgs with{Depth = pArgs.Depth + 1};
        
        int Random(int pSeed, int pX, int pY, int pMaxValue) {
            var v = pSeed;
            v ^= pX * 0x4A8CF829;
            v ^= pY * 0x19CD367A;
            v ^= v >> 10;
            v *= 0x3CC2CCAB;
            v ^= 0x4A8CF828;
            v += 0x3A8CF8A9;
            v %= pMaxValue;
            return v > 0 ? v : -v;
        }
    }
    
}