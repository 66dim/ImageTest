using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Blur
{
    class BlurImage
    {
        static void Main(string[] args)
        {
            using Bitmap image = new Bitmap("..\\..\\..\\image.jpg");

            for (int y = 0; y < image.Height; ++y)
            {
                for (int x = 0; x < image.Width; ++x)
                {
                    Color pixel = image.GetPixel(x, y);

                    int gray = (int)(Math.Round(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11, MidpointRounding.AwayFromZero));

                    Color newPixel = Color.FromArgb(gray, gray, gray);

                    image.SetPixel(x, y, newPixel);
                }
            }

            image.Save("..\\..\\..\\out.jpg", ImageFormat.Jpeg);
        }
    }
}
