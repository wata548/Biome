namespace Biome;

public interface ILayer<T> {
    public abstract LayerOutput<T> Get(T[] pInput, Coord pSize, Coord pPos);
}
