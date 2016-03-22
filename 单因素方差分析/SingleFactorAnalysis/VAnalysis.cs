using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 单因素方差分析
{
    class VAnalysis
    {
        private double sa;      //定义因素A平方和字段
        private double se;      //定义误差平方和字段
        private double st;      //总和平方和字段
        private double sam;     //对应因素A均方
        private double sem;     //对应误差均方
        private double f;       //定义F值字段
        private double[,] arrayv;       //保存测验数据
        private int[] vr_count;         //记录各水平的测验次数
        private int n;              //总的测验次数
        private int r;              //水平个数

        //各字段对应属性
        public double SA
        {
            get { return sa; }
            set { sa = value; }
        }

        public double SE
        {
            get { return se; }
            set { se = value; }
        }

        public double ST
        {
            get { return st; }
            set { st = value; }        
        }

        public double SAM
        {
            get { return sam; }
            set { sam = value; }
        }

        public double SEM
        {
            get { return sem; }
            set { sem = value; }
        }

        public double F
        {
            get { return f; }
            set { f = value; }
        }

        public double[,] ARRAYV
        {
            get { return arrayv; }
            set { arrayv = value; }
        }

        public int[] VR_COUT
        {
            get { return vr_count; }
            set { vr_count = value; }
        }

        public int N
        {
            get { return n; }
            set { n = value; }
        }

        public int R
        {
            get { return r; }
            set { r = value; }
        }


        //数据存取方法
        public VAnalysis()
        {  }
        public VAnalysis(double[,] array, int[] rcount, int r, int n)
        {
            ARRAYV = array;
            VR_COUT = rcount;
            R = r;
            N = n;
        }
        //方差分析计算求解方法
        public void Solving()
        {
            double st1 = 0, s2 = 0, sa1 = 0, temp = 0;
            
            //计算st1的值
            for (int i = 0; i <R; i++)
            {
                for (int j = 0; j < VR_COUT[i]; j++)
                    st1 += Math.Pow(ARRAYV[i, j], 2);
            }
            //计算s2的值
            for (int i = 0; i < R; i++)
            {
                for (int j = 0; j < VR_COUT[i]; j++)
                    s2 += ARRAYV[i, j];
             }
            s2 = Math.Pow(s2, 2) / N;

            ST = st1 - s2;//计算ST

            //计算sa1
            for (int i = 0; i < R; i++)
            {
                for (int j = 0; j < VR_COUT[i]; j++)
                    temp += ARRAYV[i, j];
                sa1 += Math.Pow(temp, 2) / VR_COUT[i];
                temp = 0;
            }

            //计算SA,SAM
            SA = sa1 - s2;
            if (Math.Abs(R - 1) < 0.00000001)
                throw new Exception("因素水平个数为1，无需进行方差分析！！");
            else
                SAM = SA / (R - 1);

            //计算SE,SEM
            SE = ST - SA;
            if (Math.Abs(N - R) < 0.00000001)
                throw new Exception("测验次数太少，无法进行方差分析，请添加个多的测验数据！！");
            else
                SEM = SE / (N - R);

            //计算F检验值
            if (Math.Abs(SEM) < 0.00000001)
                throw new Exception("误差均方值为零，无法计算，该方法失效，请检验数据值！！");
            else
                F = SAM / SEM;
        }

    }
}
