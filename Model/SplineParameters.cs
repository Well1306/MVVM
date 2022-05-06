using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Model
{
    public class SplineParameters 
    {
        public int N { get; set; }
        public double Deriative_l { get; set; }
        public double Deriative_r { get; set; }
        public bool Err { get; set; }
        public SplineParameters()
        {
            N = 10;
        }
        public SplineParameters(double a, double b)
        {
            N = 10;
            Deriative_l = a; Deriative_r = b;
        }

        public bool SetErr()
        {
            return Err = (N <= 2);
        }
    }
}
