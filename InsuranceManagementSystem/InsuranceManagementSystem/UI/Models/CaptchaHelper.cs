using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public static class CaptchaHelper
{
    public static Image GenerateCaptchaImage(string text)
    {
        // Create an image with specified text
        var image = new Bitmap(150, 50);
        var graphics = Graphics.FromImage(image);

        // Set background color and font
        graphics.Clear(Color.White);
        var font = new Font("Arial", 20, FontStyle.Bold);
        var brush = new SolidBrush(Color.Black);

        // Draw the text on the image
        graphics.DrawString(text, font, brush, 10, 10);

        return image;
    }

    public static byte[] ImageToByteArray(Image image)
    {
        using (var ms = new MemoryStream())
        {
            image.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
    }
}
