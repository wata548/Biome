namespace Biome;

public abstract class InputLayer<T>: ILayer<T> {
    public abstract LayerOutput<T> Get(T[] pInput, Coord pSize, Coord pPos);
}