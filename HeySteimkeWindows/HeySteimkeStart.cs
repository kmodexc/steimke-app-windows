using HeySteimke.Models;
using HeySteimke.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private void recursiveTreeNodeExpanded(TreeNodeCollection nodes, string basekey, Dictionary<string, bool> expandDict)
        {
            if (nodes == null || expandDict == null || nodes.Count <= 0) return;
            foreach (var subnode in nodes)
            {
                var treeNodeSubNod = subnode as TreeNode;
                if (treeNodeSubNod != null)
                {
                    var newKey = basekey + "->" + treeNodeSubNod.Text;

                    if (expandDict.ContainsKey(newKey))
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

        public async Task LoadData(bool refresh = false)
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
            if (refresh)
            {
                await pserv.RefreshAsync();
                await iserv.RefreshAsync();
                await userv.RefreshAsync();
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
                await LoadData();
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
        private void ItemsTreeView_DoubleClick(object sender, EventArgs e)
        {

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

            // Work
            var user1 = await userv.CreateUser("testmod", "192837465");
            await userv.RefreshAsync();
            var user2 = await userv.GetAsync(user1.Id);
            str += user2.Id + "\n";
            str += user2.Name + "\n";
            str += user2.State + "\n";


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
            await LoadData(true);
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
            await LoadData();
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
            await LoadData();
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
                    await LoadData();
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

        private async void deletePlaceMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = contextMenuHandlerGetNode(sender);
            if (node == null) return;
            var itemNode = node.Tag as Place;
            if (itemNode == null) return;
            await deletePlace(itemNode);
            await LoadData();
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
                    await LoadData();
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
            await LoadData();
        }

        private async void editPlaceMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = contextMenuHandlerGetNode(sender);
            if (node == null) return;
            var itemNode = node.Tag as Place;
            if (itemNode == null) return;
            var dialog = new PlaceDetailForm(itemNode);
            dialog.ShowDialog(this);
            await LoadData();
        }

        private async Task LoadUserView(bool refresh = false)
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                var userv = await ResourceManager.DataStore.GetUserServiceAsync();

                if (!await userv.CanEditUserUserAsync())
                {
                    MessageBox.Show("access denied");
                    IsBusy = false;
                    return;
                }
                else
                {
                    var root = ItemsTreeView.Nodes;
                    root.Clear();
                    var users = await userv.GetAllAsync();
                    foreach (var u in users)
                    {
                        var node = new TreeNode();
                        node.Tag = u;
                        node.Text = u.Name;
                        node.ContextMenuStrip = userContextMenuStrip;
                        root.Add(node);
                    }
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

        private async void userViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await LoadUserView();
        }

        private async void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode node = contextMenuHandlerGetNode(sender);
            if (node == null) return;
            var itemNode = node.Tag as Person;
            if (itemNode == null) return;
            var dialog = new UserDetailForm(itemNode);
            dialog.ShowDialog(this);
            await LoadUserView();
        }

        private async Task AddUser()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var userv = await ResourceManager.DataStore.GetUserServiceAsync();

                Person newUser = new Person();
                Random rand = new Random(DateTime.Now.ToString().GetHashCode());
                newUser.Name = "newUser" + rand.Next(100, 999);
                newUser.Pw = rand.Next(100000, 999999).ToString();
                newUser.State = UserState.user;

                if (await userv.CanCreateAsync(newUser))
                {
                    newUser = await userv.CreateAsync(newUser);
                    if (newUser == null || newUser.Id <= 0)
                        throw new Exception("create returned unvalid user");
                    var dialog = new UserDetailForm(newUser);
                    dialog.ShowDialog(this);
                    await LoadUserView(true);
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

        private async void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                await AddUser();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void serverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new ServerDetailForm();
            dialog.ShowDialog(this);
        }

        private async Task AddPlace()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var pserv = await ResourceManager.DataStore.GetPlacesServiceAsync();
                if (await pserv.CanAddPlaceAsync())
                {

                    var newPlace = new Place();
                    Random rand = new Random(DateTime.Now.ToString().GetHashCode());
                    newPlace.Name = "newPlace" + rand.Next(100, 999);
                    await pserv.CreateAsync(newPlace);
                    foreach (var place in await pserv.GetAllAsync())
                    {
                        if (place.Name == newPlace.Name)
                        {
                            var dialog = new PlaceDetailForm(place);
                            dialog.ShowDialog(this);
                            await LoadData();
                            return;
                        }
                    }
                    throw new Exception("created place not found");
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

        private async void addPlaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await AddPlace();
        }

        private async void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode node = contextMenuHandlerGetNode(sender);
            if (node == null) return;
            var itemNode = node.Tag as Person;
            if (itemNode == null) return;
            await DeleteUser(itemNode);
            await LoadUserView(true);
        }

        private async Task DeleteUser(Person itemNode)
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var userv = await ResourceManager.DataStore.GetUserServiceAsync();
                if (await userv.CanDeleteAsync(itemNode))
                {
                    await userv.DeleteAsync(itemNode);
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
    }
}
