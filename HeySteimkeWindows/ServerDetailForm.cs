using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeySteimkeWindows
{
    public partial class ServerDetailForm : Form
    {
        public ServerDetailForm()
        {
            InitializeComponent();
        }

        public bool IsBusy { get; private set; }

        public async Task LoadData()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var userv = await ResourceManager.DataStore.GetUserServiceAsync();
                var profile = await userv.GetProfileAsync();
                serverTextBox.Text = profile.Server;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var userv = await ResourceManager.DataStore.GetUserServiceAsync();
                await userv.SetServerAsync(serverTextBox.Text);
            }catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ServerDetailForm_Load(object sender, EventArgs e)
        {
            await LoadData();
        }
    }
}
