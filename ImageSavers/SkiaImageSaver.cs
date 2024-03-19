using System.Drawing;
using SkiaSharp;

namespace KLG.Drawing.ImageSavers;

public class SkiaImageSaver : IImageSaver
{
    public Task SaveImageFile(string path, Color[,] colors)
    {
        using SKBitmap bmp = new(colors.GetLength(0), colors.GetLength(1));
        for (int x = 0; x < bmp.Width; x++)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                Color currentColor = colors[x, y];
                SKColor color = new(currentColor.R, currentColor.G, currentColor.B, currentColor.A);
                bmp.SetPixel(x, y, color);
                colors[x, y] = Color.FromArgb(color.Red, color.Green, color.Blue);
            }
        }
        using FileStream fs = File.Open(path, FileMode.Create);
        SKImage.FromBitmap(bmp).Encode().SaveTo(fs);
        return Task.CompletedTask;
    }

    public Task SaveImageFileParallel(string path, Color[,] colors)
    {
        using SKBitmap bmp = new(colors.GetLength(0), colors.GetLength(1));
        Parallel.For(0, bmp.Width, x =>
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                Color currentColor = colors[x, y];
                SKColor color = new(currentColor.R, currentColor.G, currentColor.B, currentColor.A);
                bmp.SetPixel(x, y, color);
                colors[x, y] = Color.FromArgb(color.Red, color.Green, color.Blue);
            }
        });
        return Task.CompletedTask;
    }
}
