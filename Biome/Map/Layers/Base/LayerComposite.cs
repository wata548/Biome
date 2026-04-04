using System.Drawing;

namespace Biome;

public class LayerComposite<T>(List<MiddleLayer<T>> pLayers, bool pSaveImage = false, Func<T, Color> pTranslate = null): MiddleLayer<T> {
    public List<MiddleLayer<T>> MiddleLayer { get; set; } = pLayers;
    public bool SaveImage { get; set; } = pSaveImage;
    public Func<T, Color> Translate { get; set; } = pTranslate;
    
    public override LayerOutput<T> Get(T[] pInput, Coord pSize, Coord pPos) {
        int idx = 0;
        var cur = new LayerOutput<T>(pSize, pInput);
        foreach (var layer in MiddleLayer) {
            cur = layer.Get(cur.Output, cur.Size, pPos);
            if (SaveImage)
                cur.ToImage($"Output/LayerOutput{idx++}.png", Translate);              
        }
        return cur;
    }
}