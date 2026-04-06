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
        
        for (int mapIdx = 0, x = 0; x < pSize.X; x++) {
            for (int y = 0; y < pSize.Y; y++, mapIdx++) {
                var idx = y * stride + x * 3;
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