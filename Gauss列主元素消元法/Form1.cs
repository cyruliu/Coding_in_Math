using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gauss列主元素消元法
{
    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();
        }                   
        public void btnSure_Click(object sender, EventArgs e)
        {
            try 
            {
                int n;
                n = int.Parse(txtRow.Text);
                dataMatrix1.ColumnCount = n + 1;
                dataMatrix1.RowCount = n;
                for (int i = 0; i < n; i++)                
                    dataMatrix1.Columns[i].Name = "x" + (i + 1).ToString();
                dataMatrix1.Columns[n].Name = "b";                
            }
            catch(Exception exp)
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
                n = int.Parse(txtRow.Text);
                double[,] arrA = new double[n, n+1];
                double[] arrB = new double[n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j <=n; j++)
                        arrA[i, j] = double.Parse(dataMatrix1.Rows[i].Cells[j].Value.ToString());
                    arrB[i] = double.Parse(dataMatrix1.Rows[i].Cells[n].Value.ToString());
                }
                string s = "";
                if (Gauss(arrA, arrB, n) == 1)
                {
                    label3.Text = "此方法不能顺利进行！";
                }
                else
                {
                    for (int i = 0; i < n; i++)
                        s += "x" + (i + 1).ToString() + "=" + arrB[i].ToString("f4") + " ";
                    label3.Text = s;
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private int Gauss(double[,] arrA, double[] arrB, int n)
        {
            for (int k = 0; k < n-1; k++)
            {
                // 寻找第k列的主元素以及它所在的行号  
                double t, mainElement;            // mainElement用于保存主元素的值       
                int l;                            // 用于保存主元素所在的行号   
                // 从第k行到第n行寻找第k列的主元素，记下主元素mainElement和所在的行号l  
                mainElement = Math.Abs(arrA[k,k]);  // 取绝对值   
                l = k;
                for (int i = k + 1; i < n; i++)
                {
                    if (mainElement < Math.Abs(arrA[i,k]))
                    {
                        mainElement = Math.Abs(arrA[i,k]);
                        l = i;                        // 记下主元素所在的行号    
                    }
                }
                // l是主元素所在的行。将l行与k行交换，每行前面的k个元素都是0，不必交换        
                if (l != k)
                {
                    for (int j = k; j <= n; j++)
                    {
                        t = arrA[k,j]; arrA[k,j] = arrA[l,j]; arrA[l,j] = t;
                    }
                }
                //互换行后开始进行消元
                for (int i = k + 1; i < n; i++)
                {
                    double d = arrA[i,k]/arrA[k,k];  // 使用变量d将每行的首元素a[i,k]的值保存下来。    
                    for (int j = k; j <= n; j++) //开始消元
                        arrA[i,j] = arrA[i,j] - d * arrA[k,j];
                }
            }
            //k循环完后即可得整个消元矩阵a
            //回代               

                //回代               

            for (int i =n-1; i >=0; i--)
            {
                for (int j = i + 1; j < n; j++)
                    arrA[i,n] = arrA[i,n] - arrA[i, j] * arrB[j];
                arrB[i] = arrA[i,n] / arrA[i, i];
            }





          //该算法一样，只是数组的存放方式不同。
         //   int k, i, j, i0;
         //   double element;
         //   for (k = 0; k < n - 1; k++)
         //   {
         //       element = Math.Abs(arrA[k, k]);
         //       i0 = k;
         //       for ( i =k+1; i <n; i++)                
         //           if (Math.Abs(arrA[i,k])>element)
         //           {
         //               element = Math.Abs(arrA[i, k]);
         //               i0 = i;
         //           }
                
         //       if (element < 0.00000001)
         //           return 1;   
         ////把主元素所在的行换到最上行
         //       if (i0!=k)
         //       {
         //           for ( j = 0; j < n; j++)
         //           {
         //               element = arrA[k, j];
         //               arrA[k, j] = arrA[i0, j];
         //               arrA[i0, j] = element;
         //           }
         //           //互换常量两行
         //           element = arrB[k];
         //           arrB[k] = arrB[i0];
         //           arrB[i0] = element;
         //       }
         //       //换完行后开始逐行消元化为上三角形
         //       for (i = k + 1; i < n; i++)
         //       {
         //           double d = arrA[i, k] / arrA[k, k];
         //           //arrA[i, k] = arrA[i, k] / arrA[k, k];
         //           for (j = k; j < n; j++)
         //               arrA[i, j] = arrA[i, j] - d* arrA[k, j];
         //           arrB[i] = arrB[i] - d* arrB[k];
         //       }
         //   }
         //   //回代               

         //   for (i =n-1; i >=0; i--)
         //   {
         //       for (j = i + 1; j < n; j++)
         //           arrB[i] = arrB[i] - arrA[i, j] * arrB[j];
         //       arrB[i] = arrB[i] / arrA[i, i];
         //   }

           
            return 0;
        }
    }
}
