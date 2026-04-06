using System.Drawing;

namespace Biome;

public class MapGenerator: MiddleLayer<TileType> {

    private readonly InputLayer<TileType> _inputLayer;
    private readonly MiddleLayer<TileType> _composition;
    private readonly MiddleLayer<TileType> _zoom;
    private readonly bool _saveProcess; 

    public MapGenerator(bool pSaveProcess) {
        _saveProcess = pSaveProcess;
        _inputLayer = new RandomLayer<TileType>([
            new(TileType.Forest, 1),
            new(TileType.Water, 10)
        ]);
        var generateFilter = new int[,] {
            {1, 2, 1},
            {2, 0 ,2},
            {1, 2, 1},
        };
        var generate = new AddIslandLayer<TileType>(generateFilter);
        _zoom = new ZoomLayer<TileType>(true);
        var fillSea = new StochasticFillLayer<TileType>(TileType.Water, TileType.Forest, 0.5f);
        var fillDeepSea = new StochasticFillLayer<TileType>(TileType.Water, TileType.DeepSea, 0.5f);
        var addTemperatures = new ReplaceLayer<TileType>(TileType.Forest, [
            new(TileType.Desert, 2),
            new(TileType.Taiga, 1),
            new(TileType.Freezing, 1),
        ]);
        var adjustTemperatures1 = new ChangeByCloseLayer<TileType>(TileType.Desert, TileType.Forest, [
            TileType.Taiga,
            TileType.Freezing
        ]);
        var adjustTemperatures2 = new ChangeByCloseLayer<TileType>(TileType.Freezing, TileType.Taiga, [
            TileType.Forest
        ]);
        var separateWarm = new ReplaceLayer<TileType>(TileType.Desert, [
            new(TileType.Desert, 3),
            new(TileType.Savanna, 2),
            new(TileType.Plain, 1),
        ]);
        var separateForest = new ReplaceLayer<TileType>(TileType.Forest, [
            new(TileType.Forest, 1),
            new(TileType.Mountain, 1),
            new(TileType.Plain, 1),
            new(TileType.DarkForest, 1),
            new(TileType.Swamp, 1),
        ]);
        var separateCold = new ReplaceLayer<TileType>(TileType.Taiga, [
            new(TileType.Forest, 1),
            new(TileType.Mountain, 1),
            new(TileType.Taiga, 1),
            new(TileType.Plain, 1),
        ]);
        
        var zoomAndGenerate = new LayerComposite<TileType>([_zoom, generate]);
        _composition = new LayerComposite<TileType>([
            zoomAndGenerate,
            zoomAndGenerate,
            generate, 
            generate,
            fillSea,
            addTemperatures,
            generate,
            adjustTemperatures1,
            adjustTemperatures2,
            _zoom,
            _zoom,
            fillDeepSea,
            separateWarm,
            separateForest,
            separateCold,
        ], _saveProcess, TileManager.ToColor);
    }
    
    public override LayerOutput<TileType> Get(LayerOutput<TileType> pArgs, Coord pPos, int pSeed) {
        var inputLayerOutput = _inputLayer.Get(pArgs, pPos, pSeed);
        var result = _composition.Get(inputLayerOutput, pPos, pSeed);
        var idx = 0;
        while (result.PixelPerSize != Coord.One) {
            result = _zoom.Get(result, pPos, pSeed);
            if (_saveProcess) {
                result.ToImage($"Output/Zoom{idx++}.png", TileManager.ToColor);
            }
        }
        return result;
    }
}