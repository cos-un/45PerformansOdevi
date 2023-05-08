using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace PizzaSiparisProgramı
{
    public partial class Form1 : Form
    {
        private SQLiteConnection connection;
        private DataTable combinedDataTable;

        public Form1()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            textBox1.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "";
            comboBox7.Text = "";
            comboBox8.Text = "";
            comboBox9.Text = "";
            comboBox10.Text = "";
            comboBox11.Text = "";
            comboBox12.Text = "";
            comboBox13.Text = "";

        }

        private void LoadData()
        {
            string hazirConString = "Data Source=C:\\Users\\COŞĞUN\\Documents\\atolye11\\Veritabanı\\hazırPizza.db;Version=3";
            string kisiselConString = "Data Source=C:\\Users\\COŞĞUN\\Documents\\atolye11\\Veritabanı\\kisiselPizza.db;Version=3";

            DataTable hazirDataTable = new DataTable();
            DataTable kisiselDataTable = new DataTable();

            using (SQLiteConnection hazirConnection = new SQLiteConnection(hazirConString))
            using (SQLiteConnection kisiselConnection = new SQLiteConnection(kisiselConString))
            {
                hazirConnection.Open();
                kisiselConnection.Open();

                string hazirQuery = "SELECT * FROM hazır";
                string kisiselQuery = "SELECT * FROM kisisel";

                SQLiteDataAdapter hazirAdapter = new SQLiteDataAdapter(hazirQuery, hazirConnection);
                SQLiteDataAdapter kisiselAdapter = new SQLiteDataAdapter(kisiselQuery, kisiselConnection);

                hazirAdapter.Fill(hazirDataTable);
                kisiselAdapter.Fill(kisiselDataTable);
            }

            combinedDataTable = new DataTable();
            combinedDataTable.Merge(hazirDataTable);
            combinedDataTable.Merge(kisiselDataTable);

            dataGridView1.DataSource = combinedDataTable;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            Clear();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            groupBox2.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = true;
        }


        //Kaydet
        private void button2_Click(object sender, EventArgs e)
        {
            string AdSoyad = textBox1.Text;

            if (radioButton1.Checked) // Hazır Pizza seçildiyse
            {
                if (comboBox9.SelectedItem == null || comboBox10.SelectedItem == null || comboBox11.SelectedItem == null || comboBox12.SelectedItem == null || comboBox13.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen tüm seçenekleri doldurun.");
                    return;
                }

                string pizzaAdı = comboBox9.SelectedItem.ToString();
                string boyut = comboBox10.SelectedItem.ToString();
                string icecek = comboBox11.SelectedItem.ToString();
                string patates = comboBox12.SelectedItem.ToString();
                string sos = comboBox13.SelectedItem.ToString();

                string conString = "Data Source=C:\\Users\\COŞĞUN\\Documents\\atolye11\\Veritabanı\\hazırPizza.db;Version=3";
                connection = new SQLiteConnection(conString);
                connection.Open();

                string query = "INSERT INTO hazır (AdSoyad, PizzaAdı, Boyut, Icecek, Patates, Sos) " +
                               "VALUES (@AdSoyad, @PizzaAdı, @Boyut, @Icecek, @Patates, @Sos)";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AdSoyad", AdSoyad);
                    command.Parameters.AddWithValue("@PizzaAdı", pizzaAdı);
                    command.Parameters.AddWithValue("@Boyut", boyut);
                    command.Parameters.AddWithValue("@Icecek", icecek);
                    command.Parameters.AddWithValue("@Patates", patates);
                    command.Parameters.AddWithValue("@Sos", sos);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
            else if (radioButton2.Checked) // Kişisel Pizza seçildiyse
            {
                if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || comboBox3.SelectedItem == null || comboBox4.SelectedItem == null || comboBox5.SelectedItem == null || comboBox6.SelectedItem == null || comboBox7.SelectedItem == null || comboBox8.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen tüm seçenekleri doldurun.");
                    return;
                }

                string hamurInceligi = comboBox1.SelectedItem.ToString();
                string boyut = comboBox2.SelectedItem.ToString();
                string malzeme1 = comboBox3.SelectedItem.ToString();
                string malzeme2 = comboBox4.SelectedItem.ToString();
                string malzeme3 = comboBox5.SelectedItem.ToString();
                string patates = comboBox6.SelectedItem.ToString();
                string icecek = comboBox7.SelectedItem.ToString();
                string sos = comboBox8.SelectedItem.ToString();

                string conString = "Data Source=C:\\Users\\COŞĞUN\\Documents\\atolye11\\Veritabanı\\kisiselPizza.db;Version=3";
                connection = new SQLiteConnection(conString);
                connection.Open();

                string query = "INSERT INTO kisisel (AdSoyad, HamurInceligi, Boyut, Malzeme1, Malzeme2, Malzeme3, Patates, Icecek, Sos) " +
                               "VALUES (@AdSoyad, @HamurInceligi, @Boyut, @Malzeme1, @Malzeme2, @Malzeme3, @Patates, @Icecek, @Sos)";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AdSoyad", AdSoyad);
                    command.Parameters.AddWithValue("@HamurInceligi", hamurInceligi);
                    command.Parameters.AddWithValue("@Boyut", boyut);
                    command.Parameters.AddWithValue("@Malzeme1", malzeme1);
                    command.Parameters.AddWithValue("@Malzeme2", malzeme2);
                    command.Parameters.AddWithValue("@Malzeme3", malzeme3);
                    command.Parameters.AddWithValue("@Patates", patates);
                    command.Parameters.AddWithValue("@Icecek", icecek);
                    command.Parameters.AddWithValue("@Sos", sos);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            MessageBox.Show("Kayıt başarıyla eklendi.");

            LoadData();
            Clear();
        }

        //SİL
        private void button1_Click(object sender, EventArgs e)
        {
            string adSoyad = textBox1.Text;

            if (string.IsNullOrEmpty(adSoyad))
            {
                MessageBox.Show("Lütfen AdSoyad değerini girin.");
                return;
            }

            string conString = "";

            if (radioButton1.Checked)
            {
                conString = "Data Source=C:\\Users\\COŞĞUN\\Documents\\atolye11\\Veritabanı\\hazırPizza.db;Version=3";
            }
            else if (radioButton2.Checked)
            {
                conString = "Data Source=C:\\Users\\COŞĞUN\\Documents\\atolye11\\Veritabanı\\kisiselPizza.db;Version=3";
            }
            else
            {
                MessageBox.Show("Lütfen bir Pizza türü seçin.");
                return;
            }

            using (SQLiteConnection connection = new SQLiteConnection(conString))
            {
                connection.Open();

                string query = "";

                if (radioButton1.Checked)
                {
                    query = "DELETE FROM hazır WHERE AdSoyad = @AdSoyad";
                }
                else if (radioButton2.Checked)
                {
                    query = "DELETE FROM kisisel WHERE AdSoyad = @AdSoyad";
                }

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AdSoyad", adSoyad);
                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla silindi.");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Belirtilen AdSoyad ile eşleşen kayıt bulunamadı.");
                    }
                }
            }
            Clear();
        }

        //Bul
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                string adSoyad = selectedRow.Cells["AdSoyad"].Value.ToString();

                if (radioButton1.Checked) // Hazır Pizza
                {
                    string pizzaAdı = selectedRow.Cells["PizzaAdı"].Value.ToString();
                    string boyut = selectedRow.Cells["Boyut"].Value.ToString();
                    string icecek = selectedRow.Cells["Icecek"].Value.ToString();
                    string patates = selectedRow.Cells["Patates"].Value.ToString();
                    string sos = selectedRow.Cells["Sos"].Value.ToString();

                    comboBox9.SelectedItem = pizzaAdı;
                    comboBox10.SelectedItem = boyut;
                    comboBox11.SelectedItem = icecek;
                    comboBox12.SelectedItem = patates;
                    comboBox13.SelectedItem = sos;
                }
                else if (radioButton2.Checked) // Kişisel Pizza
                {
                    string hamurInceligi = selectedRow.Cells["HamurInceligi"].Value.ToString();
                    string boyut = selectedRow.Cells["Boyut"].Value.ToString();
                    string malzeme1 = selectedRow.Cells["Malzeme1"].Value.ToString();
                    string malzeme2 = selectedRow.Cells["Malzeme2"].Value.ToString();
                    string malzeme3 = selectedRow.Cells["Malzeme3"].Value.ToString();
                    string patates = selectedRow.Cells["Patates"].Value.ToString();
                    string icecek = selectedRow.Cells["Icecek"].Value.ToString();
                    string sos = selectedRow.Cells["Sos"].Value.ToString();

                    comboBox1.SelectedItem = hamurInceligi;
                    comboBox2.SelectedItem = boyut;
                    comboBox3.SelectedItem = malzeme1;
                    comboBox4.SelectedItem = malzeme2;
                    comboBox5.SelectedItem = malzeme3;
                    comboBox6.SelectedItem = patates;
                    comboBox7.SelectedItem = icecek;
                    comboBox8.SelectedItem = sos;
                }

                textBox1.Text = adSoyad;
            }
            else
            {
                MessageBox.Show("Lütfen bir satır seçin.");
            }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string adSoyad = textBox1.Text;
                string hamurInceligi = comboBox1.SelectedItem.ToString();
                string boyut = comboBox2.SelectedItem.ToString();
                string malzeme1 = comboBox3.SelectedItem.ToString();
                string malzeme2 = comboBox4.SelectedItem.ToString();
                string malzeme3 = comboBox5.SelectedItem.ToString();
                string patates = comboBox6.SelectedItem.ToString();
                string icecek = comboBox7.SelectedItem.ToString();
                string sos = comboBox8.SelectedItem.ToString();

                string conString = "Data Source=C:\\Users\\COŞĞUN\\Documents\\atolye11\\Veritabanı\\kisiselPizza.db;Version=3";
                using (SQLiteConnection connection = new SQLiteConnection(conString))
                {
                    connection.Open();

                    string query = "UPDATE kisisel SET HamurInceligi = @HamurInceligi, Boyut = @Boyut, Malzeme1 = @Malzeme1, Malzeme2 = @Malzeme2, Malzeme3 = @Malzeme3, Patates = @Patates, Icecek = @Icecek, Sos = @Sos WHERE AdSoyad = @AdSoyad";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@HamurInceligi", hamurInceligi);
                        command.Parameters.AddWithValue("@Boyut", boyut);
                        command.Parameters.AddWithValue("@Malzeme1", malzeme1);
                        command.Parameters.AddWithValue("@Malzeme2", malzeme2);
                        command.Parameters.AddWithValue("@Malzeme3", malzeme3);
                        command.Parameters.AddWithValue("@Patates", patates);
                        command.Parameters.AddWithValue("@Icecek", icecek);
                        command.Parameters.AddWithValue("@Sos", sos);
                        command.Parameters.AddWithValue("@AdSoyad", adSoyad);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Kayıt başarıyla güncellendi.");
                LoadData();
            }
            else
            {
                MessageBox.Show("Lütfen güncellenecek bir kayıt seçin.");
            }
            Clear();
        }
    }
}