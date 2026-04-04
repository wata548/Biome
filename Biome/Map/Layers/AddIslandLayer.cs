namespace Biome;

public class AddIslandLayer<T>(T pSuccess, T pFail, float[,] pProbabilityFilter): MiddleLayer<T> {
    private T _success = pSuccess;
    private T _fail = pFail;
    private float[,] _probabilityFilter = pProbabilityFilter;
    private Coord _filterSize = new(pProbabilityFilter.GetLength(1), pProbabilityFilter.GetLength(0));
    private Random _random = new();
    
    public override LayerOutput<T> Get(T[] pInput, Coord pSize, Coord pPos) {
        if ((_probabilityFilter.GetLength(0) & 1) == 0 || (_probabilityFilter.GetLength(1) & 1) == 0)
            throw new ArgumentException("filter size must be odd");
        var temp = _probabilityFilter.Cast<float>().Any(p => p > 1);

        var result = (pInput.Clone() as T[])!;
        for (int x = 0; x < pSize.X; x++) {
            for (int y = 0; y < pSize.Y; y++) {
                var probability = 1f;
                for (int fy = 0; fy < _filterSize.Y; fy++) {
                    for (int fx = 0; fx < _filterSize.X; fx++) {
                        var nx = x + fx - _filterSize.X / 2;
                        var ny = y + fy - _filterSize.Y / 2;

                        if (nx < 0 || nx >= pSize.X || ny < 0 || ny >= pSize.Y)
                            continue;
                        if(pInput[nx + ny * pSize.X]!.Equals(_success))
                            probability *= 1f - _probabilityFilter[fy, fx];
                    }
                }

                if (_random.NextDouble() > probability) 
                    result[x + pSize.X * y] = _success;
                else
                    result[x + pSize.X * y] = _fail;
            }
        }

        return new(pSize, result);
    }
}