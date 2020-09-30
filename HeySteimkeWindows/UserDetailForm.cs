using HeySteimke.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeySteimkeWindows
{
    public partial class UserDetailForm : Form
    {
        Person person;

        public bool IsBusy { get; private set; }
        public UserDetailForm(Person itemNode)
        {
            person = itemNode;
            if (person == null) person = new Person();
            InitializeComponent();
        }
        public async Task LoadData()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var userv = await ResourceManager.DataStore.GetUserServiceAsync();
                if(!await userv.CanEditUserAsync(person))
                {
                    MessageBox.Show("access denied");
                    IsBusy = false;
                    return;
                }

                nameTextBox.Text = person.Name;
                idTextBox.Text = person.Id.ToString();
                passwordTextBox.Text = person.Pw;
                workloadTextBox.Text = person.Workload.ToString();
                stateTextBox.Text = person.State.ToString();
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
        public async Task SaveData()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                if(person.Id <= 0)
                {
                    IsBusy = false;
                    return;
                }

                var userv = await ResourceManager.DataStore.GetUserServiceAsync();
                if (!await userv.CanUpdateAsync(person))
                {
                    MessageBox.Show("access denied");
                    IsBusy = false;
                    return;
                }

                person.Name = nameTextBox.Text;
                person.Pw = passwordTextBox.Text;
                person.Workload =  int.Parse(workloadTextBox.Text);

                if(await userv.CanEditUserAnyAync())
                {
                    person.State = (UserState)Enum.Parse(typeof(UserState), stateTextBox.Text);
                }

                await userv.UpdateAsync(person);
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

        private async void UserDetailForm_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            await SaveData();
        }
    }
}
