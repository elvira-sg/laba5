using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BankSystem
{
    public partial class BankManagerForm : Form
    {
        private List<BankClient> clients;
        private Database database;

        public BankManagerForm()
        {
            database = new Database();
            clients = new List<BankClient>();
            InitializeComponent();
            SetupDataGridView();
            LoadDataFromDatabase();
        }

        private void SetupDataGridView()
        {
            dataGridView.Columns.Add("colName", "Имя");
            dataGridView.Columns.Add("colPassport", "Паспорт");
            dataGridView.Columns.Add("colBalance", "Баланс");
        }

        private void ChkUseDatabase_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseDatabase.Checked)
            {
                LoadDataFromDatabase();
            }
            else
            {
                // При отключении БД очищаем список клиентов
                clients.Clear();
                RefreshGrid();
            }
        }

        private void LoadDataFromDatabase()
        {
            if (chkUseDatabase.Checked)
            {
                clients = database.GetAllClients();
                RefreshGrid();
            }
        }

        private void RefreshGrid()
        {
            dataGridView.Rows.Clear();
            foreach (var client in clients)
            {
                dataGridView.Rows.Add(client.Name, client.Passport, client.Balance.ToString("F2"));
            }
        }

        private void OnAddClick(object sender, EventArgs e)
        {
            ClientEditForm form = new ClientEditForm();
            if (form.ShowDialog() == DialogResult.OK && form.Client != null)
            {
                clients.Add(form.Client);

                if (chkUseDatabase.Checked)
                {
                    database.AddClient(form.Client);
                }

                RefreshGrid();
            }
        }

        private void OnEditClick(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите клиента.", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idx = dataGridView.SelectedRows[0].Index;
            if (idx < 0 || idx >= clients.Count) return;

            BankClient client = clients[idx];

            ClientEditForm form = new ClientEditForm(client);
            if (form.ShowDialog() == DialogResult.OK && form.Client != null)
            {
                clients[idx] = form.Client;

                if (chkUseDatabase.Checked)
                {
                    database.UpdateClient(form.Client);
                }

                RefreshGrid();
            }
        }

        private void OnDeleteClick(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0) return;

            int idx = dataGridView.SelectedRows[0].Index;
            if (idx < 0 || idx >= clients.Count) return;

            if (MessageBox.Show("Удалить выбранного клиента?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            BankClient client = clients[idx];
            clients.RemoveAt(idx);

            if (chkUseDatabase.Checked)
            {
                database.DeleteClient(client.Passport);
            }

            RefreshGrid();
        }

        private void OnSaveClick(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                DefaultExt = "txt"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(dialog.FileName, false, System.Text.Encoding.UTF8))
                    {
                        foreach (var client in clients)
                        {
                            sw.WriteLine($"{client.Name}|{client.Passport}|{client.Balance}");
                        }
                    }
                    MessageBox.Show("Данные сохранены в файл!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OnLoadClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var loadedClients = database.LoadFromFile(dialog.FileName);

                    if (!chkUseDatabase.Checked)
                    {
                        // В локальном режиме заменяем все данные
                        clients = loadedClients;
                    }
                    else
                    {
                        // В режиме БД добавляем к существующим
                        clients.AddRange(loadedClients);

                        // И добавляем в БД
                        foreach (var client in loadedClients)
                        {
                            try
                            {
                                database.AddClient(client);
                            }
                            catch
                            {
                                // Если клиент уже существует, пропускаем
                                continue;
                            }
                        }
                    }

                    RefreshGrid();
                    MessageBox.Show("Данные загружены из файла!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OnSortClick(object sender, EventArgs e)
        {
            clients = clients.OrderBy(c => c.Balance).ToList();
            RefreshGrid();
        }

        private void OnExportDBClick(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt",
                DefaultExt = "txt"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var allClients = database.GetAllClients();
                    database.SaveToFile(dialog.FileName, allClients);
                    MessageBox.Show($"Данные из БД экспортированы в файл! ({allClients.Count} записей)", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OnImportDBClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    database.ImportFromFile(dialog.FileName);
                    LoadDataFromDatabase();
                    MessageBox.Show("Данные импортированы в БД!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}