using Biome.Extendsions;

namespace Biome;

public class AddIslandLayer<T>(int[,] pProbabilityFilter): MiddleLayer<T> where T : notnull {
    private int[,] _probabilityFilter = pProbabilityFilter;
    private Coord _filterSize = new(pProbabilityFilter.GetLength(1), pProbabilityFilter.GetLength(0));
    private Random _random = new();
   public override LayerOutput<T> Get(T[] pInput, Coord pSize, Coord pPos) {
        if ((_probabilityFilter.GetLength(0) & 1) == 0 || (_probabilityFilter.GetLength(1) & 1) == 0)
            throw new ArgumentException("filter size must be odd");

        var result = (pInput.Clone() as T[])!;
        for (int x = 0; x < pSize.X; x++) {
            for (int y = 0; y < pSize.Y; y++) {
                var probability = new OrderedDictionary<T, int>();
                for (int fx = 0; fx < _filterSize.X; fx++) {
                    for (int fy = 0; fy < _filterSize.Y; fy++) {
                        var cx = x + fx - _filterSize.X / 2;
                        var cy = y + fy - _filterSize.Y / 2;

                        if (cx < 0 || cx >= pSize.X || cy < 0 || cy >= pSize.Y)
                            continue;
                        int idx = 0;
                        var c = pInput[cx + cy * pSize.Y];
                        probability.TryAdd(c, 0);
                        probability[c] += _probabilityFilter[fx, fy];
                    }
                }

                var fixedSum = 0;
                var temp = probability.Select(kvp => new KeyValuePair<T, int>(kvp.Key,fixedSum += kvp.Value))
                    .ToList();
                var r = _random.Next(fixedSum) + 1;
                var value = temp.UpperBound(r, kvp => kvp.Value).Key;
                result[x + pSize.X * y] = value;
            }
        }

        return new(pSize, result);
    }
}