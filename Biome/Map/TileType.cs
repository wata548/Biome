using System.Drawing;

namespace Biome;

public enum TileType {
    Error,
    Water,
    Field
}

public static class TileManager {
    public static Color ToColor(this TileType pTile) =>
        pTile switch {
            TileType.Error => Color.Black,
            TileType.Water => Color.DeepSkyBlue,
            TileType.Field => Color.SaddleBrown
        };
}