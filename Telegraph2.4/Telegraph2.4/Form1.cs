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
namespace Telegraph2._4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int check = 0;
        string StrRecieve;
        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;

            if (check == 0)
            {
                MessageBox.Show("ERROR 8: Check Your connection.", "Something went wrong");
                return;
            }

            if (text.Length == 0 || text.Length > 3)
            {
                MessageBox.Show("ERROR 0: You cannot enter this value.", "Something went wrong");
                return;
            }

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] < 48 || text[i] > 57)
                {
                    MessageBox.Show("ERROR 1: You cannot enter this value.", "Something went wrong");
                    return;
                }
            }

            if (Convert.ToInt16(text) < 0 || Convert.ToInt16(text) > 125)
            {
                MessageBox.Show("ERROR 2: The value must be between 0 and 125.", "Something went wrong");
                return;
            }

            serialPort1.WriteLine("@" + textBox1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serialPort1.BaudRate = 115200;
            serialPort1.DataBits = 8;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
            String[] ports = SerialPort.GetPortNames();

            button6.Enabled = false;

            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.IndexOf("^") != -1 || textBox3.Text.IndexOf("@") != -1 || textBox3.Text.IndexOf("#") != -1)
            {
                MessageBox.Show("ERROR 8: The text contains invalid characters. ^#@", "Something went wrong");
                return;
            }
            if (textBox3.Text.Length < 1)
            {
                MessageBox.Show("ERROR 1: You cannot enter this value.", "Something went wrong");
                return;
            }
            if (check == 0)
            {
                MessageBox.Show("ERROR 8: Check Your connection.", "Something went wrong");
                return;
            }

            serialPort1.WriteLine("^" + textBox3.Text.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                String portesr = comboBox1.Text.ToString();
                serialPort1.PortName = portesr;
                check = 1;
                serialPort1.Open();
                button6.Enabled = true;
                button3.Enabled = false;
            }
            else
            {
                MessageBox.Show("ERROR 8: Check Your connection.", "Something went wrong");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text2 = textBox2.Text;

            if (check == 0)
            {
                MessageBox.Show("ERROR 8: Check Your connection.", "Something went wrong");
                return;
            }

            if (text2.Length != 5)
            {
                MessageBox.Show("ERROR 0: You cannot enter this value.", "Something went wrong");
                return;
            }
            else
            {
                for (int j = 0; j < text2.Length; j++)
                {
                    if (text2[j] < 48 || text2[j] > 57)
                    {
                        MessageBox.Show("ERROR 1: You cannot enter this value.", "Something went wrong");
                        return;
                    }
                }
            }
            serialPort1.WriteLine("#" + textBox2.Text);
        }

        private void DisplayText(object sender, EventArgs e)
        {
            //textBox4.AppendText(StrRecieve);
            textBox4.Text = StrRecieve;
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            StrRecieve = serialPort1.ReadExisting();
            this.Invoke(new EventHandler(DisplayText));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                serialPort1.Close();
                button6.Enabled = false;
                button3.Enabled = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
        }
    }
}
