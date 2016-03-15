using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 弦截法
{
    class 割线法
    {

        private double x0; //旧值
        private double x1;  //初始值
        private double x2;  //新值
        private double e;   //允许误差
        private int n;    //最大迭代次数
        private int count;  //迭代次数
        private double[] arrc; //多项式系数
      
        //各字段对应属性封装
        public double X0
        {
            get { return x0; }
            set { x0 = value; }
        }

        public double X1
        {
            get{return x1;}
            private set{x1=value;}
        }

        public double X2
        {
            get{return x2;}
            private set{x2=value;}
        }

        public double E
        {
            get { return e; }
            set 
            {
                if (value < 0)
                    throw new Exception("误差不能为负数！");
                else
                    e = value;
            }
        }

        public int N
        {
            get { return n; }
            set
            {
                if (value < 0)
                    throw new Exception("最大迭代次数必须为非负整数！");
                else
                    n = value;
            }
        
        }

        public int COUNT
        {
            get { return count; }
            private set { count = value; }
        }

        public double[] ARRC
        {
            get { return arrc; }
            set { arrc = value; }
        }

        public 割线法()
        { }
        //有参数的公用方法，实现数据转换
        public 割线法(double[] arrc, double x0, double x1,double e, int n)
        {
            ARRC = arrc;
            X0 = x0;
            X1 = x1;
            E = e;
            N = n;            
        }
              
        //计算函数y值
        private double y(double x)  
        {
            double y = 0;
            for (int i = 0; i < ARRC.Length; i++)
                y += ARRC[i] * Math.Pow(x, i);
            return y;            
        }        

        //迭代方法的计算
        public void CalGe()
        {
            //定义中间变量保存X1的初始值，方便后面的迭代计算
            double temp;
            temp = X1;
            X1 = X0;
            X2 = temp;
            do
            {
                if(COUNT>N)
                    throw new Exception("迭代次数太大,该迭代发散，此方法不可用！");                
                X0 = X1;                
                X1 =X2;
                //割线法比值不为零，判断报错
                if (y(X1) == y(X0))
                    throw new Exception("割线法迭代失效!");
                else
                {
                    X2 = X1 - y(X1) * (X1 - X0) / (y(X1) - y(X0));
                    COUNT++;
                }
                
            } while (Math.Abs(X2 - X1) >= E );
        }
    


    }
}
