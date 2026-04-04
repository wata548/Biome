using System.Drawing;
using Biome;

public class Program {
    public static void Main(string[] args) {
        var randomLayer = new RandomLayer<TileType>([
            new(TileType.Hill, 1),
            new(TileType.Water, 10)
        ]);
        var generateFilter = new int[,] {
            {1, 2, 1},
            {2, 0 ,2},
            {1, 2, 1},
        };
        var generate = new AddIslandLayer<TileType>(generateFilter);
        var zoom = new ZoomLayer<TileType>(1, 2, true);
        var fillSea = new StochasticFillLayer<TileType>(TileType.Water, TileType.Hill, 0.5f);
        var fillDeepSea = new StochasticFillLayer<TileType>(TileType.Water, TileType.DeepSea, 0.5f);
        var addTemperatures = new ReplaceLayer<TileType>(TileType.Hill, [
            new(TileType.Desert, 4),
            new(TileType.Tundra, 1),
            new(TileType.Freezing, 1),
        ]);
        
        var zoomAndGenerate = new LayerComposite<TileType>([zoom, generate]);
        var graph = new LayerComposite<TileType>([
            zoomAndGenerate,
            zoomAndGenerate,
            generate, 
            generate,
            fillSea,
            fillDeepSea,
            addTemperatures,
            generate,
            zoom,
            zoom,
            zoom,
        ], true, TileManager.ToColor);
        graph.Get(randomLayer, new(8, 8), null).ToImage("Test.png", TileManager.ToColor);
    }
}