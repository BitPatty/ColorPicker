using System;
using System.Windows.Media;

namespace ColorPicker
{
  public static class Helpers
  {
    public static string ColorToHexString(Color color)
      => String.Format("#{0:X}{1:X}{2:X}", color.R, color.G, color.B);

    public static string ColorToRGBString(Color color)
      => String.Format("{0}, {1}, {2}", color.R, color.G, color.B);
  }
}
