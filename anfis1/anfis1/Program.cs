using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anfis1
{
    class Program
    {
        static void Main(string[] args)
        {
            double a11=2, a12=2, a21=2, a22=2, b=1, c11=1, c12=2, c21=9,c22=14;
            //double a11, a12, a21, a22, b, c11, c12, c21, c22;
            double f1, f2;
            double MA1, MA2, MB1, MB2;
            double W1, W2;
            double Wone, Wtwo;
            double O1, O2;
            double Ototal;
            double Error;
            double Desired=1;
            int Cycle = 0;
            Random r = new Random();
            do {

                //layer 1
                void Layer1(double x1, double y1)
                {
                    MA1 = 1 / (1 + Math.Pow(Math.Abs((x1 - c11) / a11), 2 * b));
                    MB1 = 1 / (1 + Math.Pow(Math.Abs((y1 - c12) / a12), 2 * b));
                    f1 = 0.1 * x1 + 0.1 * y1 + 0.1;
                    //Console.WriteLine("MA1=" + MA1 + "\nMB1=" + MB1 + "\nf1=" + f1);

                    MA2 = 1 / (1 + Math.Pow(Math.Abs((x1 - c21) / a21), 2 * b));
                    MB2 = 1 / (1 + Math.Pow(Math.Abs((y1 - c22) / a22), 2 * b));
                    f2 = 10 * x1 + 10 * y1 + 10;
                    //Console.WriteLine("MA2=" + MA2 + "\nMB2=" + MB2 + "\nf2=" + f2);
                    Layer2();
                }

                //layer 2
                void Layer2()
                {
                    W1 = MA1 * MB1;
                    //W1 = Math.Min(MA1, MB1);
                    W2 = MA2 * MB2;
                    //W2 = Math.Min(MA2, MB2);
                    //Console.WriteLine("w1=" + W1 + "\nw2=" + W2);
                    Layer3();
                }

                //layer 3
                void Layer3()
                {
                    Wone = W1 / (W1 + W2);
                    Wtwo = W2 / (W1 + W2);
                    //Console.WriteLine("w_1=" + Wone + "\nw_2=" + Wtwo);
                    Layer4();
                }

                //layer 4
                void Layer4()
                {
                    O1 = Wone * f1;
                    O2 = Wtwo * f2;
                    //Console.WriteLine("O1=" + O1 + "\nO2=" + O2);
                    Layer5();
                }

                //layer 5
                void Layer5()
                {
                    Ototal = O1 + O2;
                    //Console.WriteLine("Ototal=" + Ototal);
                }

                //Error calculating
                void ErrorCalc()
                {
                    Error = Math.Pow((Desired - Ototal), 2);
                   // Console.WriteLine("Error=" + Error);
                }
                Layer1(3, 4);
                ErrorCalc();
                Cycle++;
                
                if (Error > 0.001)
                {
                    c11 += 0.1;
                    c12 += 0.1;
                    c21 += 0.1;
                    c22 += 0.1;
                    a11 += 0.1;
                    a12 += 0.1;
                    a21 += 0.1;
                    a22 += 0.1;
                    b += 0.1;
                    
                    Console.WriteLine("Cycle:"+Cycle+"\nError=" + Error + "\nF=" + Ototal + "\nDesired=" + Desired);
                }
                else
                {
                    Console.WriteLine("Cycle:" + Cycle + "\nError=" + Error + "\nF=" + Ototal + "\nDesired=" + Desired);

                    break;
                }
            } while (true);

            Console.ReadKey();
        }
    }
}
