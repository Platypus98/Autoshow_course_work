using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cousrse_work_1
{
    public partial class Form1 : Form
    {
        public static List<CarClass> list = new List<CarClass>(); //Список автомобилей
        
        public static int a; //Индекс выбраной строки
        public static string filename; //Путь к файлу + название файла
        public static bool change = false; //Проверка на изменение файла

        public Form1()
        {
            InitializeComponent();

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
            groupBox1.Visible = false;

            groupBox2.Visible = false;

            if (!Directory.Exists("Manual")) //Проверка существования папки "Manual"
            {
                Directory.CreateDirectory("Manual");
            }

            if (!Directory.Exists("Cars")) //Проверка существования папки "Cars"
            {
                Directory.CreateDirectory("Cars");
            }

            Pre_load pre_load = new Pre_load();
            pre_load.ShowDialog(); //Открытие формы Pre-load

            if (Authorization.mode == 1) //Обработка отображения кнопок для режима "Только чтение"
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
            }

            if (list.Count() == 0) //Обработка отображения кнопок "Изменить запись" и "Удалить запись" для пустого списка
            {
                button2.Enabled = false;
                button3.Enabled = false;
            }

            carClassBindingSource.DataSource = list;

            if (filename == "") //Обработка присвоения заголовка форме
            {
                this.Text = "Автосалон - Новый";
            }
            else
            {
                this.Text = "Автосалон" + " - " + filename.Substring(filename.LastIndexOf("\\") + 1);
            }
            
            

            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Нажатие на пункт "Справка"
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("NotePad.exe", "Manual\\Manual.txt");
        }

        //Нажатие на кнопку "Добавить новую машину"
        private void button1_Click(object sender, EventArgs e)
        {
            change = true;
            AddNewCarForm form2 = new AddNewCarForm();
            form2.ShowDialog();     //Открытие формы AddNewCarForm
            carClassBindingSource.ResetBindings(false);
            if (list.Count() == 0 || Authorization.mode == 1)
            {
                button2.Enabled = false;
                button3.Enabled = false;
            }
            else if (list.Count() != 0)
            {
                button2.Enabled = true;
                button3.Enabled = true;
            }



        }


        //Нажатие кнопки "Изменить запись"
        private void button2_Click(object sender, EventArgs e)
        {
            change = true;
            EditCarForm form3 = new EditCarForm();
            a = dataGridView1.CurrentRow.Index;  //Получение идекса выделенной записи
            form3.ShowDialog();  //Открытие формы EditCarForm
            carClassBindingSource.ResetBindings(false);
            if (list.Count() == 0 || Authorization.mode == 1)
            {
                button2.Enabled = false;
                button3.Enabled = false;
            }
            else if (list.Count() != 0)
            {
                button2.Enabled = true;
                button3.Enabled = true;
            }
        }

        //Нажатие кнопки "Удалить запись"
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;

            DialogResult result;

            result = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтвердить действие", buttons, MessageBoxIcon.Question);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                a = dataGridView1.CurrentRow.Index; //Получение идекса выделенной записи
                list.RemoveAt(a);  //Удаление записи


            }


            carClassBindingSource.ResetBindings(false);

            if (list.Count() == 0 || Authorization.mode == 1)
            {
                button2.Enabled = false;
                button3.Enabled = false;
            }
            else if (list.Count() != 0)
            {
                button2.Enabled = true;
                button3.Enabled = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //Нажатие кнопки "Отобрать"
        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (RowFind(i))
                    dataGridView1.Rows[i].Visible = true;
                else
                    dataGridView1.Rows[i].Visible = false;
            }

        }

        //---------------------------------------------------
        // Метод проверяет поля переданной строки 
        // на одновременное равенство всем критериям.
        private bool RowFind(int i)
        {
            if (modelBox.Text != "")
                if (dataGridView1["carBrandDataGridViewTextBoxColumn", i].Value != null)
                    if (dataGridView1["carBrandDataGridViewTextBoxColumn", i].Value.ToString() != modelBox.Text)
                        return false;

            if (markBox.Text != "")
                if (dataGridView1["carModelDataGridViewTextBoxColumn", i].Value != null)
                    if (dataGridView1["carModelDataGridViewTextBoxColumn", i].Value.ToString() != markBox.Text)
                        return false;

            if (groupBox2.Visible == false)
            {
                if (yearBox.Text != "")
                    if (dataGridView1["yearDataGridViewTextBoxColumn", i].Value != null)
                        if (dataGridView1["yearDataGridViewTextBoxColumn", i].Value.ToString() != yearBox.Text)
                            return false;
            }
            else
            {
                if (textBox4.Text != "" && textBox5.Text != "")
                    if (dataGridView1["yearDataGridViewTextBoxColumn", i].Value != null)
                    {
                        try
                        {
                            int.Parse(textBox4.Text);
                            int.Parse(textBox5.Text);
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                        if (int.Parse(dataGridView1["yearDataGridViewTextBoxColumn", i].Value.ToString()) < int.Parse(textBox4.Text) || int.Parse(dataGridView1["yearDataGridViewTextBoxColumn", i].Value.ToString()) > int.Parse(textBox5.Text))
                        {
                            return false;
                        }
                    }
            }
            

            if (colorBox.Text != "")
                if (dataGridView1["colorDataGridViewTextBoxColumn", i].Value != null)
                    if (dataGridView1["colorDataGridViewTextBoxColumn", i].Value.ToString() != colorBox.Text)
                        return false;

            if (groupBox1.Visible == false)
            {
                if (textBox3.Text != "")
                    if (dataGridView1["priceDataGridViewTextBoxColumn", i].Value != null)
                        if (dataGridView1["priceDataGridViewTextBoxColumn", i].Value.ToString() != textBox3.Text)
                            return false;
            }
            else 
            {
                
                if (textBox1.Text != "" && textBox2.Text != "")
                    if (dataGridView1["priceDataGridViewTextBoxColumn", i].Value != null)
                    {
                        try
                        {
                            int.Parse(textBox1.Text);
                            int.Parse(textBox2.Text);
                        }
                        catch (Exception)
                        {
                            return false;
                        }

                        if (long.Parse(dataGridView1["priceDataGridViewTextBoxColumn", i].Value.ToString()) < long.Parse(textBox1.Text) || long.Parse(dataGridView1["priceDataGridViewTextBoxColumn", i].Value.ToString()) > long.Parse(textBox2.Text))
                        {
                            
                            return false;
                        } 
                    }
                        
            }
            




            return true;
        }


        //Показать все записи
        private void button5_Click(object sender, EventArgs e)
        {
            int count = dataGridView1.Rows.Count;

            for (int i = 0; i < count; i++)
                dataGridView1.Rows[i].Visible = true;

            modelBox.Text = "";
            markBox.Text = "";
            colorBox.Text = "";
            yearBox.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void yearBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (groupBox1.Visible == false)
            {
                groupBox1.Visible = true;
                textBox3.Visible = false;
            }
            else
            {
                groupBox1.Visible = false;
                textBox3.Visible = true;
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }


        private void button6_Click_1(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        //Нажатие пунка "Открыть"
        private void открытьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {           
            SaveFile();
          
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory + "\\" + "Team";
            openFileDialog1.Filter = "xml files (*.xml)|*.xml";

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) //Открытие диалогового окна
                return;
            // получаем выбранный файл

            filename = openFileDialog1.FileName;
            list = MySerial<List<CarClass>>.Deserialize(filename);
            carClassBindingSource.DataSource = list;
            carClassBindingSource.ResetBindings(false);
            if (filename == "")
            {
                this.Text = "Автосалон - Новый";
            }
            else
            {
                this.Text = "Автосалон" + " - " + filename.Substring(filename.LastIndexOf("\\") + 1);
            }


        }


        //Нажатие пункта "Сохранить"
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveFile();


        }

        //Нажатие пункта "Создать"
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveFile();

            list = new List<CarClass>();

            carClassBindingSource.DataSource = list;

            filename = "";

            
            this.Text = "Автосалон - Новый";


        }


        //Метод сохранения файлов, в которых были изменения или они были новыми
        public void SaveFile()
        {
            if (filename == "")
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                DialogResult result;

                result = MessageBox.Show("Вы хотите сохранить файл?", "Подтвердить действие", buttons, MessageBoxIcon.Question);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = " XML Files(*.xml)| *.xml";
                    saveFileDialog1.InitialDirectory = Environment.CurrentDirectory + "\\" + "Cars";

                    if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                        return;
                    // получаем выбранный файл
                    filename = saveFileDialog1.FileName;
                    // сохраняем текст в файл

                    MySerial<List<CarClass>>.Serialize(filename, list);
                    MessageBox.Show("Файл сохранен", "Подтвердить", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                return;
            }


            if (change == true)
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                DialogResult result;

                result = MessageBox.Show("Вы хотите сохранить изменения?", "Подтвердить действие", buttons, MessageBoxIcon.Question);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    File.Delete(filename);
                    MySerial<List<CarClass>>.Serialize(filename, list);
                    MessageBox.Show("Файл сохранен", "Подтвердить", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            
            
        }

        //Обработка события закрытия формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFile();
            if (filename != "")
                MyReg.ValueSet(filename.Substring(filename.LastIndexOf("\\") + 1));
            
        }


        //Обработка нажатия клавиш
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                button3.PerformClick();
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (groupBox2.Visible == false)
            {
                groupBox2.Visible = true;
                yearBox.Visible = false;
            }
            else
            {
                groupBox2.Visible = false;
                yearBox.Visible = true;
            }
        }


        private void обАвтореToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("NotePad.exe", "Manual\\AboutAuthor.txt");
        }
    }
}
