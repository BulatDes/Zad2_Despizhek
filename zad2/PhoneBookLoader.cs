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

                phoneBook.table.Columns.Add("Name");
                phoneBook.table.Columns.Add("Telephone");
                FileStream str = File.Open(fileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(str);
                do
                {
                    while (reader.Read())
                    {
                        phoneBook.AddContact(reader.GetString(0), reader.GetString(1));
                    }
                } while (reader.NextResult());
                phoneBook.table.Rows.RemoveAt(0);
                str.Close();
            }



             public void Save (PhoneBook phoneBook, string fileName)
            { }
        
    }
}