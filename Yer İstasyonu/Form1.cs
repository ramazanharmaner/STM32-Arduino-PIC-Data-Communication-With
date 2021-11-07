using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Yer_İstasyonu
{
    public partial class Form1 : Form
    {
        bool formTasiniyor = false;
        Point baslangicNoktasi = new Point(0, 0);
        public Form1()
        {
            InitializeComponent();
        }
        private string data;
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPort1_DataReceived);
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            data = serialPort1.ReadLine();
            this.Invoke(new EventHandler(displayData_event));
        }

        private void displayData_event(object sender, EventArgs e)
        {
            float a = float.Parse(data, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            decimal b = Decimal.Round(Convert.ToDecimal(a), 2);   
            add_Items4.Value = Convert.ToInt32(b);
            add_Items4.ForeColor = Color.Red;
            lblHeat.Text = b.ToString() + "°";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                    serialPort1.DataBits = Convert.ToInt32(comboBox3.Text);
                    serialPort1.Open();
                    lblConnect.Text = "Bağlı";
                    lblConnect.ForeColor = Color.Green;
                }
                else
                {
                    MessageBox.Show("Geçerli Bir Bağlantı Bulunmakta !", "Yer İstasyonu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Tüm Değerleri Eksiksiz Giriniz !", "RH - Yer İstasyonu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                serialPort1.Close();
                lblConnect.Text = "Bağlı Değil";
                lblConnect.ForeColor = Color.Red;
            }
            else
            {
                MessageBox.Show("Açık Bir Bağlantı Yok !", "Yer İstasyonu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
            Application.Exit();
        }

       

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (formTasiniyor)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.baslangicNoktasi.X, p.Y - this.baslangicNoktasi.Y);
            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
            Application.Exit();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            formTasiniyor = true;
            baslangicNoktasi = new Point(e.X, e.Y);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            formTasiniyor = false;
        }
    }
}
