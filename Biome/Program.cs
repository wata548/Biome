using System.Drawing;
using Biome;

public class Program {
    public static void Main(string[] args) {
        var randomLayer = new RandomLayer<TileType>([
            new(TileType.Field, 1),
            new(TileType.Water, 10)
        ]);
        var generate = new AddIslandLayer<TileType>(TileType.Field, TileType.Water, new float[,] {
            { 0.12f, 0.25f, 0.12f },
            { 0.25f, 0, 0.25f },
            { 0.12f, 0.25f, 0.12f }
        });
        var zoom = new ZoomLayer<TileType>(2, 2, true);
        
        var zoomAndGenerate = new LayerComposite<TileType>([zoom, generate]);
        var graph = new LayerComposite<TileType>([
            zoomAndGenerate,
            zoomAndGenerate,
            generate, 
            generate,
            zoom,
            zoom,
            zoom,
            zoom,
        ], true, TileManager.ToColor);
        graph.Get(randomLayer, new(256, 256), null).ToImage("Test.png", TileManager.ToColor);
    }
}