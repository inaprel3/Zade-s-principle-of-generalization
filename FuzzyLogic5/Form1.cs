using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuzzyLogic2
{
    public partial class Form1 : Form
    {
        private float tempC1 = 0, tempC2 = 0, tempC3 = 0;
        private float Coord = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {  
            drawChart();
        }
        private void textBox1_TextChanged(object sender, EventArgs e) {/*a1*/}
        private void textBox2_TextChanged(object sender, EventArgs e) {/*a2*/}
        private void textBox3_TextChanged(object sender, EventArgs e) {/*a3*/}
        private void textBox4_TextChanged(object sender, EventArgs e) {/*b1*/}
        private void textBox5_TextChanged(object sender, EventArgs e) {/*b2*/}
        private void textBox6_TextChanged(object sender, EventArgs e) {/*b3*/}
        private void textBox7_TextChanged(object sender, EventArgs e) {/*X*/}
        private void clearText()
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox7.Enabled = false;
            button1.Enabled = false;
            pictureBox1.Image = null;
           
            labelAX.Text = null;
            labelBX.Text = null;
            labelCX.Text = null;
            
            textBox7.Text = null;
        }
        private bool checkData()
        {
            try
            {
                if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "" || textBox6.Text != "")
                {
                    float a = float.Parse(textBox1.Text), b = float.Parse(textBox2.Text), c = float.Parse(textBox3.Text);
                    float q = float.Parse(textBox4.Text), w = float.Parse(textBox5.Text), z = float.Parse(textBox6.Text);
                    if (a >= b || b >= c || q >= w || w >= z)
                    {
                        MessageBox.Show("Введіть коректні дані",
                               "Помилка",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
                        clearText();
                        return false;
                    }
                    else
                        return true;
                }
                else
                {
                    MessageBox.Show("Одно чи декілька полей пусті",
                              "Помилка",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
                    clearText();
                    return false;
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Невірний формат введених даних",
                                  "Помилка",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error,
                                  MessageBoxDefaultButton.Button1);
                clearText();
                return false;
            }
        }

        private bool checkX()
        {
            try
            {
                float x = float.Parse(textBox7.Text);
                if (textBox7.Text == "")
                {
                    MessageBox.Show("Одно чи декілька полей пусті",
                              "Помилка",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
                    textBox7.Text = null;
                    return false;
                }
                else
                    return true;
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Невірний формат введених даних",
                                  "Помилка",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error,
                                  MessageBoxDefaultButton.Button1);
                textBox7.Text = null;
                return false;
            }

        }
        private void integral_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 45 && e.KeyChar != 44)
                e.Handled = true;
        }

        private string calcF(float a, float b, float c, float q, float w, float z)
        {
            tempC1 = a + q;
            tempC2 = b + w;
            tempC3 = c + z;


            return tempC1 + " - " + tempC2 + " - " + tempC3;
        }

        private void button2_Click(object sender, EventArgs e)
        {/*Result*/
            if (checkData())
            {
                float a = float.Parse(textBox1.Text), b = float.Parse(textBox2.Text), c = float.Parse(textBox3.Text);
                float q = float.Parse(textBox4.Text), w = float.Parse(textBox5.Text), z = float.Parse(textBox6.Text);
                labelResultC.Text = calcF(a, b, c, q, w, z);
                textBox7.Enabled = true;
                button1.Enabled = true;
                draw();
                drawChart();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkX())
            {
                float x = float.Parse(textBox7.Text);
                Graphics g = pictureBox1.CreateGraphics();
                Font font = new Font(this.Font, FontStyle.Bold);
                SolidBrush CommentBrush = new SolidBrush(Color.Black);
                Pen Num4pen = new Pen(Color.DarkCyan, 4);
                Pen AxisDash = new Pen(Brushes.Black, 1);
                g.TranslateTransform(200, 150);
                g.DrawLine(Num4pen, x * Coord, -60, x * Coord, 0);
                g.DrawLine(AxisDash, x * Coord, 0, x * Coord, 120);
                g.DrawString(x.ToString(), font, CommentBrush, x * Coord, 20);

                float a = float.Parse(textBox1.Text), b = float.Parse(textBox2.Text), c = float.Parse(textBox3.Text);
                float q = float.Parse(textBox4.Text), w = float.Parse(textBox5.Text), z = float.Parse(textBox6.Text);

                if (x < a || x > c)
                { labelAX.Text = "0"; }
                else if (x >= a && x <= b)
                { labelAX.Text = (x - a) / (b - a) + ""; }
                else if (x >= b && x <= c)
                { labelAX.Text = (c - x) / (c - b) + ""; }

                if (x < q || x > z)
                { labelBX.Text = "0"; }
                else if (x >= q && x <= w)
                { labelBX.Text = (x - q) / (w - q) + ""; }
                else if (x >= w && x <= z)
                { labelBX.Text = (z - x) / (z - w) + ""; }

                if (x < tempC1 || x > tempC3)
                { labelCX.Text = "0"; }
                else if (x >= tempC1 && x <= tempC2)
                { labelCX.Text = (x - tempC1) / (tempC2 - tempC1) + ""; }
                else if (x >= tempC2 && x <= tempC3)
                { labelCX.Text = (tempC3 - x) / (tempC3 - tempC2) + ""; }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {/*Clear*/
            clearChart();
            labelResultC.Text = null;

            tempC1 = 0;
            tempC2 = 0;
            tempC3 = 0;
            Coord = 0;
        }

        private void draw()
        {
            {//малювання графiку
                if (checkData())
                {
                    Pen AxisPen = new Pen(Color.Black, 2);
                    AxisPen.EndCap = LineCap.ArrowAnchor;
                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(AxisPen, 0, 150, 560, 150);
                    g.DrawLine(AxisPen, 560, 150, 545, 155);
                    g.DrawLine(AxisPen, 560, 150, 545, 145);
                    g.TranslateTransform(200, 150);
                    Pen AxisDash = new Pen(Brushes.Black, 1);
                    AxisDash.DashStyle = DashStyle.Dash;
                    g.DrawLine(AxisDash, 0, -150, 0, 120);
                    Font font = new Font(this.Font, FontStyle.Bold);
                    SolidBrush CommentBrush = new SolidBrush(Color.Black);
                    g.DrawString("0", font, CommentBrush, 0, 100);

                    float a = float.Parse(textBox1.Text);
                    float b = float.Parse(textBox2.Text);
                    float c = float.Parse(textBox3.Text);
                    float q = float.Parse(textBox4.Text);
                    float w = float.Parse(textBox5.Text);
                    float z = float.Parse(textBox6.Text);
                    float[] arr = new float[6];
                    arr[0] = a;
                    arr[1] = b;
                    arr[2] = c;
                    arr[3] = q;
                    arr[4] = w;
                    arr[5] = z;
                    float min = 99999;
                    float max = 0;
                    for (int i = 0; i < 6; i++)
                    {
                        if (arr[i] > max) max = arr[i];
                        if (arr[i] < min) min = arr[i];
                    }
                    Coord = 520 / ((max - min) * 4);

                    Pen Num1pen = new Pen(Color.Green, 4);
                    Pen Num2pen = new Pen(Color.DarkGoldenrod, 4);
                    Pen Num3pen = new Pen(Color.DarkMagenta, 4);
                    g.DrawLine(Num1pen, a * Coord, 0, b * Coord, -60);
                    g.DrawLine(Num1pen, b * Coord, -60, c * Coord, 0);
                    g.DrawLine(Num2pen, q * Coord, 0, w * Coord, -60);
                    g.DrawLine(Num2pen, w * Coord, -60, z * Coord, 0);
                    g.DrawLine(Num3pen, tempC1 * Coord, 0, tempC2 * Coord, -60);
                    g.DrawLine(Num3pen, tempC2 * Coord, -60, tempC3 * Coord, 0);

                    g.DrawLine(AxisDash, a * Coord, 0, a * Coord, 120);
                    g.DrawLine(AxisDash, b * Coord, -59, b * Coord, 120);
                    g.DrawLine(AxisDash, c * Coord, 0, c * Coord, 120);
                    g.DrawLine(AxisDash, q * Coord, 0, q * Coord, 120);
                    g.DrawLine(AxisDash, w * Coord, -59, w * Coord, 120);
                    g.DrawLine(AxisDash, z * Coord, 0, z * Coord, 120);
                    g.DrawLine(AxisDash, tempC1 * Coord, 0, tempC1 * Coord, 120);
                    g.DrawLine(AxisDash, tempC2 * Coord, -59, tempC2 * Coord, 120);
                    g.DrawLine(AxisDash, tempC3 * Coord, 0, tempC3 * Coord, 120);

                    g.DrawLine(AxisDash, -200, -60, 360, -60);

                    g.DrawString(a.ToString(), font, CommentBrush, a * Coord, 5);
                    g.DrawString(b.ToString(), font, CommentBrush, b * Coord + 5, 80);
                    g.DrawString(c.ToString(), font, CommentBrush, c * Coord, 5);
                    g.DrawString(q.ToString(), font, CommentBrush, q * Coord, 5);
                    g.DrawString(w.ToString(), font, CommentBrush, w * Coord + 5, 60);
                    g.DrawString(z.ToString(), font, CommentBrush, z * Coord, 5);
                    g.DrawString(tempC1.ToString(), font, CommentBrush, tempC1 * Coord, 5);
                    g.DrawString(tempC2.ToString(), font, CommentBrush, tempC2 * Coord + 5, 40);
                    g.DrawString(tempC3.ToString(), font, CommentBrush, tempC3 * Coord, 5);
                }
            }
        }

        void clearChart()
        {
            chart1.Series["line1"].Points.Clear();
            chart1.Series["line2"].Points.Clear();
            chart1.Refresh();
        }
        void drawChart()
        {
            int stepAmountMin = 20;
            float stepSize, stepAmountA, stepAmountB;
            if (checkData())
            {
                float a = float.Parse(textBox1.Text);
                float b = float.Parse(textBox2.Text);
                float c = float.Parse(textBox3.Text);
                float q = float.Parse(textBox4.Text);
                float w = float.Parse(textBox5.Text);
                float z = float.Parse(textBox6.Text);

                stepSize = (c - a) < (z - q) ? (c-a)/stepAmountMin : (z-q)/stepAmountMin;
                stepAmountA = (c - a) / stepSize;
                stepAmountB = (z - q) / stepSize;

                float[] setCX = new float[(int)((c - a) * (z - q))];
                float[] setCY = new float[(int)((c - a) * (z - q))];
                float[] setAX = new float[(int)stepAmountA+1];
                float[] setAY = new float[(int)stepAmountA+1];
                float[] setBX = new float[(int)stepAmountB + 1];
                float[] setBY = new float[(int)stepAmountB + 1];
                int v = 0;

                for(float x = a; v<setAX.Length; setAX[v] = x, x+=stepSize, v++)
                {
                    if (x <= a || x >= c)
                    {
                        setAY[v] = 0;
                    }
                    else if (a < x && x <= b)
                    {
                        setAY[v] = (x - a) / (b - a);
                    }
                    else if (b <= x && x < c)
                    {
                        setAY[v] = (c - x) / (c - b);
                    }
                    chart1.Series["line1"].Points.AddXY(setAX[v], setAY[v]);
                }
                v = 0;
                for (float x = q; v < setBY.Length; setBX[v] = x, x += stepSize, v++)
                {
                    if (x <= q || x >= z)
                    {
                        setBY[v] = 0;
                    }
                    else if (q <= x && x <= w)
                    {
                        setBY[v] = (x - q) / (w - q);
                    }
                    else if (w <= x && x < z)
                    {
                        setBY[v] = (z - x) / (z - w);
                    }
                    chart1.Series["line2"].Points.AddXY(setBX[v], setBY[v]);
                }
            }
        }
    }
}