using System;
using System.Windows;
using System.Windows.Forms;

namespace TplPlayground.Core
{
    /// <summary>
    /// A wrapper for a <see cref="Window"/> that exposes the underlying
    /// handle for the <see cref="IWin32Window"/>.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.IWin32Window" />
    internal class Wpf32Window : IWin32Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Wpf32Window"/> class.
        /// </summary>
        /// <param name="window">The window to wrap.</param>
        public Wpf32Window(Window window)
        {
            Handle = new System.Windows.Interop.WindowInteropHelper(window).Handle;
        }

        #region IWin32Window Members

        /// <summary>
        /// Gets the handle to the window.
        /// </summary>
        public IntPtr Handle
        {
            get;
        }

        #endregion
    }
}
