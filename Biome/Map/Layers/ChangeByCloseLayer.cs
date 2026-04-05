using System.Collections.Immutable;

namespace Biome;

public class ChangeByCloseLayer<T>(T pBefore, T pAfter, T[] pTarget): MiddleLayer<T> where T : notnull {
    private T _before = pBefore;
    private T _after = pAfter;
    private ImmutableSortedSet<T> _target = pTarget.ToImmutableSortedSet();
    
    public override LayerOutput<T> Get(T[] pInput, Coord pSize, Coord pPos) {
        for (int x = 0; x < pSize.X; x++) {
            for (int y = 0; y < pSize.Y; y++) {
                if(!pInput[x + y * pSize.X].Equals(_before))
                    continue;
                
                foreach (var dir in Coord.Directions) {
                    var cx = x + dir.X;
                    var cy = y + dir.Y;
                    if (cx < 0 || cx >= pSize.X || cy < 0 || cy >= pSize.Y) 
                        continue;
                    if (_target.Contains(pInput[cx + cy * pSize.X])) {
                        pInput[x + y * pSize.X] = _after;
                        break;
                    }
                }
            }   
        }

        return new(pSize, pInput);
    }
}