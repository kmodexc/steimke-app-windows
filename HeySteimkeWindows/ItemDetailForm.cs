using HeySteimke.Models;
using HeySteimke.Rest.HeySteimkeBase.Models;
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
    public partial class ItemDetailForm : Form
    {
        HeySteimke.Models.Item item;
        public ItemDetailForm(HeySteimke.Models.Item item)
        {
            InitializeComponent();
            this.item = item;
        }

        public async Task LoadData()
        {
            var userv = await ResourceManager.DataStore.GetUserServiceAsync();
            if(item.CreatorId > 0)
            {
                var creator = await userv.GetAsync(item.CreatorId.GetValueOrDefault());
                createdLabel.Text = "Erstellt von "+creator.Name;
            }
            else
            {
                createdLabel.Text = "[kein creator] ";
            }
            if(item.AssignedId > 0)
            {
                var assigned = await userv.GetAsync(item.AssignedId.GetValueOrDefault());
                acceptedLabel.Text = "Angenommen von " + assigned.Name + " am " + TimeStampToString(item.Assignedtime);
            }
            else
            {
                this.acceptedLabel.Text = "";
            }
            createdLabel.Text += " am " + TimeStampToString(item.Createtime);
            nameTextBox.Text = item.Name;
            descriptionTextBox.Text = item.Desc;
            switch (item.ItemState)
            {
                case ItemState.created:
                    itemActionButton.Text = "Annehmen";
                    break;
                case ItemState.assigned:
                    itemActionButton.Text = "Beenden";
                    break;
                default:
                    itemActionButton.Text = "Löschen";
                    break;
            }
        }

        private string TimeStampToString(TimeStamp ts)
        {
            return "" + ts.Day + "." + ts.Month + "." + ts.Year + " " + ts.Hour + ":" + ts.Minute + " Uhr";
        }

        private async void ItemDetailForm_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async void itemActionButton_Click(object sender, EventArgs e)
        {
            var iserv = await ResourceManager.DataStore.GetItemsServiceAsync();
            switch (item.ItemState)
            {
                case ItemState.created:
                    //itemActionButton.Text = "Annehmen";
                    await iserv.Assigne(item);
                    break;
                case ItemState.assigned:
                    //itemActionButton.Text = "Beenden";
                    await iserv.Finish(item);
                    break;
                default:
                    //itemActionButton.Text = "Löschen";
                    await iserv.Delete(item);
                    break;
            }
            this.Close();
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            var iserv = await ResourceManager.DataStore.GetItemsServiceAsync();
            await iserv.Update(item);
            this.Close();
        }
    }
}
