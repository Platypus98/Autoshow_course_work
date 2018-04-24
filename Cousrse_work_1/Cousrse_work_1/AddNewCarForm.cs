using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cousrse_work_1
{
    public partial class AddNewCarForm : Form
    {
        int id;  //Номер записи автомобиля
        long price; //Цена автомобиля
        int year;   //Год выпуска автомобиля


        public AddNewCarForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AddNewCarForm_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (!int.TryParse(textBox1.Text, out id))  //Проверка ввода числа в поле id
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(this, "В поле  должно быть число!", "Ошибка", buttons, MessageBoxIcon.Error);
                textBox1.Select();
            }

            else if(!long.TryParse(textBox6.Text, out price))  //Проверка ввода числа в поле price
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(this, String.Format("В поле {0} должно быть число!", label7.Text), "Ошибка", buttons, MessageBoxIcon.Error);
                textBox6.Select();
            }
            else if(!int.TryParse(textBox4.Text, out year))  //Проверка ввода числа в поле year
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(this, String.Format("В поле {0} должно быть число!", label6.Text), "Ошибка", buttons, MessageBoxIcon.Error);
                textBox4.Select();
            }

            else
            {
                Form1.list.Add(new CarClass(id, textBox2.Text, textBox3.Text, year, textBox5.Text, price));   //Добавление в список нового объекта
                Close();
            }
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
