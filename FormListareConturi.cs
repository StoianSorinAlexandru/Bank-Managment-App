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
    public partial class FormListareConturi : Form
    {

        String CNP;
        String CNP2;

        public FormListareConturi(String CNP, String CNP2)
        {
            this.CNP = CNP;
            this.CNP2 = CNP2;
            InitializeComponent();
        }

        private void FormListareConturi_Load(object sender, EventArgs e)
        {
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT persoana.CNP, persoana.nume, persoana.prenume FROM persoana WHERE persoana.CNP = " + '"' + this.CNP + '"' + ";", sqc);
            MySqlDataReader read = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            //MessageBox.Show("SELECT persoana.CNP, persoana.nume, persoana.prenume, persoana.numar_telefon, persoana.email, client.data_nasterii, client.sursa_venit, client.numar_conturi, persoana.adresa FROM client, persoana WHERE client.CNP = persoana.CNP and persoana.CNP = " + '"' + this.CNP + '"' + ";");
            // MessageBox.Show(this.CNP);
            while (read.Read())
            {
                textBox26.Text = (read[0].ToString());
                textBox27.Text = (read[1].ToString());
                textBox28.Text = (read[2].ToString());
            }
            read.Close();

            MySqlCommand cmd2 = new MySqlCommand("SELECT cont.IBAN, cont.numerar FROM persoana, client, cont WHERE cont.idClient = client.idClient and client.CNP = persoana.CNP and persoana.CNP = " + '"' + this.CNP + '"' + ";", sqc);
            MySqlDataReader read2 = cmd.ExecuteReader();


            dt.Load(read);


            read.Close();
            sqc.Close();

            dataGridView1.DataSource = dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormAdminUtil fr = new FormAdminUtil(this.CNP, this.CNP2);
            fr.Show();
            this.Hide();
        }
    }
}
