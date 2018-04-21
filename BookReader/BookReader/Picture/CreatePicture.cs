using System.IO;
using SkiaSharp;
using Xamarin.Forms;

namespace BookReader.Picture
{
    public class CreatePicture
    {
        public static Stream Create(float width, float height)
        {
            SKBitmap skBitmap = new SKBitmap((int)width, (int)height);
            using (SKCanvas canvas = new SKCanvas(skBitmap))
            {
                canvas.DrawColor(SKColors.YellowGreen);
                using (SKPaint skPaint = new SKPaint())
                {
                    skPaint.Typeface = SKFontManager.Default.MatchCharacter('中');
                    skPaint.IsAntialias = true;
                    skPaint.Color = SKColors.White;
                    canvas.DrawText("测试文字", width / 2 ,height /2, skPaint);
                }
                using (SKImage img = SKImage.FromBitmap(skBitmap))
                {
                    using (SKData p = img.Encode(SKEncodedImageFormat.Jpeg, 70))
                    {
                        return p.AsStream();
                    }
                }
            }
        }
    }
}