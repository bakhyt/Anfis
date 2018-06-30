using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anfis2
{
    class Train
    {
        public double A11;
        public double A12;
        public double A21;
        public double A22;
        public double B11;
        public double B12;
        public double B21;
        public double B22;
        public double C11;
        public double C12;
        public double C21;
        public double C22;
        public Train()
        {
            A11 = 2; A12 = 2; A21 = 2; A22 = 2;
            B11 = 1; B12 = 1; B21 = 1; B22 = 1;
            C11 = 1; C12 = 2; C21 = 9; C22 = 14;
        }
        public void UpdateA()
        {
            Random r = new Random();
            A11 =A11 - (r.NextDouble()/A11);
            A12 =A12 - (r.NextDouble()/A12);
            A21 =A21 - (r.NextDouble()/A21);
            A22 -=A22 -(r.NextDouble()/A22);
        }
        /*
        public void UpdateB()
        {
            Random r = new Random();
            B11 -= r.NextDouble()/B11;
            B12 -= r.NextDouble()/B12;
            B21 -= r.NextDouble()/B21;
            B22 -= r.NextDouble()/B22;
        }
        */
        public void UpdateC()
        {
            Random r = new Random();
            C11 -= r.NextDouble()/C11;
            C12 -= r.NextDouble()/C12;
            C21 -= r.NextDouble()/C21;
            C22 -= r.NextDouble()/C22;
        }
    }
}
