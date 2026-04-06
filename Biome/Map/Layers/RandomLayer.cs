using Biome.Extendsions;

namespace Biome;

public class RandomLayer<T>: InputLayer<T> where T : notnull {
    
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
    public override LayerOutput<T> Get(LayerOutput<T> pArgs, Coord pPos, int pSeed) {
        var size = pArgs.Size;
        var length = size.X * size.Y;
        var datas = new T[length];

        var range = pArgs.PixelPerSize * pArgs.Size;
        var startPos = new Coord(
            (int)Math.Floor(pPos.X / (double)range.X) * range.X,
            (int)Math.Floor(pPos.Y / (double)range.Y) * range.Y
        );

        var seed = pSeed + pArgs.Depth;
        for (var x = 0; x < size.X; x++) {
            for (int y = 0; y < size.Y; y++) {
                var value = Random(
                    seed,
                    startPos.X + x * pArgs.PixelPerSize.X,
                    startPos.Y + y * pArgs.PixelPerSize.Y,
                    _fixedTileRatio[^1].Amount
                ) + 1;
                datas[x + y * size.X] = _fixedTileRatio.UpperBound(value, row => row.Amount).Data;
            }
        }

        return pArgs with { StartPos = startPos, Output = datas, Depth = pArgs.Depth };
    }
    int Random(int pSeed, int pX, int pY, int pMaxValue) {
        var v = pSeed;
        v ^= pX * 0x4CE2A0C3;
        v ^= pY * 0x1A57AA2C;
        v ^= v >> 8;
        v *= 0x2C52ACC7;
        v %= pMaxValue;
        return v > 0 ? v : -v;
    }
}