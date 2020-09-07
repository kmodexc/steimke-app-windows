using HeySteimke.Models;
using HeySteimke.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

        private void recursiveTreeNodeExpanded(TreeNodeCollection nodes, string basekey, Dictionary<string,bool> expandDict)
        {
            if (nodes == null || expandDict == null || nodes.Count <= 0) return;
            foreach(var subnode in nodes)
            {
                var treeNodeSubNod = subnode as TreeNode;
                if(treeNodeSubNod != null)
                {
                    var newKey = basekey + "->" + treeNodeSubNod.Text;

                    if(expandDict.ContainsKey(newKey))
                    {
                        //throw new Exception("key " + newKey + " already exists");
                    }
                    else
                    {
                        expandDict.Add(newKey, treeNodeSubNod.IsExpanded);
                    }
                    recursiveTreeNodeExpanded(treeNodeSubNod.Nodes, newKey, expandDict);
                }
                else
                {
                    throw new Exception("unvalid node");
                }
            }
        }

        public async Task LoadData()
        {
            if (IsBusy) return;
            IsBusy = true;

            if (!await ResourceManager.DataStore.PingServer())
            {
                MessageBox.Show("cant ping server");
                IsBusy = false;
                return;
            }

            var pserv = await ResourceManager.DataStore.GetPlacesServiceAsync();
            var iserv = await ResourceManager.DataStore.GetItemsServiceAsync();
            var userv = await ResourceManager.DataStore.GetUserServiceAsync();
            if (await userv.NeedsInitializationAsync())
            {
                var mf = new LoginForm();
                mf.ShowDialog(this);
            }
            // if user is guest dont let it show data
            if (!(await iserv.CanAddItemTodoAsync()))
            {
                MessageBox.Show("guests cant use this program. contact admin if you need access");
                IsBusy = false;
                return;
            }
            var places = await pserv.GetAllAsync();
            if (places == null)
            {
                MessageBox.Show("could not load places");
                IsBusy = false;
                return;
            }
            var root = ItemsTreeView.Nodes;

            // save all expanded to make the new items expanded in same way
            Dictionary<string, bool> expandenDict = new Dictionary<string, bool>();
            recursiveTreeNodeExpanded(root, "root", expandenDict);

            root.Clear();
            foreach (var place in places)
            {
                TreeNode node = new TreeNode(place.Name);
                root.Add(node);
                node.Tag = place;
                node.ContextMenuStrip = placeContextMenu;

                var openit = await iserv.GetOpen(place);
                TreeNode open = new TreeNode("offen");
                node.Nodes.Add(open);
                foreach (var it in openit)
                {
                    TreeNode node1 = new TreeNode(it.Name);
                    node1.Tag = it;
                    node1.ContextMenuStrip = itemContextMenu;
                    open.Nodes.Add(node1);
                }
                string nodeKey = "root->" + place + "->offen";
                if (expandenDict.ContainsKey(nodeKey) && expandenDict[nodeKey])
                    open.Expand();
                var progit = await iserv.GetInProgress(place);
                TreeNode inProgress = new TreeNode("in Arbeit");
                node.Nodes.Add(inProgress);
                foreach (var it in progit)
                {
                    TreeNode node1 = new TreeNode(it.Name);
                    node1.Tag = it;
                    node1.ContextMenuStrip = itemContextMenu;
                    inProgress.Nodes.Add(node1);
                }
                nodeKey = "root->" + place + "->in Arbeit";
                if (expandenDict.ContainsKey(nodeKey) && expandenDict[nodeKey])
                    inProgress.Expand();
                var finit = await iserv.GetFinished(place);
                TreeNode finished = new TreeNode("beendet");
                node.Nodes.Add(finished);
                foreach (var it in finit)
                {
                    TreeNode node1 = new TreeNode(it.Name);
                    node1.Tag = it;
                    node1.ContextMenuStrip = itemContextMenu;
                    finished.Nodes.Add(node1);
                }
                nodeKey = "root->" + place + "->beendet";
                if (expandenDict.ContainsKey(nodeKey) && expandenDict[nodeKey])
                    finished.Expand();

                nodeKey = "root->" + place;
                if (expandenDict.ContainsKey(nodeKey) && expandenDict[nodeKey])
                    node.Expand();
            }

            IsBusy = false;
        }

        private async void HeySteimkeStart_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async Task createItem(Place p)
        {
            var iserv = await ResourceManager.DataStore.GetItemsServiceAsync();
            if (await iserv.CanAddItemTodoAsync())
            {
                var item = new HeySteimke.Models.Item(-1, ItemState.created, "", "", "", -1, -1, 0, 0, p.Id);
                await iserv.Create(item);
                await openItemDetail(item);
            }
            else
            {
                MessageBox.Show("access denied");
            }
        }
        private async Task openItemDetail(Item it)
        {
            var idp = new ItemDetailForm(it);
            idp.ShowDialog(this);
            await LoadData();
        }
        private async void ItemsTreeView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (ItemsTreeView.SelectedNode == null) return;
                var placeNode = ItemsTreeView.SelectedNode.Tag as Place;
                var itemNode = ItemsTreeView.SelectedNode.Tag as Item;
                if (itemNode != null)
                {
                    await openItemDetail(itemNode);
                }
                else if (placeNode != null)
                {
                    await createItem(placeNode);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No Handler for this");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Exception while process click:\n" + exc);
            }
        }

        private async void execSkriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var userv = await ResourceManager.DataStore.GetUserServiceAsync();

            if ((await userv.GetProfileAsync()).State != UserState.admin)
            {
                MessageBox.Show("no admin");
                return;
            }

            var pserv = await ResourceManager.DataStore.GetPlacesServiceAsync();
            var iserv = await ResourceManager.DataStore.GetItemsServiceAsync();
            var dataBase = new DataStore();
            await dataBase.LoadConfig();
            string str = "";

            var tmp = await dataBase.GetPlacesAsync();
            if (tmp == null)
            {
                MessageBox.Show("cant load places");
                return;
            }
            var places = new List<Place>(tmp);

            foreach (var it in places)
            {

                if (string.IsNullOrWhiteSpace(it.Name) || it.Name == string.Empty)
                {
                    str += "Place:" + it.Name + " " + it.Id + "\n";
                    foreach (var op in await iserv.GetOpen(it))
                    {
                        str += "Open:" + op.Name + "\n";
                    }
                    foreach (var op in await iserv.GetInProgress(it))
                    {
                        str += "Progress:" + op.Name + "\n";
                    }
                    foreach (var op in await iserv.GetFinished(it))
                    {
                        str += "Closed:" + op.Name + "\n";
                    }
                    //str += "delete: " + it.Name + " " + it.Id + "\n";
                    try
                    {
                        //await pserv.RemovePlaceAsync(it);
                    }
                    catch (Exception) { }
                }
                //if(it.Id == 11)
                //{
                //    str += "rename " + it.Name + " " + it.Id + "\n";
                //    it.Name = "Ruben";
                //    RestBase rbase = new RestBase();
                //    //urest.ReplaceUser(it.Id, rbase.toRestUser(it));
                //}
            }

            //var items = await dataBase.GetItemsAsync();

            //foreach (var it in items)
            //{
            //    if (it.CreatorId == 10)
            //    {
            //        if (it.Id >= 78 || it.Id == 8)
            //        {
            //            //str += it.Name + " " + it.Id + " " + it.PlaceId + "\n";
            //            //await iserv.Delete(it);
            //        }
            //    }
            //}

            MessageBox.Show(str);
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new LoginForm()).Show(this);
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        private TreeNode contextMenuHandlerGetNode(object sender)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem == null) return null;
            ContextMenuStrip strip = menuItem.Owner as ContextMenuStrip;
            if (strip == null) return null;
            TreeView tree = strip.SourceControl as TreeView;
            if (tree == null) return null;
            return tree.SelectedNode;
        }

        private async void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = contextMenuHandlerGetNode(sender);
            if (node == null) return;
            var itemNode = node.Tag as Item;
            if (itemNode == null) return;

            await openItemDetail(itemNode);
        }

        private void ItemsTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            TreeView tree = sender as TreeView;
            if (tree == null) return;
            TreeNode node = tree.GetNodeAt(e.X, e.Y);
            tree.SelectedNode = node;
        }

        private async void addItemMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = contextMenuHandlerGetNode(sender);
            if (node == null) return;
            var placeNode = node.Tag as Place;
            if (placeNode == null) return;

            await createItem(placeNode);
        }

        private async Task deleteItem(Item it)
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var iserv = await ResourceManager.DataStore.GetItemsServiceAsync();
                if (await iserv.CanDeleteAsync(it))
                {
                    await iserv.Delete(it);
                }
                else
                {
                    MessageBox.Show("access denied");
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void deletePlaceMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = contextMenuHandlerGetNode(sender);
            if (node == null) return;
            var itemNode = node.Tag as Place;
            if (itemNode == null) return;
            await deletePlace(itemNode);
        }

        private async Task deletePlace(Place itemNode)
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var pserv = await ResourceManager.DataStore.GetPlacesServiceAsync();
                if (await pserv.CanDeleteAsync(itemNode))
                {
                    await pserv.RemovePlaceAsync(itemNode);
                }
                else
                {
                    MessageBox.Show("access denied");
                }
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

        private async void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = contextMenuHandlerGetNode(sender);
            if (node == null) return;
            var itemNode = node.Tag as Item;
            if (itemNode == null) return;
            await deleteItem(itemNode);
        }
    }
}
