using Biome.Extendsions;

namespace Biome;

public class AddIslandLayer<T>(int[,] pProbabilityFilter): MiddleLayer<T> where T : notnull {
    private int[,] _probabilityFilter = pProbabilityFilter;
    private Coord _filterSize = new(pProbabilityFilter.GetLength(1), pProbabilityFilter.GetLength(0));
    public override LayerOutput<T> Get(LayerOutput<T> pArgs, Coord pPos, int pSeed) {
        if ((_probabilityFilter.GetLength(0) & 1) == 0 || (_probabilityFilter.GetLength(1) & 1) == 0)
            throw new ArgumentException("filter size must be odd");

        var size = pArgs.Size;
        var input = pArgs.Output;
        var result = (pArgs.Output.Clone() as T[])!;
        var seed = pSeed + pArgs.Depth;
        
        for (int x = 0; x < size.X; x++) {
            for (int y = 0; y < size.Y; y++) {
                var probability = new OrderedDictionary<T, int>();
                for (int fx = 0; fx < _filterSize.X; fx++) {
                    for (int fy = 0; fy < _filterSize.Y; fy++) {
                        var cx = x + fx - _filterSize.X / 2;
                        var cy = y + fy - _filterSize.Y / 2;

                        if (cx < 0 || cx >= size.X || cy < 0 || cy >= size.Y)
                            continue;
                        int idx = 0;
                        var c = input[cx + cy * size.Y];
                        probability.TryAdd(c, 0);
                        probability[c] += _probabilityFilter[fx, fy];
                    }
                }

                var fixedSum = 0;
                var temp = probability.Select(kvp => new KeyValuePair<T, int>(kvp.Key,fixedSum += kvp.Value))
                    .ToList();
                var r = Random(
                    seed,
                    pArgs.StartPos.X + x * pArgs.PixelPerSize.X,
                    pArgs.StartPos.Y + y * pArgs.PixelPerSize.Y,
                    fixedSum
                ) + 1;
                var value = temp.UpperBound(r, kvp => kvp.Value).Key;
                result[x + size.X * y] = value;
            }
        }

        return pArgs with{Output = result, Depth = pArgs.Depth + 1};

        int Random(int pSeed, int pX, int pY, int pMaxValue) {
            var v = pSeed;
            v ^= pX * 0x587EA0B3;
            v ^= pY * 0x5847ACDF;
            v ^= v >> 10;
            v *= 0x3E125FCB;
            v %= pMaxValue;
            return v > 0 ? v : -v;
        }
    }
   
}