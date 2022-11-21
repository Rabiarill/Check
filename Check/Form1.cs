using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Check
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public double totalPrice = 0;
        public double totalDiscount = 0;
        public double totalCash = 0;
        public double totalBonus = 0;
        private void AddButton_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string name = nameTextBox.Text;
            int count = Convert.ToInt32(countTextBox.Text);
            double price = Convert.ToDouble(priceTextBox.Text);
            double cost = price * count;
            double discount = (Convert.ToDouble(discountTextBox.Text) / 100.0) * cost;
            double costWithDiscount = cost - discount;
            double bonus = cost * 0.1;
            informationTextBox.Text +=
                "\r\nДата:........................................................." + dt.ToShortDateString() +
                "\r\nТовар:......................................................." + name +    
                "\r\nКоличество:.........................................." + count +
                "\r\nЦена:........................................................" + price +
                "\r\nСтоимость без скидки:..................." + cost +
                "\r\nСкидка:..................................................." + discount +
                "\r\nСтоимость со скидкой...................." + costWithDiscount + 
                "\r\nНДС (13%) :.........................................." + cost*0.13 +
                "\r\nБонус (10%):........................................." + bonus + 
                "\r\n------------------------------------------------------------------------";

            totalPrice += cost;
            totalDiscount += discount;
            totalCash += costWithDiscount;
            totalBonus += bonus;
            totalPriceTextBox.Text = Convert.ToString(totalPrice);
            totalDiscountTextBox.Text = Convert.ToString(totalDiscount);
            totalCashTextBox.Text = Convert.ToString(totalCash);
            totalBonusTextBox.Text = Convert.ToString(totalBonus);

        }

        private void nameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void priceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (number == '-' && priceTextBox.Text != "") e.Handled = true;
            if (number == ',')
            {
                if (priceTextBox.Text.Contains(",")) e.Handled = true;
                if (priceTextBox.Text == "") e.Handled = true;
            }

            if (!Char.IsDigit(number) && number != 8 && number != 44 && number != 45)
            {
                e.Handled = true;
            }
        }

        private void discountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            

            if (number == ',')
            {
                if (discountTextBox.Text.Contains(",")) e.Handled = true;
                if (discountTextBox.Text == "") e.Handled = true;
            }

            if (!Char.IsDigit(number) && number != 8 && number != 44)
            {
                e.Handled = true;
            }

            if(discountTextBox.Text != "")
            {
                if (Convert.ToDouble(discountTextBox.Text) > 9d && number != 8)
                {
                    e.Handled = true;
                }
            }
                
            
        }

        private void countTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar; 
            if (!Char.IsDigit(number) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            informationTextBox.Text = "";
            nameTextBox.Text = "";
            priceTextBox.Text = "";
            countTextBox.Text = "";
            discountTextBox.Text = "10";

            totalPriceTextBox.Text = "";
            totalDiscountTextBox.Text = "";
            totalCashTextBox.Text = "";
            totalPrice = 0;
            totalDiscount = 0;
            totalCash = 0;
            totalBonus = 0;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            StreamWriter file = new StreamWriter("check.txt",true);
            file.Write(informationTextBox.Text);
            this.Close();
        }
    }
}
