using System.Windows.Forms;

namespace DSTMedia.WebsiteScreenSaver
{
    public class GlobalMouseHandler : IMessageFilter
    {
        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_LBUTTONDOWN = 0x0201;

        public delegate void MouseMovedEvent();
        public delegate void MouseDownEvent();

        public event MouseMovedEvent MouseMoved;
        public event MouseDownEvent MouseDown;

        #region IMessageFilter Members

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEMOVE)
            {
                if (MouseMoved != null)
                    MouseMoved();
                
            }

            if (m.Msg == WM_LBUTTONDOWN)
            {
                if (MouseDown != null)
                    MouseDown();
            }

            // Always allow message to continue to the next filter control
            return false;
        }

        #endregion
    }
}