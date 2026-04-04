namespace Biome;

public abstract class InputLayer<T>: ILayer<T> where T: notnull {
    public abstract LayerOutput<T> Get(T[] pInput, Coord pSize, Coord pPos);
}