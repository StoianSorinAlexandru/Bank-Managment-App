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

    public partial class FormAdminUtil : Form
    {
        String CNP;
        String CNP2;
        public FormAdminUtil(String CNP, String CNP2)
        {
            this.CNP = CNP;
            this.CNP2 = CNP2; 
            //MessageBox.Show(this.CNP);

            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void FormAdminUtil_Load(object sender, EventArgs e)
        {
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT persoana.CNP, persoana.nume, persoana.prenume, persoana.numar_telefon, persoana.email, client.data_nasterii, client.sursa_venit, client.numar_conturi, persoana.adresa FROM client, persoana WHERE client.CNP = persoana.CNP and persoana.CNP = " + '"' + this.CNP +'"' + ";", sqc) ;
            MySqlDataReader read = cmd.ExecuteReader();

            DataTable dt = new DataTable();
                //MessageBox.Show("SELECT persoana.CNP, persoana.nume, persoana.prenume, persoana.numar_telefon, persoana.email, client.data_nasterii, client.sursa_venit, client.numar_conturi, persoana.adresa FROM client, persoana WHERE client.CNP = persoana.CNP and persoana.CNP = " + '"' + this.CNP + '"' + ";");
           // MessageBox.Show(this.CNP);
            while (read.Read())
            {
                textBox1.Text = (read[1].ToString());
                textBox2.Text = (read[2].ToString());
                textBox3.Text = (read[0].ToString());
                textBox4.Text = (read[4].ToString());
                textBox5.Text = (read[3].ToString());
                textBox6.Text = (read[5].ToString());
                textBox7.Text = (read[7].ToString());
                textBox8.Text = (read[8].ToString());
                textBox9.Text = (read[6].ToString());
            }
            read.Close();
            sqc.Close();
        }

        private void FormAdmin_FormClosing(object sender, FormClosingEventArgs e)
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

        

        private void button3_Click(object sender, EventArgs e)
        {
            FormMain fr = new FormMain(this.CNP2);
            fr.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();
            MySqlCommand cmd = new MySqlCommand(String.Format("update persoana set nume = '{0}', prenume = '{1}', CNP = '{2}', email = '{3}', numar_telefon = '{4}', adresa = '{5}'  where CNP = " + '"' + this.CNP + '"'
                , textBox1.Text,
                textBox2.Text,
                textBox3.Text,
                textBox4.Text,
                textBox5.Text,
                textBox8.Text), sqc);


            cmd.ExecuteNonQuery();

            MySqlCommand cmd2 = new MySqlCommand(String.Format("update client set numar_conturi = '{0}', sursa_venit = '{1}'  where CNP = " + '"' + this.CNP + '"'
                ,textBox7.Text,
                textBox9.Text), sqc);


            cmd2.ExecuteNonQuery();

            string sqlQuerry = "insert into notificari (idPersoana, detalii, seen) values(" + "(Select idPersoana from persoana where CNP = " + '"' + this.CNP + '"' + " ), " + ", SCHIMBARE DATE PERSONALE, 0";

            ///MessageBox.Show(sqlQuerry);

            MySqlCommand cmd3 = new MySqlCommand(sqlQuerry, sqc);

            MessageBox.Show("Actualizare reusita!");
            sqc.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormListareConturi fr = new FormListareConturi(this.CNP, this.CNP2);
            fr.Show();
            this.Hide();
        }
}
}
