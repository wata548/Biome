using System.Collections.Immutable;

namespace Biome;

public class ChangeByCloseLayer<T>(T pBefore, T pAfter, T[] pTarget): MiddleLayer<T> where T : notnull {
    private T _before = pBefore;
    private T _after = pAfter;
    private ImmutableSortedSet<T> _target = pTarget.ToImmutableSortedSet();
    
    public override LayerOutput<T> Get(LayerOutput<T> pArgs, Coord pPos, int pSeed) {
        var size = pArgs.Size;
        var input = pArgs.Output;
        for (int x = 0; x < size.X; x++) {
            for (int y = 0; y < size.Y; y++) {
                if(!input[x + y * size.X].Equals(_before))
                    continue;
                
                foreach (var dir in Coord.Directions) {
                    var cx = x + dir.X;
                    var cy = y + dir.Y;
                    if (cx < 0 || cx >= size.X || cy < 0 || cy >= size.Y) 
                        continue;
                    if (_target.Contains(input[cx + cy * size.X])) {
                        input[x + y * size.X] = _after;
                        break;
                    }
                }
            }   
        }

        return pArgs with{Depth = pArgs.Depth + 1};
    }
}