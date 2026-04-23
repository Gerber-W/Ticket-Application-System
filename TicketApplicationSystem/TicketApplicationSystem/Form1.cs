using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicketApplicationSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            textBox2.KeyPress += NumericTextBox_KeyPress;
            textBox3.KeyPress += DecimalTextBox_KeyPress;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string passengerName = textBox1.Text.Trim();
            string gender = "";
            int age;
            decimal distance;
            decimal pricePerKm = 0;
            decimal totalPrice;
            decimal discount = 0;
            string category;
            string note = "No discount applied.";

            if (passengerName == "")
            {
                MessageBox.Show("Please enter passenger name.");
                textBox1.Focus();
                return;
            }

            if (radioButton1.Checked)
            {
                gender = "Male";
            }
            else if (radioButton2.Checked)
            {
                gender = "Female";
            }
            else
            {
                MessageBox.Show("Please select gender.");
                return;
            }

            if (!int.TryParse(textBox2.Text, out age) || age < 0)
            {
                MessageBox.Show("Please enter a valid age.");
                textBox2.Focus();
                return;
            }

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select category.");
                comboBox1.Focus();
                return;
            }

            category = comboBox1.SelectedItem.ToString();

            if (!decimal.TryParse(textBox3.Text, out distance) || distance <= 0)
            {
                MessageBox.Show("Please enter a valid distance.");
                textBox3.Focus();
                return;
            }

            if (category == "One")
            {
                pricePerKm = 20;
            }
            else if (category == "Two")
            {
                pricePerKm = 35;
            }
            else if (category == "Three")
            {
                pricePerKm = 50;
            }

            totalPrice = pricePerKm * distance;

            if (age < 12)
            {
                totalPrice = 0;
                note = "Ticket is free because passenger is under 12.";
            }
            else if (gender == "Female")
            {
                discount = totalPrice * 0.5m;
                totalPrice = totalPrice - discount;
                note = "50% discount applied for female passenger.";
            }

            string summary =
                "Ticket Summary" + Environment.NewLine +
                "------------------------------" + Environment.NewLine +
                "Passenger Name: " + passengerName + Environment.NewLine +
                "Gender: " + gender + Environment.NewLine +
                "Age: " + age + Environment.NewLine +
                "Category: " + category + Environment.NewLine +
                "Distance: " + distance.ToString("0.##") + " km" + Environment.NewLine +
                "Price Per Km: R" + pricePerKm.ToString("0.00") + Environment.NewLine +
                "Discount: R" + discount.ToString("0.00") + Environment.NewLine +
                "Final Price: R" + totalPrice.ToString("0.00") + Environment.NewLine +
                "Note: " + note;

            MessageBox.Show(summary, "Ticket Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboBox1.SelectedIndex = -1;
            textBox1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void DecimalTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox targetTextBox = sender as TextBox;
            string decimalSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            bool isDecimalSeparator = e.KeyChar.ToString() == decimalSeparator;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !isDecimalSeparator)
            {
                e.Handled = true;
                return;
            }

            if (isDecimalSeparator && targetTextBox != null && targetTextBox.Text.Contains(decimalSeparator))
            {
                e.Handled = true;
            }

        }
    }
}
