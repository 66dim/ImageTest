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

            Console.WriteLine("Введите размерность матрицы размытия: ");
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

                    for (int i = y - shiftAroundPixel, k = 0; i <= y + shiftAroundPixel; i++, k++)
                    {
                        for (int j = x - shiftAroundPixel, m = 0; j <= x + shiftAroundPixel; j++, m++)
                        {
                            Color pixel = inputImage.GetPixel(j, i);

                            double red = pixel.R * matrix[k, m];
                            double green = pixel.G * matrix[k, m];
                            double blue = pixel.B * matrix[k, m];

                            redSum += red;
                            greenSum += green;
                            blueSum += blue;
                        }
                    }

                    int outputRed = GetOutputComponentValue((int)Math.Round(redSum, MidpointRounding.AwayFromZero));
                    int outputGreen = GetOutputComponentValue((int)Math.Round(greenSum, MidpointRounding.AwayFromZero));
                    int outputBlue = GetOutputComponentValue((int)Math.Round(blueSum, MidpointRounding.AwayFromZero));

                    Color newPixel = Color.FromArgb(outputRed, outputGreen, outputBlue);

                    outputImage.SetPixel(x, y, newPixel);
                }
            }

            outputImage.Save("..\\..\\..\\out.jpg", ImageFormat.Jpeg);
        }

        public static int GetOutputComponentValue(int inputComponentValue)
        {
            if (inputComponentValue < 0)
            {
                return 0;
            }
            if (inputComponentValue > 255)
            {
                return 255;
            }

            return inputComponentValue;
        }
    }
}
