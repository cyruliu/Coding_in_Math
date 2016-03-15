using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 弦截法
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                //输入字符串，数据输入转换
                string[] s = txtCoefficient.Text.Split(',');
                double[] Coefficient = new double[s.Length];//将字符串中系数存放在数组中
                for (int i = 0; i < s.Length; i++)
                    Coefficient[i] = double.Parse(s[i]);

                //开始迭代计算
                //生成类的新对象
                割线法 G = new 割线法(Coefficient, double.Parse(txtInitial.Text),double.Parse(txtInitial1.Text),double.Parse(txtError.Text), int.Parse(txtNumber.Text));
                G.CalGe();                
                txtOutPut.Text = "迭代结果是：迭代" + G.COUNT.ToString() + "次得到:X=" + G.X2.ToString("f7");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //清空文本框
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCoefficient.Text = "";
            txtInitial.Text = "";
            txtInitial1.Text = "";
            txtError.Text = "";
            txtNumber.Text = "";
            txtOutPut.Text = "";

        }
    }
}
