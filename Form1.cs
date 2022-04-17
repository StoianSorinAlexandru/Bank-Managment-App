using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace ProiectBRC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ok = 0;
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT CNP,token,nume,prenume FROM persoana", sqc);
            MySqlDataReader read = cmd.ExecuteReader();

            int token = 0, tokenDb = -1;
            while (read.Read())
            {
                ///MessageBox.Show(token.ToString() + tokenDb.ToString());
                try
                {
                    token = Convert.ToInt32(textBox2.Text);
                    tokenDb = Convert.ToInt32(read[1].ToString());
                }
                catch
                {

                }
                if (read[0].ToString() == textBox1.Text && token == tokenDb)
                {
                    ok = 1;
                    if (read[1].ToString().Length == 4)
                    {
                        FormClient frm = new FormClient(textBox1.Text);
                        frm.Show();
                        this.Hide();
                    }
                    else if (read[1].ToString().Length == 5) //angajat
                    {
                        Form2 frm = new Form2(read[2].ToString(), read[3].ToString(), read[0].ToString());
                        frm.Show();
                        this.Hide();
                    }
                    else if (read[1].ToString().Length == 6) //admin
                    {
                        FormMain frm = new FormMain(read[0].ToString());
                        frm.Show();
                        this.Hide();
                    }
                }
            }
            read.Close();
            sqc.Close();

            if (ok == 0)
                MessageBox.Show("Logare invalida!");

            //Form2 frm = new Form2();
            //frm.Show();
            //this.Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Doriti sa iesiti din aplicatie?", "Mesaj", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
