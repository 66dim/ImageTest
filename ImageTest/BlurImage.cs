using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageTest
{
    class BlurImage
    {
        static void Main(string[] args)
        {
            using Bitmap inputImage = new Bitmap("..\\..\\..\\image.jpg");
            using Bitmap outputImage = new Bitmap(inputImage.Width, inputImage.Height);

            Console.WriteLine("Введите размерность матрицы размытия:");
            int matrixDimension = Convert.ToInt32(Console.ReadLine());

            double[,] matrix = new double[matrixDimension, matrixDimension];

            double coefficient = 1.0 / (matrixDimension * matrixDimension);

            for (int i = 0; i < matrixDimension; ++i)
            {
                for (int j = 0; j < matrixDimension; ++j)
                {
                    matrix[i, j] = coefficient;
                }
            }

            int shiftAroundPixel = matrixDimension / 2;

            int heightPixelBorder = inputImage.Height - shiftAroundPixel;
            int widthPixelBorder = inputImage.Width - shiftAroundPixel;

            for (int y = shiftAroundPixel; y < heightPixelBorder; ++y)
            {
                for (int x = shiftAroundPixel; x < widthPixelBorder; ++x)
                {
                    double redSum = 0;
                    double greenSum = 0;
                    double blueSum = 0;

                    for (int adjacentPixelY = y - shiftAroundPixel, i = 0; adjacentPixelY <= y + shiftAroundPixel; adjacentPixelY++, i++)
                    {
                        for (int adjacentPixelX = x - shiftAroundPixel, j = 0; adjacentPixelX <= x + shiftAroundPixel; adjacentPixelX++, j++)
                        {
                            Color pixel = inputImage.GetPixel(adjacentPixelX, adjacentPixelY);

                            redSum += pixel.R * matrix[i, j];
                            greenSum += pixel.G * matrix[i, j];
                            blueSum += pixel.B * matrix[i, j];
                        }
                    }

                    int outputRed = GetSaturatedComponent(redSum);
                    int outputGreen = GetSaturatedComponent(greenSum);
                    int outputBlue = GetSaturatedComponent(blueSum);

                    Color newPixel = Color.FromArgb(outputRed, outputGreen, outputBlue);

                    outputImage.SetPixel(x, y, newPixel);
                }
            }

            outputImage.Save("..\\..\\..\\out.jpg", ImageFormat.Jpeg);
        }

        public static int GetSaturatedComponent(double component)
        {
            if (component <= 0)
            {
                return 0;
            }

            if (component >= 255)
            {
                return 255;
            }

            return (int)Math.Round(component, MidpointRounding.AwayFromZero);
        }
    }
}
