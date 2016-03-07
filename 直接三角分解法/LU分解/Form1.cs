using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 直接三角分解法
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            try
            {
                int n;
                n = int.Parse(txtNum.Text);
                dataGrid1.ColumnCount = n + 1;
                dataGrid1.RowCount = n;
                for (int i = 0; i < n; i++)
                    dataGrid1.Columns[i].Name = "x" + (i + 1).ToString();
                dataGrid1.Columns[n].Name = "b";
                //变形后的矩阵存放地
                dataGrid2.ColumnCount = n + 1;
                dataGrid2.RowCount = n;
                for (int i = 0; i < n; i++)
                    dataGrid2.Columns[i].Name = "x" + (i + 1).ToString();
                dataGrid2.Columns[n].Name = "b";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }         
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            try
            {
                //赋值，将系数矩阵转存到数组里
                int n;
                n = int.Parse(txtNum.Text);
                double[,] arrA = new double[n, n + 1];
                double[] arrB = new double[n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j <= n; j++)
                        arrA[i, j] = double.Parse(dataGrid1.Rows[i].Cells[j].Value.ToString());                    
                }
                string s = "";
                if (LU(arrA, arrB, n) == 1)
                {
                    txtResult.Text = "此方法不能顺利进行！";
                }
                else
                {
                    for (int i = 0; i < n; i++)
                        s += "x" + (i + 1).ToString() + "=" + arrB[i].ToString("f4") + " ";
                    txtResult.Text = s;
                }
                //将变形的矩阵输入到该区域dataGrid2中。
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j <= n; j++)
                        dataGrid2.Rows[i].Cells[j].Value = double.Parse(arrA[i, j].ToString());
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private int LU(double[,] arrA, double[] arrB, int n)
        {
             //先求第一列
            for (int i =1; i < n; i++)
            {
                arrA[i, 0] = arrA[i, 0] / arrA[0, 0];
            }
            //紧凑法对矩阵arrA进行变换。
            for (int k = 1; k < n ; k++)
            {
                //求行U的值
                for (int j = k; j <=n; j++)
                {                    
                    for(int i0=k-1;i0>=0;i0-- )
                    arrA[k,j]=arrA[k,j]-arrA[k,i0]*arrA[i0,j];
                }
                //求列L的值
                for (int i = k+1; i < n; i++)
                {
                    for (int i0 = k - 1; i0>=0; i0--)
                        arrA[i, k] = arrA[i, k] - arrA[i, i0] * arrA[i0, k];
                    arrA[i, k] = arrA[i, k] / arrA[k, k];                    
                }

            }
              
            //回代               

            for (int i = n - 1; i >= 0; i--)
            {
                for (int j = i + 1; j < n; j++)
                    arrA[i, n] = arrA[i, n] - arrA[i, j] * arrB[j];
                if (Math.Abs(arrA[i,i])<0.00000001)                
                    return 1;                
                arrB[i] = arrA[i, n] / arrA[i, i];
            }
            return 0;
        }
        
    }
}
