using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zad2
{
    public partial class Form1 :Form
    {
        PhoneBookLoader loader = new PhoneBookLoader();
        PhoneBook mybook = new PhoneBook();
        private string FileName = @"D:\2 Семестр\Практика\zad2\bin\Debug\contacts.txt";
        public Form1 ()
        {
            InitializeComponent();
        }

        private void Form1_Load (object sender, EventArgs e)
        {
            loader.Load(mybook, FileName);
            RefreshDataGrid();
            mybook.ReloadList();
        }
         public void RefreshDataGrid ()
        {
            dataGridView1.DataSource = mybook.table;
        }
        

        private void searchBut_Click (object sender, EventArgs e)
        {
            if (searchBox.Text == "")
            {
                MessageBox.Show("Пустое поле", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string search = searchBox.Text;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().ToLower().Contains(search.ToLower()))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            label2.Text = dataGridView1.Rows[i].Cells[j+1].Value.ToString();
                            return;
                            
                        }
            }
            label2.Text = "Контакт не найден";
            
        }

        private void searchBox_KeyPress (object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) == true) return; 
            e.Handled = true;
        }

        private void addButton_Click (object sender, EventArgs e)
        {
            if(phoneBox.Text== "" || NameBox.Text=="" || FamBox.Text == "")
            {
                MessageBox.Show("Не все поля заполнены", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string name,phone;
            name = $"{NameBox.Text} {FamBox.Text}";
            phone = phoneBox.Text;
            mybook.table.Rows.Add(name, phone);
            mybook.ReloadList();
            RefreshDataGrid();
            MessageBox.Show("Контакт добавлен", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NameBox.Text = "";
            FamBox.Text = "";
            phoneBox.Text = "";

        }

        private void NameBox_KeyPress (object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) == true)
                return;
            e.Handled = true;
        }

        private void phoneBox_KeyPress (object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) == true)
                return; 
            e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string name = selectedRow.Cells[0].Value.ToString();
                mybook.RemoveContact(name);
                RefreshDataGrid();
                MessageBox.Show("Контакт удален", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else MessageBox.Show("Пожалуйста, выберите контакт для удаления.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            loader.Save(mybook,FileName);
        }
    }
}
