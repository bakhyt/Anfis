using System;
using System.Collections.Generic;

namespace anfis3
{
    class Anfis
    {
        double fx1,fy1,fx2,fy2,fx3,fy3;
        double MA1, MA2, MA3, MB1, MB2, MB3;
        double W1, W2, W3;
        double Wone, Wtwo, Wthree;
        double O1, O2, O3;
        public double Ototal;
        public double Error;        
        public double c;        
        public double Desired;
        public double x;
        public double y;

        public Anfis()
        {
            c = 2;
        }

        void mR1()
        {
            if (x <= 1)
            {
                MA1 = 0.1;
                fx1 = x;
            }

            if (y <=1)
            {
                MB1 = 0.1;
                fy1 = y;
            }

            if (x >1 && x <= 4)
            {
                MA1 = (Math.Pow(x,c)*0.33) - 0.33;
                fx1 = (5*x)/4;
            }

            if (y >1 && y <= 4)
            {
                MB1 =  (Math.Pow(y,c)*0.33) - 0.33;
                fy1 = (5*y)/4;              
            }
        }

        void mR2()
        {
            if (x >=4 && x <= 7)
            {
                MA2 = (Math.Pow(x,c)*0.33) - 1.33;
                fx2 = (10*x)/7;
            }

            if (y >=4 && y <= 7)
            {
                MB2 = (Math.Pow(y,c)*0.33) - 1.33;
                fy2 = (10*y)/7;
            }
        }

        void mR3()
        {
            if (x >=7)
            {
                MA3 = (Math.Pow(x,c)*0.33) - 2.33;
                fx3 = (15*x)/10;
            }

            if (y>=7)
            {
                MB3 = (Math.Pow(y,c)*0.33) - 2.33;
                fy3 = (15*y)/10;
            }          
        }
        
        //layer 1
        void Layer1()
        {
            mR1();
            mR2();
            mR3();
        }

        //layer 2
        void Layer2()
        {                      
            W1 = Math.Max(MA1, MB1);
            W2 = Math.Max(MA2, MB2);
            W3 = Math.Max(MA3, MB3);
        }

        //layer 3
        void Layer3()
        {
            Wone = W1 / (W1 + W2 + W3);
            Wtwo = W2 / (W1 + W2 + W3);
            Wthree = W3 / (W1 + W2 + W3);
        }

        //layer 4
        void Layer4()
        {
            O1 = Wone * (fx1+fy1)/2;
            O2 = Wtwo * (fx2+fy2)/2;
            O3 = Wthree * (fx3+fy3)/2;
        }

        //layer 5
        void Layer5()
        {
            Ototal = O1 + O2 + O3;
        }

        //Error calculating
        void ErrorCalc()
        {
            Error = Math.Pow((Desired - Ototal), 2);
        }

        public void SetUserInput(double x, double y, double Desired)
        {
            this.x = x;
            this.y = y;
            this.Desired = Desired;
        }

        public void SetNull()
        {
            fx1 = fy1 = fx2 = fy2 = fx3 = fy3 = 0;
            MA1 = MA2 = MA3 = MB1 = MB2 = MB3 = 0;
            W1 = W2 = W3 = 0;
            Wone=Wtwo=Wthree=0;
            O1=O2=O3=0;
            Ototal=0;
            Error=0;
        }

        public void getResult()
        {
            Console.WriteLine(String.Format("Result: {0:F10}", Ototal));
            Console.WriteLine(String.Format("Desired: {0:F10}", Desired));
            Console.WriteLine(String.Format("Error: {0:F10}", Error));
        }

        public void Calculate()
        {
            SetNull();
            Layer1();
            Layer2();
            Layer3();
            Layer4();
            Layer5();
            ErrorCalc();
        }
    }

    class Training
    {
        public Anfis t = new Anfis();
        double Etotal;
        int Max = 0;
        double[][] array;

        List<List<double>> massive = new List<List<double>>()
        {
          new List<double>() {0, 0, 0},
          new List<double>() {1, 1, 1},
          new List<double>() {2, 2, 2.5},
          new List<double>() {3, 3, 3.75},
          new List<double>() {4, 4, 5},
          new List<double>() {5, 5, 7.14},
          new List<double>() {6, 6, 8.57},
          new List<double>() {7, 7, 10},
          new List<double>() {8, 8, 12},
          new List<double>() {9, 9, 13.5},
          new List<double>() {10, 10, 15}
        };

        public Training()
        {
            Adding();            
        }

        public void Adding()
        {
            array = new double[massive.Count][];
            int i = 0;
            int j = 0;
            foreach (List<double> list in massive)
            {
                array[i] = new double[list.Count];
                j = 0;
                foreach (double d in list)
                {
                    array[i][j] = d;
                    j++;
                }                
                i++;
            }            
        }

        public void Update()
        {
            double k = 0.4;

            for (int i = -10; i <10; i++)
            {
                Etotal = 0;
                t.Error = 0;
                t.c = i;

                for (int p = 0; p < array.Length; p++)
                {
                    t.SetUserInput(array[p][0], array[p][1], array[p][2]);
                    t.Calculate();
                    Etotal += t.Error;
                }

                if (k>Etotal)
                {
                    Max = i; k = Etotal; 
                }
            }

            t.c = Max;                 
        }
    }

    class Program
    {        
        static void Main(string[] args)
        {            
            double x, y, Desired;
            double service, food, d;
            Console.WriteLine("Anfis Model");
            Console.WriteLine("Restaurant");
            
                Console.WriteLine("Enter number from 0 to 10 for service:");
                Console.WriteLine("Service:");
                service = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter number from 0 to 10 for food:");
                Console.WriteLine("Food:");
                food = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter number from 0 to 15 persent for expectation:");
                Console.WriteLine("Expectation persent:");
                d = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Results Before Training:");
                Anfis anfis = new Anfis();
                anfis.c = 2;
                x = service;
                y = food;
                Desired = d;
                anfis.SetUserInput(x, y, Desired);
                anfis.Calculate();
                anfis.getResult();
                Console.WriteLine("\nResults After Training the Model: ");
                Training training = new Training();
                training.Update();
                x = service;
                y = food;
                Desired = d;
                training.t.SetUserInput(x, y, Desired);
                training.t.Calculate();
                training.t.getResult();

            
            Console.ReadKey();
        }
    }
}
