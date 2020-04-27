using HeySteimke.Models;
using HeySteimke.Services;
using HeySteimke.Services.Rest.HeySteimkeUser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeySteimkeWindows
{
    public partial class HeySteimkeStart : Form
    {
        public bool IsBusy { get; set; }

        public HeySteimkeStart()
        {
            InitializeComponent();
        }

        public async Task LoadData()
        {
            if (IsBusy) return;
            IsBusy = true;

            var pserv = await ResourceManager.DataStore.GetPlacesServiceAsync();
            var iserv = await ResourceManager.DataStore.GetItemsServiceAsync();
            var userv = await ResourceManager.DataStore.GetUserServiceAsync();
            if (await userv.NeedsInitializationAsync())
            {
                var mf = new NewUserForm();
                mf.ShowDialog(this);
                return;
            }
            var places = await pserv.GetAllAsync();
            var root = ItemsTreeView.Nodes;
            root.Clear();
            foreach (var place in places)
            {
                TreeNode node = new TreeNode(place.Name);
                root.Add(node);
                node.Tag = place;

                await userv.SetPlaceAsync(place.Id);
                var openit = await iserv.GetOpen();
                TreeNode open = new TreeNode("offen");
                node.Nodes.Add(open);
                foreach (var it in openit)
                {
                    TreeNode node1 = new TreeNode(it.Name);
                    node1.Tag = it;
                    open.Nodes.Add(node1);
                }
                var progit = await iserv.GetInProgress();
                TreeNode inProgress = new TreeNode("in Arbeit");
                node.Nodes.Add(inProgress);
                foreach (var it in progit)
                {
                    TreeNode node1 = new TreeNode(it.Name);
                    node1.Tag = it;
                    inProgress.Nodes.Add(node1);
                }
                var finit = await iserv.GetFinished();
                TreeNode finished = new TreeNode("beendet");
                node.Nodes.Add(finished);
                foreach (var it in finit)
                {
                    TreeNode node1 = new TreeNode(it.Name);
                    node1.Tag = it;
                    finished.Nodes.Add(node1);
                }

            }

            IsBusy = false;
        }

        private async void HeySteimkeStart_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async void ItemsTreeView_DoubleClick(object sender, EventArgs e)
        {
            var placeNode = ItemsTreeView.SelectedNode.Tag as Place;
            var itemNode = ItemsTreeView.SelectedNode.Tag as Item;
            if (itemNode != null)
            {
                var idp = new ItemDetailForm(itemNode);
                idp.ShowDialog(this);
                await LoadData();
            }
            else if (placeNode != null)
            {
                var userv = await ResourceManager.DataStore.GetUserServiceAsync();
                await userv.SetPlaceAsync(placeNode.Id);
                var item = new HeySteimke.Models.Item(-1, ItemState.created, "HeySteimkeWindowsItem");
                var iserv = await ResourceManager.DataStore.GetItemsServiceAsync();
                await iserv.Create(item);
                var idp = new ItemDetailForm(item);
                idp.ShowDialog(this);
                await LoadData();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No Handler for this");
            }
        }

        private async void execSkriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var userv = await ResourceManager.DataStore.GetUserServiceAsync();
            var pserv = await ResourceManager.DataStore.GetPlacesServiceAsync();
            var iserv = await ResourceManager.DataStore.GetItemsServiceAsync();
            var dataBase = new DataStore();
            var urest = new HeySteimkeUserClient(null);
            string str = "";


            var users = await userv.GetAllAsync();

            foreach (var it in users)
            {
                str += it.Name + " " + it.Id + "\n";
                if (it.Id == 15 || it.Id == 16)
                {
                    str += "delete: "+it.Name + " " + it.Id + "\n";
                    //urest.DeleteUser(it.Id);
                }
            }

            MessageBox.Show(str);
        }
    }
}
