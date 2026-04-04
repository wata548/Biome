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
    public override LayerOutput<T> Get(T[] pInput, Coord pSize, Coord pPos) {
        for(int i = 0; i < pSize.X * pSize.Y; i++) {
            
            if(!pInput[i].Equals(_target))
                continue;
            var value = pRandom.Next(pRatio[^1].Amount) + 1;
            pInput[i] = pRatio.UpperBound(value, r => r.Amount).Data;
        }
        return new(pSize, pInput);
    }
}