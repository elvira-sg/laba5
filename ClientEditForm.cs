using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BankSystem
{
    public partial class ClientEditForm : Form
    {
        public BankClient Client { get; private set; }
        private BankClient clientToEdit;

        public ClientEditForm(BankClient client = null)
        {
            clientToEdit = client;
            InitializeComponent();

            if (client != null)
            {
                Text = "Редактировать клиента";
                txtName.Text = client.Name;
                txtPassport.Text = client.Passport;
                txtPassport.ReadOnly = true;
                numBalance.Value = Math.Min(numBalance.Maximum, (decimal)client.Balance);
            }
            else
            {
                Text = "Добавить клиента";
            }
        }

        private void OnOKClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Имя не может быть пустым.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtPassport.Text.Length != 8 || !Regex.IsMatch(txtPassport.Text, "^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("Паспорт должен содержать ровно 8 буквенно-цифровых символов.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Client = new BankClient(txtName.Text, txtPassport.Text, (double)numBalance.Value);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}