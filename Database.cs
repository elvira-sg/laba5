using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BankSystem
{
    public class Database
    {
        private string connectionString;
        private string databasePath = "bank.db";

        public Database()
        {
            // Проверяем текущую директорию
            string currentDir = Directory.GetCurrentDirectory();
            databasePath = Path.Combine(currentDir, "bank.db");

            connectionString = $"Data Source={databasePath};Version=3;";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            try
            {
                // Если файл не существует, создаем его и таблицу
                if (!File.Exists(databasePath))
                {
                    SQLiteConnection.CreateFile(databasePath);
                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        CreateTables(connection);
                    }
                }
                else
                {
                    // Если файл существует, проверяем таблицы
                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        CheckAndCreateTables(connection);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации БД: {ex.Message}\nПуть: {databasePath}",
                    "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateTables(SQLiteConnection connection)
        {
            string createTable = @"
                CREATE TABLE Clients (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Passport TEXT UNIQUE NOT NULL,
                    Balance REAL NOT NULL
                )";

            using (var command = new SQLiteCommand(createTable, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        private void CheckAndCreateTables(SQLiteConnection connection)
        {
            try
            {
                // Проверяем существует ли таблица Clients
                string checkTable = "SELECT name FROM sqlite_master WHERE type='table' AND name='Clients'";
                using (var command = new SQLiteCommand(checkTable, connection))
                {
                    var result = command.ExecuteScalar();
                    if (result == null)
                    {
                        // Таблицы нет - создаем
                        CreateTables(connection);
                    }
                }
            }
            catch
            {
                // В случае ошибки создаем таблицу
                CreateTables(connection);
            }
        }

        public List<BankClient> GetAllClients()
        {
            var clients = new List<BankClient>();

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Name, Passport, Balance FROM Clients ORDER BY Id";
                    using (var command = new SQLiteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clients.Add(new BankClient
                            {
                                Name = reader["Name"].ToString(),
                                Passport = reader["Passport"].ToString(),
                                Balance = Convert.ToDouble(reader["Balance"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиентов: {ex.Message}",
                    "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return clients;
        }

        public void AddClient(BankClient client)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Clients (Name, Passport, Balance) VALUES (@name, @passport, @balance)";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", client.Name);
                        command.Parameters.AddWithValue("@passport", client.Passport);
                        command.Parameters.AddWithValue("@balance", client.Balance);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex) when (ex.Message.Contains("UNIQUE constraint failed"))
            {
                MessageBox.Show($"Клиент с паспортом {client.Passport} уже существует!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления клиента: {ex.Message}",
                    "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public void UpdateClient(BankClient client)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Clients SET Name = @name, Balance = @balance WHERE Passport = @passport";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", client.Name);
                        command.Parameters.AddWithValue("@passport", client.Passport);
                        command.Parameters.AddWithValue("@balance", client.Balance);
                        int rows = command.ExecuteNonQuery();

                        if (rows == 0)
                        {
                            MessageBox.Show($"Клиент с паспортом {client.Passport} не найден!",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления клиента: {ex.Message}",
                    "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public void DeleteClient(string passport)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Clients WHERE Passport = @passport";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@passport", passport);
                        int rows = command.ExecuteNonQuery();

                        if (rows == 0)
                        {
                            MessageBox.Show($"Клиент с паспортом {passport} не найден!",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления клиента: {ex.Message}",
                    "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public void SaveToFile(string filePath, List<BankClient> clients)
        {
            try
            {
                using (var writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    foreach (var client in clients)
                    {
                        writer.WriteLine($"{client.Name}|{client.Passport}|{client.Balance}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения файла: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public List<BankClient> LoadFromFile(string filePath)
        {
            var clients = new List<BankClient>();

            try
            {
                if (!File.Exists(filePath))
                {
                    MessageBox.Show($"Файл не найден: {filePath}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return clients;
                }

                using (var reader = new StreamReader(filePath, System.Text.Encoding.UTF8))
                {
                    string line;
                    int lineNumber = 0;

                    while ((line = reader.ReadLine()) != null)
                    {
                        lineNumber++;

                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        var parts = line.Split('|');

                        if (parts.Length != 3)
                        {
                            MessageBox.Show($"Неверный формат строки {lineNumber}: {line}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }

                        if (!double.TryParse(parts[2], out double balance))
                        {
                            MessageBox.Show($"Неверный баланс в строке {lineNumber}: {parts[2]}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }

                        clients.Add(new BankClient(parts[0], parts[1], balance));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки файла: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

            return clients;
        }

        public void ImportFromFile(string filePath)
        {
            try
            {
                var importedClients = LoadFromFile(filePath);
                int importedCount = 0;
                int skippedCount = 0;

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    foreach (var client in importedClients)
                    {
                        try
                        {
                            // Проверяем, существует ли уже такой паспорт
                            string checkQuery = "SELECT COUNT(*) FROM Clients WHERE Passport = @passport";
                            using (var checkCommand = new SQLiteCommand(checkQuery, connection))
                            {
                                checkCommand.Parameters.AddWithValue("@passport", client.Passport);
                                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                                if (count == 0)
                                {
                                    // Добавляем нового клиента
                                    string insertQuery = "INSERT INTO Clients (Name, Passport, Balance) VALUES (@name, @passport, @balance)";
                                    using (var insertCommand = new SQLiteCommand(insertQuery, connection))
                                    {
                                        insertCommand.Parameters.AddWithValue("@name", client.Name);
                                        insertCommand.Parameters.AddWithValue("@passport", client.Passport);
                                        insertCommand.Parameters.AddWithValue("@balance", client.Balance);
                                        insertCommand.ExecuteNonQuery();
                                        importedCount++;
                                    }
                                }
                                else
                                {
                                    skippedCount++;
                                }
                            }
                        }
                        catch (SQLiteException ex) when (ex.Message.Contains("UNIQUE constraint failed"))
                        {
                            skippedCount++;
                        }
                    }
                }

                MessageBox.Show($"Импорт завершен!\nДобавлено: {importedCount}\nПропущено (дубли): {skippedCount}",
                    "Импорт данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка импорта: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}