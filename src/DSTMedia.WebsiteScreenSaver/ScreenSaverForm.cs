using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DSTMedia.WebsiteScreenSaver
{
    public partial class ScreenSaverForm : Form
    {
        #region Preview Window Declarations

        // Changes the parent window of the specified child window
        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        // Changes an attribute of the specified window
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        // Retrieves information about the specified window
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        // Retrieves the coordinates of a window's client area
        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        #endregion

        #region Private Members

        // Flag used to for initially setting mouseMove location
        private bool _active;

        // Used to determine if the Mouse has actually been moved
        private Point _mouseLocation;

        // Used to indicate if screensaver is in Preview Mode
        private bool _previewMode = false;

        #endregion

        #region Constructors

        public ScreenSaverForm()
        {
            InitializeComponent();
            RegisterMouseEvents();
            RegisterKeyboardEvents();
        }

        public ScreenSaverForm(IntPtr previewHandle)
        {
            InitializeComponent();
            RegisterMouseEvents();
            RegisterKeyboardEvents();

            // Set the preview window of the screen saver selection 
            // dialog in Windows as the parent of this form.
            SetParent(Handle, previewHandle);

            // Set this form to a child form, so that when the screen saver selection 
            // dialog in Windows is closed, this form will also close.
            SetWindowLong(Handle, -16, new IntPtr(GetWindowLong(Handle, -16) | 0x40000000));

            // Set the size of the screen saver to the size of the screen saver 
            // preview window in the screen saver selection dialog in Windows.
            Rectangle parentRect;
            
            GetClientRect(previewHandle, out parentRect);
            Size = parentRect.Size;
            Location = new Point(0, 0);

            _previewMode = true;
        }

        private void RegisterMouseEvents()
        {
            var mouseHandler = new GlobalMouseHandler();
            mouseHandler.MouseMoved += MouseMove;
            mouseHandler.MouseDown += MouseDown;
            Application.AddMessageFilter(mouseHandler);
        }

        private void RegisterKeyboardEvents()
        {
            var keyboardHandler = new GlobalKeyboardHandler();
            keyboardHandler.KeyDown += KeyDown;
            Application.AddMessageFilter(keyboardHandler);
        }

        #endregion

        #region Events

        /// <summary>
        /// Points the web browser at the site configured in the settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScreenSaverForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Initialise private members
                _active = false;

                // Hide the cursor
                Cursor.Hide();

                // Set the page on the browser
                Browser.Url = new Uri(Properties.Settings.Default.WebpageUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error loading screen saver! {0}", ex.Message), "Website Screen Saver", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the mouse move event. Closes the screen saver if not in preview mode.
        /// </summary>
        private void MouseMove()
        {
            if (_previewMode) 
                return;

            var cursorPosition = Cursor.Position;
            
            // If the mouseLocation still points to 0,0, move it to its actual 
            // location and save the location. Otherwise, check to see if the user
            // has moved the mouse at least 10 pixels.
            if (!_active)
            {
                _mouseLocation = new Point(cursorPosition.X, cursorPosition.Y);
                _active = true;
            }
            else
            {
                // Exit the screensaver
                if ((Math.Abs(cursorPosition.X - _mouseLocation.X) > 10) || (Math.Abs(cursorPosition.Y - _mouseLocation.Y) > 10))
                    Application.Exit();
            }
        }

        /// <summary>
        /// Handles the mouse down event. Closes the screen saver if not in preview mode.
        /// </summary>
        private void MouseDown()
        {
            // Exit the screensaver if not in preview mode
            if (!_previewMode)
                Application.Exit();
        }

        /// <summary>
        /// Handles the key down event. Closes the screen saver if not in preview mode.
        /// </summary>
        private void KeyDown()
        {
            // Exit the screensaver if not in preview mode
            if (!_previewMode)
                Application.Exit();
        }

        #endregion
    }
}