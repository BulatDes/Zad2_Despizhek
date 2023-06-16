using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using System.IO;
using System.Data;
using System.Collections;
using System.Windows.Forms;

namespace zad2
{
    class PhoneBookLoader
    {
        public void Load (PhoneBook phoneBook, string fileName)
        {
            if (File.Exists(fileName))
            {
                phoneBook.table.Columns.Add("Name");
                phoneBook.table.Columns.Add("Telephone");
                StreamReader str = File.OpenText(fileName);
                while (!str.EndOfStream)
                {
                    string s = str.ReadLine();
                    string[] ss = s.Split(';');
                    phoneBook.AddContact(ss[0], ss[1]);
                }
                str.Close();
            }
            else
            {
                MessageBox.Show("Файл не найден", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }



        public void Save(PhoneBook phoneBook, string fileName)
        {
            if (File.Exists(fileName))
            {
                StreamWriter sw = File.CreateText(fileName);
                foreach(Contact ct in phoneBook.list)
                {
                    sw.WriteLine($"{ct.Name};{ct.Phone}");
                }
                sw.Close();

            }
            else
            {
                MessageBox.Show("Файл не найден", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
    }
}