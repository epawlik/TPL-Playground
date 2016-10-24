using System.Windows;
using System.Windows.Forms;

namespace TplPlayground.Core
{
    /// <summary>
    /// Extensions for the <see cref="System.Windows.Window"/>.
    /// </summary>
    /// <seealso cref="System.Windows.Window"/>
    internal static class WindowExtensions
    {
        /// <summary>
        /// Wraps the <see cref="Window"/> as an <see cref="IWin32Window"/>.
        /// </summary>
        /// <param name="window">The window to wrap.</param>
        /// <returns>The <see cref="IWin32Window"/> representing the <see cref="Window"/>.</returns>
        public static IWin32Window AsWin32Window(this Window window) =>
            new Wpf32Window(window);
    }
}
