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
    public partial class FormAdminAngajat : Form
    {

        String CNP;
        String CNP2;

        public FormAdminAngajat(String CNP, String CNP2)
        {
            this.CNP = CNP;
            this.CNP2 = CNP2;

            InitializeComponent();
        }

        private void FormAdminAngajat_Load(object sender, EventArgs e)
        {
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT persoana.CNP, persoana.nume, persoana.prenume, persoana.numar_telefon, persoana.email, angajat.sucursala, angajat.departament, angajat.salariu, angajat.norma, persoana.adresa FROM angajat, persoana WHERE angajat.CNP = persoana.CNP ORDER BY persoana.nume, persoana.prenume;", sqc);
            MySqlDataReader read = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            while (read.Read())
            {
                textBox1.Text = (read[1].ToString());
                textBox2.Text = (read[2].ToString());
                textBox3.Text = (read[0].ToString());
                textBox4.Text = (read[4].ToString());
                textBox5.Text = (read[3].ToString());
                textBox6.Text = (read[5].ToString());
                textBox7.Text = (read[6].ToString());
                textBox8.Text = (read[9].ToString());
                textBox9.Text = (read[8].ToString());
                textBox10.Text = (read[7].ToString());
            }
            read.Close();
            sqc.Close();
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

            MySqlCommand cmd2 = new MySqlCommand(String.Format("update angajat set sucursala = '{0}', departament = '{1}', norma = '{2}', salariu = '{3}'  where CNP = " + '"' + this.CNP + '"'
                , textBox6.Text,
                textBox7.Text,
                textBox9.Text,
                textBox10.Text), sqc);


            cmd2.ExecuteNonQuery();

            string sqlQuerry = "insert into notificari (idPersoana, detalii, seen) values("  + "(Select idPersoana from persoana where CNP = " + '"' + this.CNP + '"' + " ), " + ", SCHIMBARE DATE PERSONALE, 0";

            ///MessageBox.Show(sqlQuerry);

            MySqlCommand cmd3 = new MySqlCommand(sqlQuerry, sqc);

            MessageBox.Show("Actualizare reusita!");
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

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();

            MySqlCommand cmd = new MySqlCommand("Select persoana.CNP, cont.numerar, angajat.salariu, cont.idClient from angajat, persoana, client, cont where angajat.CNP = persoana.CNP and persoana.CNP = client.CNP and cont.idCLient = client.idClient;", sqc);
            MySqlDataReader read = cmd.ExecuteReader();

            double suma = 0;
            string aux = null;
            while (read.Read())
            {
                //MessageBox.Show(read[0].ToString());
                aux = read[3].ToString();
                suma = Convert.ToDouble(read[1].ToString());
                suma = suma + Convert.ToDouble(read[2].ToString());
            }
            MySqlCommand cmd2 = new MySqlCommand(String.Format("update cont set numerar = '{0}' where idClient = " + '"' + aux + '"', suma),  sqc);
            read.Close();
            cmd2.ExecuteNonQuery();
            MessageBox.Show("Salariu platiti cu succes!!!");
            sqc.Close();

        }
    }
}
