using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anfis2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Train.Enabled = false;
        }

        double f1, f2;
        double MA1, MA2, MB1, MB2;
        double W1, W2;
        double Wone, Wtwo;
        double O1, O2;
        double Ototal;
        double Error;
        double Desired;
        double a11;
        double a12;
        double a21;
        double a22;
        double b11;
        double b12;
        double b21;
        double b22;
        double c11;     
        double c12;
        double c21;
        double c22;
        double x;
        double y;

        //layer 1
        void Layer1()
        {
            MA1 = 1 / (1 + Math.Pow(Math.Abs((c11-x) / a11), (2*b11)));
            MB1 = 1 / (1 + Math.Pow(Math.Abs((c12-y) / a12), (2*b12)));
            f1 = (0.1 * x) + (0.1 * y);
            //f1 = (0.1 * x) + (0.1 * y) + 0.1;
            MA2 = 1 / (1 + Math.Pow(Math.Abs((c21-x) / a21), (2*b21)));
            MB2 = 1 / (1 + Math.Pow(Math.Abs((c22-x) / a22), (2*b22)));
            f2 = (10 * x) +(10 * y);
            //f2 = (10 * x) + (10 * y) + 10;
        }

        //layer 2
        void Layer2()
        {
            W1 = MA1 * MB1;
            //W1 = Math.Min(MA1, MB1);
            W2 = MA2 * MB2;
            //W2 = Math.Min(MA2, MB2);
        }
      
        //layer 3
        void Layer3()
        {
            Wone = W1 / (W1 + W2);
            Wtwo = W2 / (W1 + W2);
        }

        //layer 4
        void Layer4()
        {
            O1 = Wone * f1;
            O2 = Wtwo * f2;
        }

        //layer 5
        void Layer5()
        {
            Ototal = O1 + O2;
        }

        //Error calculating
        void ErrorCalc()
        {
            Error = Math.Pow((Desired - Ototal),2);
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Calculate.Enabled = false;
            Train.Enabled = true;
            a11 =Convert.ToDouble(a11User.Text);
            a12 = Convert.ToDouble(a12User.Text);
            a21 = Convert.ToDouble(a21User.Text);
            a22 = Convert.ToDouble(a22User.Text);
            b11 = Convert.ToDouble(b11User.Text);
            b12 = Convert.ToDouble(b12User.Text);
            b21 = Convert.ToDouble(b21User.Text);
            b22 = Convert.ToDouble(b22User.Text);
            c11 = Convert.ToDouble(c11User.Text);
            c12 = Convert.ToDouble(c12User.Text);
            c21 = Convert.ToDouble(c21User.Text);
            c22 = Convert.ToDouble(c22User.Text);
            x= Convert.ToDouble(xUser.Text);
            y= Convert.ToDouble(yUser.Text);
            Desired = Convert.ToDouble(DesiredResult.Text);
            Layer1();
            Layer2();
            Layer3();
            Layer4();
            Layer5();
            OutputInitial.Text = String.Format("{0:F5}", Ototal);
            ErrorCalc();
            ErrorInitial.Text = String.Format("{0:F5}", Error);
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Calculate.Enabled = true;
            /*
            a11User.Text = null;
            a12User.Text = null;
            a21User.Text = null;
            a22User.Text = null;

            b11User.Text = null;
            b12User.Text = null;
            b21User.Text = null;
            b22User.Text = null;

            c11User.Text = null;
            c12User.Text = null;
            c21User.Text = null;
            c22User.Text = null;

            //xUser.Text = null;
            //yUser.Text = null;
            */
            Error = 0;
            Ototal = 0;
            OutputInitial.Text = null;
            ErrorInitial.Text = null;
            a11 = a12 = a21 = a22 = b11 = b12 = b21 = b22 = c11 = c12 = c21 = c22 = 0;
        }

        private void Train_Click(object sender, EventArgs e)
        {
            Train.Enabled = false;
            Train t = new Train();           
            
            do
            {
                a11 = t.A11;
                a12 = t.A12;
                a21 = t.A21;
                a22 = t.A22;
                b11 = t.B11;
                b12 = t.B12;
                b21 = t.B21;
                b22 = t.B22;
                c11 = t.C11;
                c12 = t.C12;
                c21 = t.C21;
                c22 = t.C22;
                Layer1();
                Layer2();
                Layer3();
                Layer4();
                Layer5();
                ErrorCalc();
                if (Error > 0.0001)
                {
                    t.UpdateA();
                    //t.UpdateB();
                    t.UpdateC();
                }
                else
                {
                    c11Final.Text = String.Format("{0:F5}", c11);
                    c12Final.Text = String.Format("{0:F5}", c12);
                    c21Final.Text = String.Format("{0:F5}", c21);
                    c22Final.Text = String.Format("{0:F5}", c22);
                    a11Final.Text = String.Format("{0:F5}", a11);
                    a12Final.Text = String.Format("{0:F5}", a12);
                    a21Final.Text = String.Format("{0:F5}", a21);
                    a22Final.Text = String.Format("{0:F5}", a22);
                    b11Final.Text = String.Format("{0:F5}", b11);
                    b12Final.Text = String.Format("{0:F5}", b12);
                    b21Final.Text = String.Format("{0:F5}", a21);
                    b22Final.Text = String.Format("{0:F5}", a22);
                    OutputFinal.Text = String.Format("{0:F5}",Ototal);
                    ErrorFinal.Text = String.Format("{0:F5}", Error); ;
                    break;
                }
                
                
            } while (true);
        }
    }
}
