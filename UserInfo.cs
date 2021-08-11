using FTPServ.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTPServ
{
    public partial class UserInfo : Form
    {
        public UserInfo()
        {
            InitializeComponent();
        }

        private void init()
        {
            foreach(string username in User.users.Keys)
            {
                User u = User.users[username];
                dataGridView1.Rows.Add(u.userName, u.password, u.workDir);
            }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            User u = new User();
            object tmp = e.Row.Cells[0].Value;
            if (tmp == null)
                return;
            u.userName = tmp.ToString();
            tmp = e.Row.Cells[1].Value;
            if (tmp == null)
                return;
            u.password = tmp.ToString();
            tmp = e.Row.Cells[2].Value;
            if (tmp == null)
                return;
            u.workDir = tmp.ToString();
            User.InsertUser(u);
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            User.DeleteUser(e.Row.Cells[0].Value.ToString());
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            User u = new User();
            object tmp = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            if (tmp == null)
                return;
            u.userName = tmp.ToString();
            tmp = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
            if (tmp == null)
                return;
            u.password = tmp.ToString();
            tmp = dataGridView1.Rows[e.RowIndex].Cells[2].Value;
            if (tmp == null)
                return;
            u.workDir = tmp.ToString();
            User.UpdateUser(u);
        }

        private void UserInfo_Load(object sender, EventArgs e)
        {
            init();
        }
    }
}
