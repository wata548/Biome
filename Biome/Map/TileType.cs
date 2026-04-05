using System.Drawing;

namespace Biome;

public enum TileType {
    Error,
    Water,
    DeepSea,
    Savanna,
    Plain,
    Forest,
    Desert,
    Freezing,
    Taiga,
    Mountain,
    Swamp,
    DarkForest
}

public static class TileManager {
    public static Color ToColor(this TileType pTile) =>
        pTile switch {
            TileType.Error => Color.Magenta,
            TileType.Water => Color.MidnightBlue,
            TileType.DeepSea => Color.Black,
            TileType.Forest => Color.Green,
            TileType.Desert => Color.DarkOrange,
            TileType.Savanna => Color.DarkKhaki,
            TileType.Plain => Color.YellowGreen,
            TileType.Freezing => Color.White,
            TileType.Taiga => Color.DarkGreen,
            TileType.Mountain => Color.DarkGray,
            TileType.DarkForest => Color.DarkSlateGray,
            TileType.Swamp => Color.Aquamarine
        };
}