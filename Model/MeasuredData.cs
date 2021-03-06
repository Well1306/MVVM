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
    public enum Spf
    {
        Linear,
        Cubic,
        Random
    }
    public class MeasuredData
    {
        public int N { get; set; }
        public double Start { get; set; }
        public double End { get; set; }
        public Spf Func { get; set; }
        public double[] Grid { get; set; }
        public double[] Measured { get; set; }
        public double Int_Start { get; set; }
        public double Int_End { get; set; }
        public bool Iszeros
        {
            get
            {
                for (int i = 0; i < Grid.Length; i++)
                {
                    if (!((Grid[i] == 0) && (Measured[i] == 0)))
                        return false;
                }
                return true;
            }
        }
        public MeasuredData()
        {
            N = 10; Start = 0; End = 1; Func = Spf.Linear;
            Int_Start = 0; Int_End = 1;
            Grid = new double[N]; Measured = new double[N];
        }
        public MeasuredData(int n, double s, double e, Spf f)
        {
            N = n; Start = s; End = e; Func = f;
            Int_Start = 0; Int_End = 1;
            Grid = new double[N]; Measured = new double[N];
        }
        public void SetGrid()
        {
            Grid = new double[N];
            Measured = new double[N];
            int s_int = (int)Start;
            int e_int = (int)End;
            Grid[0] = Start; Grid[N - 1] = End;
            Random rnd = new();
            for (int i = 1; i < N - 1; i++) Grid[i] = rnd.Next(s_int, e_int) + rnd.NextDouble();
            Array.Sort(Grid);
            switch (Func)
            {
                case Spf.Random:
                    for (int i = 0; i < N; i++) Measured[i] = 13 * rnd.NextDouble();
                    break;
                case Spf.Linear:
                    for (int i = 0; i < N; i++) Measured[i] = Grid[i];
                    break;
                case Spf.Cubic:
                    for (int i = 0; i < N; i++) Measured[i] = Math.Pow(Grid[i], 3);
                    break;
            }
        }

        public bool SetErr()
        {
            return (N <= 2) || (Start >= End) || !(Start <= Int_Start && Int_Start < Int_End && Int_End <= End);
        }
        public ObservableCollection<string>? _str = new();

        public override string ToString()
        {
            string res = $"[{Start}, {End}] count: {N}\n";
            for (int i = 0; i < N - 1; i++)
                res += $"{Grid[i]}, ";
            res += $"{Grid[N - 1]}\n\n\n";
            for (int i = 0; i < N - 1; i++)
                res += $"{Measured[i]}, ";
            res += $"{Measured[N - 1]}\n";
            return res;
        }

    }
}