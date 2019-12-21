using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace ColorPicker
{
  public partial class MainWindow : Window
  {
    private Thread _previewThread { get; set; }
    private SavedColor[] _savedColors { get; } = new SavedColor[4];
    private Color _currentColor = Colors.White;

    public MainWindow()
    {
      InitializeComponent();
      Closing += MainWindow_Closing;

      KeyDown += MainWindow_KeyDown;

      for (int i = 0; i < _savedColors.Length; i++)
      {
        _savedColors[i] = new SavedColor(Colors.White);
        StkSavedColors.Children.Add(_savedColors[i]);
      }

      _previewThread = new Thread(RefreshPreview);
      _previewThread.Start();
    }

    private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
      switch (e.Key)
      {
        case System.Windows.Input.Key.F1: _savedColors[0].SetColor(_currentColor); break;
        case System.Windows.Input.Key.F2: _savedColors[1].SetColor(_currentColor); break;
        case System.Windows.Input.Key.F3: _savedColors[2].SetColor(_currentColor); break;
        case System.Windows.Input.Key.F4: _savedColors[3].SetColor(_currentColor); break;
      }
    }

    private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      _previewThread.Abort();
    }

    private void RefreshPreview()
    {
      while (true)
      {
        Thread.Sleep(15);

        Dispatcher.Invoke(new Action(() =>
        {
          PickerPreview.Source = ScreenCapture.Capture(out _currentColor);
          ColorPreview.Background = new SolidColorBrush(_currentColor);
          FillCurrentColorInformation(_currentColor);
        }));
      }
    }

    private void FillCurrentColorInformation(Color color)
    {
      TxtPixelR.Text = color.R.ToString();
      TxtPixelG.Text = color.G.ToString();
      TxtPixelB.Text = color.B.ToString();
      TxtPixelRGB.Text = Helpers.ColorToRGBString(color);
      TxtPixelHex.Text = Helpers.ColorToHexString(color);
    }
  }
}
