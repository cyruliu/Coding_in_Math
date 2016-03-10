using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 非线性方程求解__Newton
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

      





    //    private void Fun(double x)
    //{
    //        double f;
    //    f = x * x * x - x * x - 1;
    //    return f;     
    //}
    //    private void Fun1(double x)
    //    {
    //        double f1 = 3 * x * x - 2 * x;
    //        return f1;
    //    }

        private void btnOk_Click_1(object sender, EventArgs e)
        {
            try
            {

                int n = int.Parse(txtNum.Text);
                dataGrid1.ColumnCount = n + 1;

                for (int i = 0; i <= n; i++)
                    dataGrid1.Columns[i].Name = "a" + (i).ToString();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        } 

        private void btnSolve_Click(object sender, EventArgs e)
        {
            try
            {
                double result= double.Parse(txtStart.Text);
                double Min = double.Parse(txtMin.Text);
                double Max = double.Parse(txtMax.Text);
                double Limit = double.Parse(txtLimit.Text);                
                int n = int.Parse(txtNum.Text);
                double[] A = new double[n + 1];
                double[] B = new double[n];
                double[] C=new double[n+100];
                //将方程函数的系数和其一阶导系数分别存放到两数组中。
                for (int i = 0; i < n + 1; i++)
                    A[i] = double.Parse(dataGrid1.Rows[0].Cells[i].Value.ToString());
                for (int i = 0; i < n; i++)
                    B[i] = A[i + 1] * (i+1);
                for (int i = 1; i < n+100; i++)
                {
                    C[0] = result;
                    if (C[i-1]>=Min&&C[i-1]<=Max)
                  {
                    //计算f(x)的值
                    double sum=0;
                    for (int j = 0; j < n + 1; j++)
                        sum += A[j] * Math.Pow(C[i-1], j);                                           
                   
                    //计算f(x)一阶导的值
                    double sum1 = 0;
                    for (int j = 0; j < n ; j++) 
                        sum1 += B[j] * Math.Pow(C[i-1], j);
       
                    //Newton开始迭代求下一个x的值
                    C[i] = C[i-1] - (sum / sum1);
                    if (Math.Abs(C[i]-C[i-1])<Limit)
                    {
                        result = C[i];
                        break;
                    }               
                  }
                }
                
                txtResult.Text = "方程满足精度的近似解为：X=" + result.ToString();

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }         

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

       



    }
}
