using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker
{
  public partial class SavedColor : UserControl
  {
    public SavedColor(Color color)
    {
      InitializeComponent();
      FillCurrentColorInformation(color);
    }

    private void FillCurrentColorInformation(Color color)
    {
      CanvasPreview.Background = new SolidColorBrush(color);
      TxtPixelR.Text = color.R.ToString();
      TxtPixelG.Text = color.G.ToString();
      TxtPixelB.Text = color.B.ToString();
      TxtPixelRGB.Text = Helpers.ColorToRGBString(color);
      TxtPixelHex.Text = Helpers.ColorToHexString(color);
    }

    public void SetColor(Color color) => FillCurrentColorInformation(color);
  }
}
