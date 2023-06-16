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
        private string FileName = @"data.xlsx";
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
            name = $"{NameBox.Text} {FamBox}";
            phone = phoneBox.Text;
            Contact ct = new Contact();
            ct.Name = name;
            ct.Phone = phone;
            mybook.list.Add(ct);

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
                return; // Если символ цифра, то возвращаемся из метода
            e.Handled = true;
        }
    }
}
