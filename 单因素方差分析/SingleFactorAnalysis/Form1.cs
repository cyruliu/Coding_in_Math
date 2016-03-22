using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 单因素方差分析
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
                int ni, r;//r为因数A水平个数，ni为各因素水平的最大测验次数
                ni = int.Parse(txtData.Text);
                r = int.Parse(txtFactors.Text);

                //分别定义数据表的列和行及其标题。
                dataGridView1.ColumnCount = ni;
                dataGridView1.RowCount = r;               
                for (int i = 0; i < r; i++)
                    dataGridView1.Rows[i].HeaderCell.Value = "水平A" + (i + 1).ToString(); 
                for (int j = 0; j < ni; j++)
                    dataGridView1.Columns[j].Name = "测验数" + (j + 1).ToString();

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
   private void btnCal_Click(object sender, EventArgs e)
        {
            try
            {
                int ni, r, n;
                ni = int.Parse(txtData.Text);//在定义一次赋值
                r = int.Parse(txtFactors.Text);
                double[,] array = new double[r, ni];//转存datagridvie1中的数据到数组中去。
                int[] r_count=new int[r];
                n = 0;
                
                //赋值、转存到数组中去
                for (int i = 0; i < r; i++)
                {
                    for (int j = 0; j < ni; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                        {
                            array[i, j] = double.Parse(dataGridView1.Rows[i].Cells[j].Value.ToString());
                            r_count[i]++;
                            n++;
                        }
                        else
                            array[i, j] = 0;
                    }
                }
                string s,s1;
                //开始方差分析计算
                VAnalysis VA = new VAnalysis(array, r_count, r, n);
                VA.Solving();
                if (VA.F >= table[r - 2, n - r - 1])
                    s1 = "拒绝假设：试验因素对试验指标的影响显著";
                else
                    s1 = "接受假设：试验因素对试验指标的影响不显著";
                //将计算结果输出
                s = "方差来源       " + "平方和       " + "自有度    " + "均方           " + "F值  "+Environment.NewLine;
                s+="因素A          "+VA.SA.ToString("f2")+"       "+(r-1).ToString()+"     "+VA.SAM.ToString("f2")+"      "+VA.F.ToString("f2")+Environment.NewLine;
                s+="误差           "+VA.SE.ToString("f2")+"       "+(n-r).ToString()+"    "+VA.SEM.ToString("f2")+Environment.NewLine;
                s+="总和           "+VA.ST.ToString("f2")+"       "+(n-1).ToString()+Environment.NewLine;
                s += s1;
                txtOutput.Text = s;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

   double[,] table = new double[40, 40];//F分布分位点值的查表数据记录
        private void cboLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboLevel.SelectedIndex)
            {
                case 0:
                    table[2, 31] = 2.35;    //a=0.1,F(3,22)的值
                    break;
                case 1:
                    table[1, 31] = 3.89;  //a=0.05,F(2,12)的值
                    break;
                case 2:
                    table[3, 14] = 4.89;  //a=0.01,F(4,15)的值
                    break;
            }
        }

     

    
     

     

        
    }
}
