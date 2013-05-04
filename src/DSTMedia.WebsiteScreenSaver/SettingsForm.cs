using System.Windows.Forms;

namespace DSTMedia.WebsiteScreenSaver
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, System.EventArgs e)
        {
            txtWebPageUrl.Text = Properties.Settings.Default.WebpageUrl;
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            Properties.Settings.Default.WebpageUrl = txtWebPageUrl.Text;
            Properties.Settings.Default.Save();
            Application.Exit();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }
    }
}
