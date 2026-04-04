using Biome.Extendsions;

namespace Biome;

public class RandomLayer<T>: InputLayer<T> {
    
   //==================================================||Fields 
    private List<RatioValue<T>> _fixedTileRatio;
    
   //==================================================||Constructors 
    public RandomLayer(List<RatioValue<T>> pTileRatio) {
        var value = 0;
        _fixedTileRatio = pTileRatio
            .Select(row => row with {Amount = value += row.Amount})
            .ToList();
    }
    
   //==================================================||Methods 
    public override LayerOutput<T> Get(T[] pInput, Coord pSize, Coord pPos) {
        var length = pSize.X * pSize.Y;
        var datas = new T[length];
        var random = new Random();
        for (var i = 0; i < length; i++) {
            var value = random.Next() % _fixedTileRatio[^1].Amount + 1;
            datas[i] = _fixedTileRatio.UpperBound(value, row => row.Amount).Data;
        }

        return new(pSize, datas);
    }
}