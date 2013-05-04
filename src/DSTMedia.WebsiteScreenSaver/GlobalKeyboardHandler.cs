using System.Windows.Forms;

namespace DSTMedia.WebsiteScreenSaver
{
    public class GlobalKeyboardHandler : IMessageFilter
    {
        private const int WM_KEYDOWN = 0x0100;
        
        public delegate void KeyDownEvent();

        public event KeyDownEvent KeyDown;

        #region IMessageFilter Members

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_KEYDOWN)
            {
                if (KeyDown != null)
                    KeyDown();
            }

            // Always allow message to continue to the next filter control
            return false;
        }

        #endregion
    }
}