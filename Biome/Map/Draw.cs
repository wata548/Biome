using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Biome;

public static class Draw {


    public static void Generate<T>(string pPath, Coord pSize, T[] pDatas, Func<T, Color> pTranslate) =>
        Generate(pPath, pSize, pDatas, pTranslate, Color.Black);
    
    public static void Generate<T>(string pPath, Coord pSize, T[] pDatas, Func<T, Color> pData2Color, Color pDefaultColor) {
        using var bmp = new Bitmap(pSize.X, pSize.Y, PixelFormat.Format24bppRgb);
        var data = bmp.LockBits(
            new(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite, 
            bmp.PixelFormat
        );

        var stride = data.Stride;
        var length = stride * pSize.Y;
        var buffer = new byte[length];
        
        var coord = new Coord(0, 0);
        for (int mapIdx = 0; coord.X < pSize.X; coord.X++) {
            for (coord.Y = 0; coord.Y < pSize.Y; coord.Y++, mapIdx++) {
                var idx = coord.Y * stride + coord.X * 3;
                var color = pDefaultColor;
                if(mapIdx < pDatas.Length)
                    color = pData2Color(pDatas[mapIdx]);
                
                buffer[idx] = color.B;
                buffer[idx + 1] = color.G;
                buffer[idx + 2] = color.R;
            }
        }
        Marshal.Copy(buffer, 0, data.Scan0, length);
        
        bmp.UnlockBits(data);
        bmp.Save(pPath, ImageFormat.Png);
    }
}