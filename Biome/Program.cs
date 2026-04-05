using System.Drawing;
using Biome;

public class Program {
    public static void Main(string[] args) {
        var randomLayer = new RandomLayer<TileType>([
            new(TileType.Forest, 1),
            new(TileType.Water, 10)
        ]);
        var generateFilter = new int[,] {
            {1, 2, 1},
            {2, 0 ,2},
            {1, 2, 1},
        };
        var generate = new AddIslandLayer<TileType>(generateFilter);
        var zoom = new ZoomLayer<TileType>(1, 2, true);
        var realZoom = new ZoomLayer<TileType>(2, 2, true);
        var fillSea = new StochasticFillLayer<TileType>(TileType.Water, TileType.Forest, 0.5f);
        var fillDeepSea = new StochasticFillLayer<TileType>(TileType.Water, TileType.DeepSea, 0.5f);
        var addTemperatures = new ReplaceLayer<TileType>(TileType.Forest, [
            new(TileType.Desert, 4),
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
        
        var zoomAndGenerate = new LayerComposite<TileType>([zoom, generate]);
        var graph = new LayerComposite<TileType>([
            zoomAndGenerate,
            zoomAndGenerate,
            generate, 
            generate,
            fillSea,
            addTemperatures,
            generate,
            adjustTemperatures1,
            adjustTemperatures2,
            zoom,
            zoom,
            fillDeepSea,
            separateWarm,
            separateForest,
            separateCold,
            realZoom,
            realZoom,
            realZoom,
        ], true, TileManager.ToColor);
        graph.Get(randomLayer, new(4 , 4), null).ToImage("Test.png", TileManager.ToColor);
    }
}