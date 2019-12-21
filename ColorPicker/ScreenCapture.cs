using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ColorPicker
{
  public static class ScreenCapture
  {
    [DllImport("gdi32.dll")]
    public static extern bool DeleteObject(IntPtr hObject);

    public static BitmapSource Capture(out System.Windows.Media.Color currentPixel)
    {
      System.Drawing.Point cursorPosition = GetCursorPosition();
      using (Bitmap result = new Bitmap(30, 30, System.Drawing.Imaging.PixelFormat.Format64bppPArgb))
      {
        using (Graphics g = Graphics.FromImage(result))
        {
          g.CopyFromScreen(cursorPosition.X - 15, cursorPosition.Y - 15, 0, 0, result.Size, CopyPixelOperation.SourceCopy);
        }

        currentPixel = result.GetPixel(15, 15).Convert();
        result.SetPixel(15, 15, Color.Red);

        IntPtr hBitMap = result.GetHbitmap();
        BitmapSource src = ConvertBitmap(hBitMap);
        DeleteObject(hBitMap);

        return src;
      }
    }

    private static System.Windows.Media.Color Convert(this System.Drawing.Color color)
      => System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);

    private static System.Drawing.Point GetCursorPosition()
      => new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y);

    private static BitmapSource ConvertBitmap(IntPtr HBitMap)
      => Imaging.CreateBitmapSourceFromHBitmap(
            HBitMap,
            IntPtr.Zero,
            Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());
  }
}
