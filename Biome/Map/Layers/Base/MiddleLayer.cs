using System.Drawing;

namespace Biome;

public record LayerOutput<T>(Coord Size, T[] Output) {
    public void ToImage(string pPath, Func<T, Color> pTranslate) => Draw.Generate(pPath, Size, Output, pTranslate);
}

public abstract class MiddleLayer<T>:ILayer<T> {
    public abstract LayerOutput<T> Get(T[] pInput, Coord pSize, Coord pPos);

    public LayerOutput<T> Get(InputLayer<T> pInput, Coord pSize, Coord pPos) {
        var data = pInput.Get(null, pSize, pPos);
        return Get(data.Output, data.Size, pPos);
    }
}