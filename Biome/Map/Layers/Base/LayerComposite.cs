using System.Drawing;

namespace Biome;

public class LayerComposite<T>(List<MiddleLayer<T>> pLayers, bool pSaveImage = false, Func<T, Color> pTranslate = null): MiddleLayer<T> where T : notnull {
    public List<MiddleLayer<T>> MiddleLayer { get; set; } = pLayers;
    public bool SaveImage { get; set; } = pSaveImage;
    public Func<T, Color> Translate { get; set; } = pTranslate;
    
    public override LayerOutput<T> Get(LayerOutput<T> pArgs, Coord pPos, int pSeed) {
        int idx = 0;
        foreach (var layer in MiddleLayer) {
            pArgs = layer.Get(pArgs, pPos, pSeed);
            if (SaveImage)
                pArgs.ToImage($"Output/LayerOutput{idx++}.png", Translate);              
        }
        return pArgs;
    }
}