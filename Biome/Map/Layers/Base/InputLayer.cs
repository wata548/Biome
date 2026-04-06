namespace Biome;

public abstract class InputLayer<T>: ILayer<T> where T: notnull {
    public abstract LayerOutput<T> Get(LayerOutput<T> pArgs, Coord pPos, int pSeed);
}