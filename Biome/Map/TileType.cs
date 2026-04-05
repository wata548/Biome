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
            TileType.Desert => Color.LightGoldenrodYellow,
            TileType.Savanna => Color.DarkOrange,
            TileType.Plain => Color.YellowGreen,
            TileType.Freezing => Color.White,
            TileType.Taiga => Color.DarkGreen,
            TileType.Mountain => Color.DarkGray,
            TileType.DarkForest => Color.DarkSlateGray,
            TileType.Swamp => Color.Aquamarine
        };
     public static int ToMin(this TileType pTile) =>
            pTile switch {
                TileType.Error => 0,
                TileType.Water => 30,
                TileType.DeepSea => 25,
                TileType.Forest => 40,
                TileType.Desert => 40,
                TileType.Savanna => 40,
                TileType.Plain => 40,
                TileType.Freezing => 40,
                TileType.Taiga => 50,
                TileType.Mountain => 50,
                TileType.DarkForest => 45,
                TileType.Swamp => 38
            };
     
     public static int ToMax(this TileType pTile) =>
            pTile switch {
                TileType.Error => 0,
                TileType.Water => 45,
                TileType.DeepSea => 35,
                TileType.Forest => 55,
                TileType.Desert => 50,
                TileType.Savanna => 70,
                TileType.Plain => 45,
                TileType.Freezing => 65,
                TileType.Taiga => 66,
                TileType.Mountain => 75,
                TileType.DarkForest => 55,
                TileType.Swamp => 50
            };
}