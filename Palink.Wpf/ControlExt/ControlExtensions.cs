using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Palink.Wpf.ControlExt;

public static class ControlExtensions
{
    public static void SaveControlContent(this FrameworkElement element, string fileName)
    {
        var bounds = VisualTreeHelper.GetDescendantBounds(element);
        const double dpi = 96d;

        var rtb = new RenderTargetBitmap((int)bounds.Width,
            (int)bounds.Height, dpi, dpi, PixelFormats.Default);

        var dv = new DrawingVisual();
        using (var dc = dv.RenderOpen())
        {
            var vb = new VisualBrush(element);
            dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
        }

        rtb.Render(dv);

        BitmapEncoder pngEncoder = new PngBitmapEncoder();
        pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

        try
        {
            var ms = new MemoryStream();

            pngEncoder.Save(ms);
            ms.Close();

            File.WriteAllBytes(fileName, ms.ToArray());
        }
        catch (Exception err)
        {
            MessageBox.Show(err.ToString(), "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}