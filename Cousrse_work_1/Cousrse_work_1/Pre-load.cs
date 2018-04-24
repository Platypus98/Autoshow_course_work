using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Cousrse_work_1
{
    public partial class Pre_load : Form
    {
        public Pre_load()
        {
            InitializeComponent();
        }

        //Нажатие на кнопку "Загрузить файл"
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory + "\\" + "Cars";
            openFileDialog1.Filter = "xml files (*.xml)|*.xml";

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл

            Form1.filename = openFileDialog1.FileName;
            Form1.list = MySerial<List<CarClass>>.Deserialize(Form1.filename);
            Close();

            
        }

        //Нажатие на кнопку "Создать новый"
        private void button2_Click(object sender, EventArgs e)
        {
            Form1.filename = "";
            Close();
        }

        
        private void Pre_load_Load(object sender, EventArgs e)
        {
            if (Directory.GetFiles(Environment.CurrentDirectory + "\\" + "Cars\\").Length == 0)
            {
                button1.Enabled = false;
            }
            
            if (Directory.GetFiles(Environment.CurrentDirectory + "\\" + "Cars\\").Contains(Environment.CurrentDirectory + "\\" + "Cars\\" + MyReg.ValueGet()))
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
            
        }

        private void Pre_load_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        //Нажатие на кнопку "Загрузить тестовый список"
        private void button3_Click(object sender, EventArgs e)
        {
            Form1.filename = "";
            List<CarClass> a = new List<CarClass>();
            a.Add(new CarClass(1, "BMW", "X5", 2017, "Синий", 2000000));
            a.Add(new CarClass(2, "Ford", "Focus", 2008, "Белый", 400000));
            a.Add(new CarClass(3, "Tesla", "Model S", 2018, "Серый", 10000000));
            a.Add(new CarClass(4, "Mazda", "6", 2016, "Красный", 1500000));
            a.Add(new CarClass(5, "Volkswagen", "Polo", 2015, "Белый", 1400000));
            Form1.list = a;
            Close();
        }

        //Нажатие на кнопку "Открыть ранее закрытый файл"
        private void button4_Click(object sender, EventArgs e)
        {
            Form1.filename = Environment.CurrentDirectory + "\\" + "Cars\\" + MyReg.ValueGet();
            Form1.list = MySerial<List<CarClass>>.Deserialize(Form1.filename);
            Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
