using System.Drawing;

namespace Biome;

public abstract class MiddleLayer<T>:ILayer<T> where T: notnull {
    public abstract LayerOutput<T> Get(LayerOutput<T> pArgs, Coord pPos, int pSeed);
}