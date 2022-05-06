using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using ViewModel;

namespace View
{
    public class ViewData : INotifyPropertyChanged
    {
        public ChartData Chart { get; set; }
        public ViewModelData Data { get; set; }

        public ViewData()
        {
            Data = new ViewModelData();
            Chart = new ChartData();
        }

        public bool Changed
        {
            get { return Data.Changed; }
            set { Data.Changed = value; }
        }

        public void MdSetGrid()
        {
            Data.MdSetGrid();
            OnPropertyChanged(nameof(Data));
        }
        public double[] Splain(ref double a, ref double[] Int)
        {
            OnPropertyChanged(nameof(Data));
            return Data.Splain(ref a, ref Int);
        }
        public void Measured_on_Chart()
        {
            Data.Func = Data.SpfList.selectedFunc.func;
            Data.Changed = true;
            Data.MdSetGrid();
            Chart.AddPlot(Data.Grid, Data.Measured, 2, "Points");
        }

        public void Splain_on_Chart()
        {
            double a = 213123;
            double[] Int = new double[1];
            double[] r = Data.Splain(ref a, ref Int);
           
            double[] res = new double[Data.Uniform_N];
            for (int i = 0; i < res.Length; i++)
                res[i] = r[0 + 3 * i];
            double[] grid = new double[Data.Uniform_N];
            for (int i = 0; i < Data.Uniform_N; i++)
                grid[i] = Data.Uneven_Start + i * (Data.Uneven_End - Data.Uneven_Start) / (Data.Uniform_N - 1);


            Chart.AddPlot(grid, res, 1, "Splain1");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        internal void Clear()
        {
            Chart.Clear();
            Data.Clear();
        }
    }
}
