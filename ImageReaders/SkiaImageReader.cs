using System.Drawing;
using SkiaSharp;

namespace KLG.Drawing.ImageReaders;

public class SkiaImageReader : IImageReader
{
    public Color[,] LoadImageFile(string path)
    {
        using SKImage img = SKImage.FromEncodedData(path);
        using SKBitmap bmp = SKBitmap.FromImage(img);
        Color[,] colors = new Color[img.Width, img.Height];
        Parallel.For(0, img.Width, x =>
        {
            for (int y = 0; y < img.Height; y++)
            {
                SKColor color = bmp.GetPixel(x, y);
                colors[x, y] = Color.FromArgb(color.Red, color.Green, color.Blue);
            }
        });
        return colors;
    }
}
