using System.Windows.Forms;
using Palink.Wpf.ScreenExt;

namespace Palink.Wpf;

public class WpfHelper
{
    public static (double scaleX, double scaleY, Screen current) GetDpiScale(
        int index)
    {
        var screen = Screen.AllScreens[0];
        screen.GetDpi(DpiType.Effective, out var xDpi, out var yDpi);
        var scaleX = xDpi / 96.0;
        var scaleY = yDpi / 96.0;
        if (index >= Screen.AllScreens.Length)
        {
            return (scaleX, scaleY, screen);
        }

        screen = Screen.AllScreens[index];
        screen.GetDpi(DpiType.Effective, out xDpi, out yDpi);
        scaleX = xDpi / 96.0;
        scaleY = yDpi / 96.0;
        return (scaleX, scaleY, screen);
    }
}