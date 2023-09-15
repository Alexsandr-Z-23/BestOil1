using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace BestOil
{
    public partial class Form1 : Form
    {
        ToolStripLabel dateLabel;
        ToolStripLabel timeLabel;
        ToolStripLabel infoLabel;
        Timer timer;
        bool IsLabel11_TextChanged = false;
        Good fuel;
        Good[] food = new Good[4]
            {
                new Good { Name = "Чай", Price = 80 },
                new Good { Name = "Кофе", Price = 100 },
                new Good { Name = "Лимонад", Price = 60 },
                new Good { Name = "Булочка", Price = 55 }
            };
        double food_sum = 0;
        double proceeds = 0;
        string s;
        int[] qt = { 0, 0, 0, 0 };

        public Form1()
        {
            InitializeComponent();
            List<Good> fuels = new List<Good> 
            {
                new Good { Name="Бензин АИ-80 (А-76)", Price = 43.9},
                new Good { Name="Бензин АИ-92", Price = 46.56},
                new Good { Name="Бензин АИ-95", Price = 50.34},
                new Good { Name="Бензин АИ-98", Price = 61.7},
                new Good { Name="Pulsar-100", Price = 62.64},
                new Good { Name="Дизельное топливо", Price = 56.52},
                new Good { Name="Газ", Price = 19.24},
                new Good { Name="Газ (метан)", Price = 19.79},
                new Good { Name="Авиационное топливо", Price = 0}
            };
            comboBox1.DataSource = fuels;
            comboBox1.DisplayMember = "Name";
            comboBox1.SelectedIndex = 1;

            
            checkBox1.Text = food[0].Name;
            checkBox2.Text = food[1].Name;
            checkBox3.Text = food[2].Name;
            checkBox4.Text = food[3].Name;
            textBox4.Text = food[0].Price.ToString("n2");
            textBox6.Text = food[1].Price.ToString("n2");
            textBox8.Text = food[2].Price.ToString("n2");
            textBox10.Text = food[3].Price.ToString("n2");
            infoLabel = new ToolStripLabel();
            infoLabel.Text = "Текущие дата и время:";
            dateLabel = new ToolStripLabel();
            timeLabel = new ToolStripLabel();

            statusStrip1.Items.Add(infoLabel);
            statusStrip1.Items.Add(dateLabel);
            statusStrip1.Items.Add(timeLabel);

            timer = new Timer() { Interval = 1000 };
            timer.Tick += timer2_Tick;
            timer.Start();

        }

      
        private void ShowPrice_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                fuel = (Good)comboBox1.SelectedItem;
                textBox1.Text = fuel.Price.ToString("n2");
                if (radioButton1.Checked == true)
                {
                    textBox2_TextChanged(this, e);
                }
                else
                {
                    textBox3_TextChanged(this, e);
                }
                return;
            }
            else
            {
                textBox1.Text = "0";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.ReadOnly = true;
            textBox2.Text = "";
            textBox3.ReadOnly = false;
            groupBox5.Text = "К выдаче: ";
            label5.Text = "0,000";
            label6.Text = "л";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = true;
            textBox3.Text = "";
            groupBox5.Text = "К оплате: ";
            label5.Text = "0,00";
            label6.Text = "руб.";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                    label5.Text = (Convert.ToDouble(textBox1.Text) * Convert.ToDouble(textBox2.Text)).ToString("n2");
            }
            else
            {
                label5.Text = "0,00";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox3.Text != "" && Convert.ToDouble(textBox1.Text) != 0)
            {
                label5.Text = Math.Round((decimal)(Convert.ToDouble(textBox3.Text) / Convert.ToDouble(textBox1.Text)), 3).ToString("n3");
            }
            else
            {
                label5.Text = "0,00";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox5.ReadOnly = false;
                textBox5.Text = "1"; 
                textBox5.Focus();
            }
            else
            {
                textBox5.ReadOnly = true;
                textBox5.Text = "";
            }
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                textBox7.ReadOnly = false;
                textBox7.Text = "1"; 
                textBox7.Focus();
            }
            else
            {
                textBox7.ReadOnly = true;
                textBox7.Text = "";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                textBox9.ReadOnly = false;
                textBox9.Text = "1";
                textBox9.Focus();
            }
            else
            {
                textBox9.ReadOnly = true;
                textBox9.Text = "";
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                textBox11.ReadOnly = false;
                textBox11.Text = "1";
                textBox11.Focus();
            }
            else
            {
                textBox11.ReadOnly = true;
                textBox11.Text = "";
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            qt[0] = Convert.ToInt32(textBox5.Text!=""? textBox5.Text:"0");
            qt[1] = Convert.ToInt32(textBox7.Text != "" ? textBox7.Text : "0");
            qt[2] = Convert.ToInt32(textBox9.Text != "" ? textBox9.Text : "0");
            qt[3] = Convert.ToInt32(textBox11.Text != "" ? textBox11.Text : "0");
            food_sum = 0;
            for(int i=0;i<4;i++)
            {
                food_sum += qt[i]* food[i].Price;
            }
            label10.Text = food_sum.ToString("n2");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double d = 0;
            if (radioButton1.Checked == true) 
            {
                d = Convert.ToDouble(label5.Text) + Convert.ToDouble(label10.Text);
            }
            else
            {
                d = Convert.ToDouble(textBox3.Text) + Convert.ToDouble(label10.Text);
            }
            label11.Text = d.ToString("n2");
            proceeds += d;
            s = "Дневная выручка: " + string.Format("{0:f}", proceeds) + ".";
            if (timer1.Enabled == false)
            {
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            DialogResult result = MessageBox.Show($"Сумма к оплате: {label11.Text}. \nОчистить форму? ", "Ожидание следующего покупателя", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                timer1.Start();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(s, "Завершение работы", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.ToLongDateString();
            timeLabel.Text = DateTime.Now.ToLongTimeString();
        }

       
    }
}
