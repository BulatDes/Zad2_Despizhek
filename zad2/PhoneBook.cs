using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics.Contracts;

namespace zad2
{
    internal class PhoneBook
    {
        public DataTable table = new DataTable();
        public List<Contact> list = new List<Contact>();

        public void AddContact (string name, string phone)
        {
            table.Rows.Add(name, phone);
        }

        public void ReloadList ()
        {
            foreach (DataRow value in table.Rows)
            {
                Contact contact = new Contact();
                contact.Name = (string) value[0];
                contact.Phone = (string) value[1];
                list.Add(contact);
            }
        }
        public void ReloadTable()
        {
            table.Clear();
            /*table.Columns.Add("Name");
            table.Columns.Add("Telephone");*/
            foreach(Contact ct in list)
            {
                AddContact(ct.Name, ct.Phone);
            }
        }
        public void RemoveContact(string name)
        {
            Contact contactToRemove = list.FirstOrDefault(c => c.Name == name);
            if (contactToRemove != null) { list.Remove(contactToRemove); }
            ReloadTable();
        }
    }
}