namespace Biome;

public interface ILayer<T> {
    public abstract LayerOutput<T> Get(LayerOutput<T> pArgs, Coord pPos, int pSeed);
}
