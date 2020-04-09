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
    public partial class NewUserForm : Form
    {
        public NewUserForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SaveData().ConfigureAwait(false);
            this.Close();
        }

        private async Task SaveData()
        {
            var userv = await ResourceManager.DataStore.GetUserServiceAsync();
            string name = nameTextBox.Text;
            await userv.SetName(name.Trim());
        }

        private void NewUserForm_Load(object sender, EventArgs e)
        {
        }

        private void NewUserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
