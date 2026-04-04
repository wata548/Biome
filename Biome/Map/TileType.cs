using System.Drawing;

namespace Biome;

public enum TileType {
    Error,
    Water,
    DeepSea,
    Hill,
    Desert,
    Freezing,
    Tundra
}

public static class TileManager {
    public static Color ToColor(this TileType pTile) =>
        pTile switch {
            TileType.Error => Color.Magenta,
            TileType.Water => Color.MidnightBlue,
            TileType.DeepSea => Color.Black,
            TileType.Hill => Color.YellowGreen,
            TileType.Desert => Color.DarkOrange,
            TileType.Freezing => Color.White,
            TileType.Tundra => Color.DarkGreen
        };
}