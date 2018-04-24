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
    public partial class Authorization : Form
    {
        public static int mode=0; //0- полный доступ, 1 - только чтение

        public Authorization()
        {
            InitializeComponent();
            
        }

        //Нажатие на конпку "Войти"
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            if (mode == 1)
            {
                Hide();
                form1.ShowDialog();
                Close();
              
            }
            else
            {
                if (textBox1.Text == "test" && textBox2.Text == "test") //Проверка логина и пароля
                {
                    mode = 0;
                    Hide();
                    form1.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;

                    DialogResult result;

                    result = MessageBox.Show("Неправильно введен логин или пароль", "Повторите ввод", buttons, MessageBoxIcon.Error);
                }

            }
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        //Изменение значения CheckBox "Только просмотр"
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (mode == 0)
            {
                groupBox1.Visible = false;
                mode = 1;
                this.Size = new System.Drawing.Size(230, 200);

            }
            else
            {
                groupBox1.Visible = true;
                mode = 0;
                this.Size = new System.Drawing.Size(595, 220);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void Authorization_Load(object sender, EventArgs e)
        {

        }

        private void Authorization_FormClosing(object sender, FormClosingEventArgs e)
        {
            
                
        }
    }
}
