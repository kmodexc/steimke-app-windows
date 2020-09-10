using HeySteimke.Models;
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
    public partial class PlaceDetailForm : Form
    {
        Place place;
        public bool IsBusy { get; private set; }
        public PlaceDetailForm(Place p)
        {
            place = p;
            if (place == null) place = new Place();
            InitializeComponent();
            nameTextBox.Text = place.Name;
            privateCheckBox.Checked = (place.Type == PlaceType._private);
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var pserv = await ResourceManager.DataStore.GetPlacesServiceAsync();
                if (!await pserv.CanEditAsync(place))
                {
                    MessageBox.Show("access denied");
                    IsBusy = false;
                    return;
                }
                else
                {
                    // TODO create function
                    // await pserv.UpdatePlaceAsync(place);
                    MessageBox.Show("feature not supported");
                }

            }catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadData()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var userv = await ResourceManager.DataStore.GetUserServiceAsync();
                var creator = (await userv.GetAsync(place.CreatorId));
                if(creator == null || creator.Id <= 0)
                {
                    creatorLabel.Text = "id(" + place.CreatorId + ")";
                }
                else
                {
                    creatorLabel.Text = creator.Name;
                }
            }catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void PlaceDetailForm_Load(object sender, EventArgs e)
        {
            await LoadData();
        }
    }
}
