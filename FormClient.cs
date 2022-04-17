using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.Types;
using MySql.Data.MySqlClient;

namespace ProiectBRC
{
    public partial class FormClient : Form
    {
        public string ContActual;
        public string SumaActuala;
        public string CNP;
        public string idPersoana;
        public string idClient;


        public void GetInfo()
        {
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";
            sqc.Open();


            MySqlCommand cmd1 =  new MySqlCommand(String.Format("select idPersoana from persoana where CNP = '{0}'", CNP), sqc);
            MySqlDataReader read = cmd1.ExecuteReader();

            while (read.Read())
            {
                idPersoana = read[0].ToString();
            }

            read.Close();

            MySqlCommand cmd2 = new MySqlCommand(String.Format("select idClient from client where CNP = '{0}'", CNP), sqc);
            read = cmd2.ExecuteReader();

            while (read.Read())
            {
                idClient = read[0].ToString();
            }

            read.Close();
            sqc.Close();
        }

        public FormClient(string CNP)
        {
            InitializeComponent();
            this.CNP = CNP;
            GetInfo();
            ContActual = "Neselectat";
            SumaActuala = "Neselectat";
            label9.Text = ContActual;
            label14.Text = SumaActuala;

            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();
            MySqlCommand cmd = new MySqlCommand(String.Format("SELECT * FROM persoana WHERE idPersoana = {0}", idPersoana), sqc);
            MySqlDataReader read = cmd.ExecuteReader();

            while (read.Read())
            {
                //dt.Rows.Add(read[0], read[1], read[2], read[3], read[4], read[5]);
                textBox1.Text = (read[1].ToString());
                textBox2.Text = (read[2].ToString());
                textBox3.Text = (read[3].ToString());
                textBox4.Text = (read[4].ToString());
                textBox5.Text = (read[5].ToString());
                textBox6.Text = (read[6].ToString());

                //MessageBox.Show((string)read[0]);

            }
            read.Close();
            sqc.Close();

        }

       

        private void tabControl1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
            {
                
                MySqlConnection sqc = new MySqlConnection();
                sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

                sqc.Open();
                MySqlCommand cmd = new MySqlCommand(String.Format("SELECT * FROM persoana WHERE idPersoana = {0}", idPersoana), sqc);
                MySqlDataReader read = cmd.ExecuteReader();

                /*DataTable dt = new DataTable();

                dt.Columns.Add("Id Client");
                dt.Columns.Add("CNP");
                dt.Columns.Add("Data nasterii");
                dt.Columns.Add("Sursa venit");
                dt.Columns.Add("Tranzactii online");
                dt.Columns.Add("Numar conturi");*/



                while (read.Read())
                {
                    //dt.Rows.Add(read[0], read[1], read[2], read[3], read[4], read[5]);
                    textBox1.Text = (read[1].ToString());
                    textBox2.Text = (read[2].ToString());
                    textBox3.Text = (read[3].ToString());
                    textBox4.Text = (read[4].ToString());
                    textBox5.Text = (read[5].ToString());
                    textBox6.Text = (read[6].ToString());

                    //MessageBox.Show((string)read[0]);

                }
                read.Close();
                sqc.Close();

                //dataGridView2.DataSource = dt;
            }

            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
            {
                MySqlConnection sqc = new MySqlConnection();
                sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

                sqc.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT detalii FROM notificari WHERE idPersoana = @idPersoana AND statusNotificare = 0", sqc);
                cmd.Parameters.AddWithValue("@idPersoana", idPersoana);
                MySqlDataReader read = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Columns.Add("Detalii Notificari");

                while (read.Read())
                {
                    dt.Rows.Add(read[0]);
                }

                read.Close();
                sqc.Close();

                dataGridView1.DataSource = dt;

            }

            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage4"])
            {
                MySqlConnection sqc = new MySqlConnection();
                sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";
                sqc.Open();

                MySqlCommand cmd1 = new MySqlCommand(String.Format("select idDepozit, tip_depozit, dobanda, numerar from depozit where idClient = {0}", idClient.ToString()), sqc);
                MySqlDataReader read = cmd1.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Columns.Add("ID Depozit");
                dt.Columns.Add("Tip Depozit");
                dt.Columns.Add("Dobanda");
                dt.Columns.Add("Numerar");

                while (read.Read())
                {
                    dt.Rows.Add(read[0].ToString(), read[1].ToString(), read[2].ToString(), read[3].ToString());
                }

                dataGridView6.DataSource = dt;

                read.Close();
                sqc.Close();
            }

            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage6"])
            {
                MySqlConnection sqc = new MySqlConnection();
                sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";
                sqc.Open();
                MySqlCommand cmd = new MySqlCommand(String.Format("select IBAN, tip_cont, numerar from cont where idClient = {0}", idClient), sqc);
                MySqlDataReader read = cmd.ExecuteReader();

                textBox7.Text = "Introducere IBAN";

                DataTable dt = new DataTable();
                dt.Columns.Add("IBAN");
                dt.Columns.Add("Tip Cont");
                dt.Columns.Add("Sold");

                while (read.Read())
                {
                    dt.Rows.Add(read[0], read[1], read[2]);
                }

                dataGridView2.DataSource = dt;

                read.Close();
                sqc.Close();

            }

            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage7"])
            {
                MySqlConnection sqc = new MySqlConnection();
                sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";
                sqc.Open();

                MySqlCommand cmd = new MySqlCommand(String.Format("select distinct IBAN_sursa, IBAN_destinatar, valoare, status_tran from tranzactie " +
                    "where IBAN_sursa = '{0}' or IBAN_destinatar = '{1}'", ContActual, ContActual), sqc);
                MySqlDataReader read = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Columns.Add("IBAN Sursa");
                dt.Columns.Add("IBAN Destinatar");
                dt.Columns.Add("Valoare");
                dt.Columns.Add("Status");

                while (read.Read())
                {
                    dt.Rows.Add(read[0], read[1], read[2], read[3]);
                }

                dataGridView3.DataSource = dt;

                read.Close();
                sqc.Close();

            }

            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage8"])
            {
                MySqlConnection sqc = new MySqlConnection();
                sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";
                sqc.Open();

                MySqlCommand cmd = new MySqlCommand(String.Format("select IBAN, tip_cont from cont where idClient = {0} and tip_cont = 'curent'", idClient), sqc);
                MySqlDataReader read = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Columns.Add("IBAN");
                dt.Columns.Add("Tip Cont");
               
                while (read.Read())
                {
                    dt.Rows.Add(read[0], read[1]);
                }

                dataGridView4.DataSource = dt;

                read.Close();
                sqc.Close();
            }

            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage9"])
            {
                MySqlConnection sqc = new MySqlConnection();
                sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";
                sqc.Open();

                MySqlCommand cmd = new MySqlCommand(String.Format("select idFactura, furnizor, numerar from facturi where idClient = {0}", idClient), sqc);
                MySqlDataReader read = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Columns.Add("ID Factura");
                dt.Columns.Add("Furnizor");
                dt.Columns.Add("Numerar");

                while (read.Read())
                {
                    dt.Rows.Add(read[0].ToString(), read[1].ToString(), read[2].ToString());
                }

                dataGridView5.DataSource = dt;

                read.Close();
                sqc.Close();

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();
            MySqlCommand cmd = new MySqlCommand(String.Format("update persoana set nume = '{0}', prenume = '{1}', adresa = '{2}', numar_telefon = '{3}', email = '{4}' where idPersoana = {5}"
                , textBox2.Text,
                textBox3.Text,
                textBox4.Text,
                textBox5.Text,
                textBox6.Text, idPersoana), sqc);

            cmd.ExecuteNonQuery();


            MySqlCommand cmd2 = new MySqlCommand(String.Format("insert into notificari(idPersoana, detalii, statusNotificare)" +
                " values ({1}, '{0}', 0)", "Modificare a datelor personale", idPersoana), sqc);
            cmd2.ExecuteNonQuery();


            MessageBox.Show("Actualizare reusita!");

            sqc.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";
            sqc.Open();

            MySqlCommand cmd1 = new MySqlCommand(String.Format("update notificari set statusNotificare = 1 where idPersoana = {0} and statusNotificare = 0", idPersoana), sqc);
            cmd1.ExecuteNonQuery();

            MySqlCommand cmd2 = new MySqlCommand(String.Format("SELECT detalii FROM notificari WHERE idPersoana = {0} AND statusNotificare = 0", idPersoana), sqc);
            MySqlDataReader read = cmd2.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Columns.Add("Detalii Notificari");

            while (read.Read())
            {
                dt.Rows.Add(read[0]);
            }

            read.Close();
            sqc.Close();

            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int ok = 0;
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";
            sqc.Open();

            MySqlCommand cmd1 = new MySqlCommand(String.Format("select count(*) from cont where idClient = {0} and IBAN = '{1}'", idClient, textBox7.Text), sqc);
            MySqlDataReader read = cmd1.ExecuteReader();

            while (read.Read())
            {
                if (read[0].ToString().Equals("0"))
                {
                    MessageBox.Show("IBAN Invalid!");
                    ok = 1;
                }

                else
                {
                    ContActual = textBox7.Text;
                }
            }

            read.Close();

            if (ok == 0)
            {
                MySqlCommand cmd2 = new MySqlCommand(String.Format("select numerar from cont where idClient = {0} and IBAN = '{1}'", idClient, textBox7.Text), sqc);
                MySqlDataReader read1 = cmd2.ExecuteReader();

                while (read1.Read())
                {
                    label14.Text = read1[0].ToString();
                }

                read1.Close();

            }

            label9.Text = ContActual;

           
            sqc.Close();
        }

        private void FormClient_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int ok = 1;

            if (ContActual.Equals("Neselectat"))
            {
                MessageBox.Show("Selecteaza un cont!");
                return;
            }

            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";
            sqc.Open();

            MySqlCommand cmd1 = new MySqlCommand(String.Format("select count(*) from cont where IBAN = '{0}' and tip_cont = 'curent'", textBox8.Text), sqc);
            MySqlDataReader read1 = cmd1.ExecuteReader();

            while (read1.Read())
            {
                if (read1[0].ToString().Equals("0"))
                {
                    MessageBox.Show("IBAN Invalid");
                    ok = 0;
                }
            }

            read1.Close();

            if (ok == 1)
            {
                int c1 = 1;
                MySqlCommand cmd2 = new MySqlCommand(String.Format("select numerar from cont where IBAN = '{0}'", ContActual), sqc);
                read1 = cmd2.ExecuteReader();

                while (read1.Read())
                {
                    if (Convert.ToDouble(read1[0]) < Convert.ToDouble(textBox9.Text))
                    {
                        c1 = 0;
                        ok = 0;
                    }
                }

                read1.Close();

                if (c1 == 0)
                {
                    MessageBox.Show("Sold Insuficient");
                    MySqlCommand cmd4 = new MySqlCommand(String.Format("insert into tranzactie(IBAN_sursa, IBAN_destinatar, valoare, status_tran) values " +
                   "('{0}', '{1}', {2}, 'ERROR')", ContActual, textBox8.Text, textBox9.Text), sqc);
                    cmd4.ExecuteNonQuery();
                }

                

                if (ok == 1)
                {
                    MessageBox.Show("Transferul este in asteptare!");
                    MySqlCommand cmd3 = new MySqlCommand(String.Format("insert into tranzactie(IBAN_sursa, IBAN_destinatar, valoare, status_tran) values " +
                        "('{0}', '{1}', {2}, 'PENDING')", ContActual, textBox8.Text, textBox9.Text), sqc);
                    cmd3.ExecuteNonQuery();

                    MySqlCommand cmd5 = new MySqlCommand(String.Format("insert into notificari(idPersoana, detalii, statusNotificare) values ({2}, 'In asteptare transfer catre {0} in valoare de {1}', 0)", textBox8.Text, textBox9.Text, idPersoana), sqc);
                    cmd5.ExecuteNonQuery();
                }

            }

            read1.Close();
            sqc.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string aux = null;
            string id_client = null;

            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";
            sqc.Open();

            MySqlCommand cmd1 = new MySqlCommand(String.Format("select count(*) from cont where IBAN = '{0}' and idClient = {1} and tip_cont = 'curent'", textBox10.Text, idClient), sqc);
            MySqlDataReader read = cmd1.ExecuteReader();

            while (read.Read())
            {
                if (read[0].ToString().Equals("0"))
                {
                    MessageBox.Show("IBAN Invalid!");
                    read.Close();
                    sqc.Close();
                    return;
                }
            }

            read.Close();

            MySqlCommand cmd2 = new MySqlCommand(String.Format("select idCont from cont where IBAN = '{0}' and tip_cont = 'curent'", textBox10.Text), sqc);
            read = cmd2.ExecuteReader();

            while (read.Read())
            {
                aux = read[0].ToString();
            }

            read.Close();

            MySqlCommand cmd3 = new MySqlCommand(String.Format("select count(*) from card where idCont = {0}", aux), sqc);
            read = cmd3.ExecuteReader();

            while (read.Read())
            {
                if (!read[0].ToString().Equals("0"))
                {
                    MessageBox.Show("Cardul exista sau este in curs de validare!");
                    read.Close();
                    sqc.Close();
                    return;
                }
            }

            read.Close();

            MySqlCommand cmd4 = new MySqlCommand(String.Format("insert into card(stare_card, idCont) values ('PENDING', {0})", aux), sqc);
            cmd4.ExecuteNonQuery();

            MySqlCommand cmd5 = new MySqlCommand(String.Format("select idClient from cont where IBAN = '{0}'", textBox10.Text), sqc);
            read = cmd5.ExecuteReader();

            while (read.Read())
            {
                id_client = read[0].ToString();
            }

            read.Close();

            MySqlCommand cmd6 = new MySqlCommand(String.Format("insert into notificari(idPersoana, detalii, statusNotificare) values({0}, 'Solicitare card depusa pentru contul " +
                "{1}', 0)", id_client, textBox10.Text), sqc);
            cmd6.ExecuteNonQuery();

            MessageBox.Show("Solicitarea pentru card a fost depusa!");

            sqc.Close();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";
            sqc.Open();

            MySqlCommand cmd1 = new MySqlCommand(String.Format("select count(*) from cont where IBAN = '{0}' and idClient = {1} and tip_cont = 'facturi'", ContActual, idClient), sqc);
            MySqlDataReader read = cmd1.ExecuteReader();

            while (read.Read())
            {
                if (read[0].ToString().Equals("0"))
                {
                    MessageBox.Show("Cont incorect!");
                    sqc.Close();
                    return;
                }
            }

            read.Close();

            MySqlCommand cmd2 = new MySqlCommand(String.Format("select count(*) from facturi where idFactura = {0} and idClient = {1}", textBox12.Text, idClient), sqc);
            read = cmd2.ExecuteReader();

            while (read.Read())
            {
                if (read[0].ToString().Equals("0"))
                {
                    MessageBox.Show("Factura inexistenta!");
                    sqc.Close();
                    return;
                }
            }

            read.Close();

            MySqlCommand cmd3 = new MySqlCommand(String.Format("select numerar from cont where IBAN = '{0}' and idClient = {1}", ContActual, idClient), sqc);
            read = cmd3.ExecuteReader();
            double valoare_cont = 0;

            while (read.Read())
            {
                valoare_cont = Convert.ToDouble(read[0]);
            }

            read.Close();

            MySqlCommand cmd4 = new MySqlCommand(String.Format("select numerar from facturi where idFactura = {0}", textBox12.Text), sqc);
            read = cmd4.ExecuteReader();
            double valoare_factura = 0;

            while (read.Read())
            {
                valoare_factura = Convert.ToDouble(read[0]);
            }

            read.Close();

            if (valoare_cont < valoare_factura)
            {
                sqc.Close();
                MessageBox.Show("Sold insuficient!");
                return;
            }


            MySqlCommand cmd5 = new MySqlCommand(String.Format("delete from facturi where idFactura = {0}", textBox12.Text), sqc);
            cmd5.ExecuteNonQuery();

            MySqlCommand cmd6 = new MySqlCommand("update cont set numerar = @numerar where IBAN = @iban", sqc);
            cmd6.Parameters.AddWithValue("@numerar", valoare_cont - valoare_factura);
            cmd6.Parameters.AddWithValue("@iban", ContActual);
            cmd6.ExecuteNonQuery();

            MySqlCommand cmd7 = new MySqlCommand(String.Format("insert into notificari(idPersoana, detalii, statusNotificare) values ({0}, 'S-a platit factura cu id: {1} in valoare de {2}', 0)", idPersoana, textBox12.Text, valoare_factura.ToString()), sqc);
            cmd7.ExecuteNonQuery();

            MessageBox.Show("FACTURA A FOST PLATITA!");

            sqc.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";
            sqc.Open();

            MySqlCommand cmd1 = new MySqlCommand(String.Format("select count(*) from cont where idClient = {0} and IBAN = '{1}' and tip_cont = 'curent'", idClient, ContActual), sqc);
            MySqlDataReader read = cmd1.ExecuteReader();

            while (read.Read())
            {
                if (read[0].ToString().Equals("0"))
                {
                    MessageBox.Show("Cont Invalid");
                    sqc.Close();
                    return;
                }
            }

            read.Close();

            MySqlCommand cmd2 = new MySqlCommand(String.Format("select numerar from cont where idClient = {0} and IBAN = '{1}' and tip_cont = 'curent'", idClient, ContActual), sqc);
            read = cmd2.ExecuteReader();
            double valoare_cont = 0;
            double valoare_depozit = Convert.ToDouble(textBox11.Text);

            while (read.Read())
            {
                valoare_cont = Convert.ToDouble(read[0].ToString());
            }

            read.Close();

            if (valoare_cont < valoare_depozit)
            {
                sqc.Close();
                MessageBox.Show("Sold Insuficient!");
                return;
            }

            /*
             * 
             * MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = sqc;

            sqc.Open();
            
            if(listBox3.SelectedItem.ToString() == "1")
            {
                cmd.CommandText = "Select dobanda from tip_depozit where type_depozit=@tip";
                cmd.Parameters.AddWithValue("@tip", 1);
                tipdepozit = 1;
            }
            else if(listBox3.SelectedItem.ToString() == "3")
            {
                cmd.CommandText = "Select dobanda from tip_depozit where type_depozit=@tip";
                cmd.Parameters.AddWithValue("@tip", 3);
                tipdepozit = 3;
            }
            else if(listBox3.SelectedItem.ToString() == "6")
            {
                cmd.CommandText = "Select dobanda from tip_depozit where type_depozit=@tip";
                cmd.Parameters.AddWithValue("@tip", 6);
                tipdepozit = 6;
            }
            MySqlDataReader read = cmd.ExecuteReader();
            */

            double valoare_dobanda = 0;
            int tipdepozit = 0;

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = sqc;

            if (listBox1.SelectedItem.ToString() == "1")
            {
                cmd.CommandText = "Select dobanda from tip_depozit where type_depozit=@tip";
                cmd.Parameters.AddWithValue("@tip", 1);
                 tipdepozit = 1;
            }
            else if (listBox1.SelectedItem.ToString() == "3")
            {
                cmd.CommandText = "Select dobanda from tip_depozit where type_depozit=@tip";
                cmd.Parameters.AddWithValue("@tip", 3);
                tipdepozit = 3;
            }
            else if (listBox1.SelectedItem.ToString() == "6")
            {
                cmd.CommandText = "Select dobanda from tip_depozit where type_depozit=@tip";
                cmd.Parameters.AddWithValue("@tip", 6);
                tipdepozit = 6;
            }

            read = cmd.ExecuteReader();
            while (read.Read())
            {
                valoare_dobanda = Convert.ToDouble(read[0].ToString());
            }

            read.Close();

            

            MySqlCommand cmd3 = new MySqlCommand("update cont set numerar = @numerar where IBAN = @iban", sqc);
            cmd3.Parameters.AddWithValue("@numerar", valoare_cont - valoare_depozit);
            cmd3.Parameters.AddWithValue("@iban", ContActual);
            cmd3.ExecuteNonQuery();

            valoare_depozit *= (1 + valoare_dobanda);

            MySqlCommand cmd4 = new MySqlCommand(String.Format("insert into notificari(idPersoana, detalii, statusNotificare) values ({0}, 'S-a creat depozit in valoare de {1}', 0)", idPersoana, valoare_depozit.ToString()), sqc);
            cmd4.ExecuteNonQuery();

            MySqlCommand cmd5 = new MySqlCommand("insert into depozit(idClient, tip_depozit, dobanda, numerar) values (@id,@tip,@valdob,@valdep)", sqc);
            cmd5.Parameters.AddWithValue("@id", idClient);
            cmd5.Parameters.AddWithValue("@tip", tipdepozit);
            cmd5.Parameters.AddWithValue("@valdob", valoare_dobanda);
            cmd5.Parameters.AddWithValue("@valdep", valoare_depozit);
            //idClient,  tipdepozit.ToString(), valoare_dobanda, valoare_depozit
            cmd5.ExecuteNonQuery();

            MessageBox.Show("Depozitul a fost creat!");

            sqc.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";
            sqc.Open();

            MySqlCommand cmd1 = new MySqlCommand(String.Format("select count(*) from depozit where idClient = {0} and idDepozit = {1}", idClient, textBox13.Text), sqc);
            MySqlDataReader read = cmd1.ExecuteReader();

            while (read.Read())
            {
                if (read[0].ToString().Equals("0"))
                {
                    MessageBox.Show("Depozit Inexistent!");
                    sqc.Close();
                    return;
                }
            }

            read.Close();

            MySqlCommand cmd2 = new MySqlCommand(String.Format("select numerar, tip_depozit from depozit where idClient = {0} and idDepozit = {1}", idClient, textBox13.Text), sqc);
            read = cmd2.ExecuteReader();

            double valoare_depozit = 0;
            double valoare_dobanda = 0.02;
            int tip_depozit = 0;

            while (read.Read())
            {
                valoare_depozit = Convert.ToDouble(read[0].ToString());
                tip_depozit = Convert.ToInt32(read[1].ToString());
            }

            read.Close();

            MySqlCommand cmd3 = new MySqlCommand(String.Format("select tip_cont from cont where IBAN = '{0}'", ContActual), sqc);
            read = cmd3.ExecuteReader();

            while (read.Read())
            {
                if (!read[0].ToString().Equals("curent"))
                {
                    MessageBox.Show("Cont incorect");
                    sqc.Close();
                    return;

                }
            }

            read.Close();

            MySqlCommand cmd4 = new MySqlCommand(String.Format("select numerar from cont where IBAN = '{0}'", ContActual), sqc);
            read = cmd4.ExecuteReader();

            double valoare_cont_curent = 0;

            while (read.Read())
            {
                valoare_cont_curent = Convert.ToDouble(read[0].ToString());
            }

            read.Close();

            if (valoare_depozit > 50000)
            {
                MessageBox.Show("Depozitele mai mari de 50000 necesita permisiunea unui angajat pentru a fi lichidate!");
                sqc.Close();
                return;
            }

            MySqlCommand cmd5 = new MySqlCommand("update cont set numerar = @numerar where IBAN = @iban", sqc);

            cmd5.Parameters.AddWithValue("@numerar", (valoare_cont_curent + valoare_depozit * (1 - valoare_dobanda)));
            cmd5.Parameters.AddWithValue("@iban", ContActual);
            cmd5.ExecuteNonQuery();

            /*
            MySqlCommand cmd5 = new MySqlCommand("insert into depozit(idClient, tip_depozit, dobanda, numerar) values (@id,@tip,@valdob,@valdep)", sqc);
            cmd5.Parameters.AddWithValue("@id", idClient);
            cmd5.Parameters.AddWithValue("@tip", idClient);
            cmd5.Parameters.AddWithValue("@valdob", idClient);
            cmd5.Parameters.AddWithValue("@valdep", idClient);
            */

            MySqlCommand cmd6 = new MySqlCommand(String.Format("insert into notificari(idPersoana, detalii, statusNotificare) values ({0}, 'Depozitul cu ID: {1} in valoare de {2} a fost lichidat', 0)",
                idPersoana, textBox13.Text, (valoare_depozit * (1 - valoare_dobanda)).ToString()), sqc);
            cmd6.ExecuteNonQuery();

            MySqlCommand cmd7 = new MySqlCommand(String.Format("delete from depozit where idDepozit = {0}", textBox13.Text), sqc);
            cmd7.ExecuteNonQuery();

            MessageBox.Show("Depozitul a fost lichidat!");

            sqc.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";
            sqc.Open();

            MySqlCommand cmd1 = new MySqlCommand(String.Format("select count(*) from cont where IBAN = '{0}'", ContActual), sqc);
            MySqlDataReader read = cmd1.ExecuteReader();


            while (read.Read())
            {
                if (read[0].ToString().Equals("0"))
                {
                    label14.Text = "Neselectat";
                    sqc.Close();
                    return;
                }
            }

            read.Close();

            MySqlCommand cmd2 = new MySqlCommand(String.Format("select numerar from cont where IBAN = '{0}'", ContActual), sqc);
            read = cmd2.ExecuteReader();

            while (read.Read())
            {
                label14.Text = read[0].ToString();
            }

            read.Close();

            sqc.Close();
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Doriti sa iesiti din cont?", "Mesaj", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Form1 frm = new Form1();
                    frm.Show();
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

        /*
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Doriti sa iesiti din cont?", "Mesaj", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Form1 frm = new Form1();
                    frm.Show();
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
        */

    }
}
