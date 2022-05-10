using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using Model;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace ViewModel 
{
    public class ViewModelData : INotifyPropertyChanged, IDataErrorInfo
    {
        public SplinesData Data { get; set; }
        public SpfList SpfList { get; set; }
        public ChartData Chart { get; set; }

        public int Uneven_N { get { return Data.Md.N; } set { Data.Md.N = value; OnPropertyChanged("Uneven_N"); } }
        public double Uneven_Start { get { return Data.Md.Start; } set { Data.Md.Start = value; OnPropertyChanged("Uneven_Start"); } }
        public double Uneven_End { get { return Data.Md.End; } set { Data.Md.End = value; OnPropertyChanged("Uneven_End"); } }
        public double[] Uneven_Grid { get { return Data.Md.Grid; } set { Data.Md.Grid = value; OnPropertyChanged("Uneven_Grid"); } }
        public double[] Uneven_Measured { get { return Data.Md.Measured; } set { Data.Md.Measured = value; OnPropertyChanged("Uneven_Measured"); } }
        public Spf Func { get { return Data.Md.Func; } set { Data.Md.Func = value; OnPropertyChanged("Func"); } }


        public int Uniform_N{ get { return Data.Sp.N; } set { Data.Sp.N = value; OnPropertyChanged("Uniform_N"); } }
        public double Uniform_Int_Start { get { return Data.Md.Int_Start; } set { Data.Md.Int_Start = value; OnPropertyChanged("Uniform_Int_Start"); } }
        public double Uniform_Int_End { get { return Data.Md.Int_End; } set { Data.Md.Int_End = value; OnPropertyChanged("Uniform_Int_End"); } }
        public double Uniform_Deriative_l { get { return Data.Sp.Deriative_l; } set { Data.Sp.Deriative_l = value; OnPropertyChanged("Uniform_Deriative_l"); } }
        public double Uniform_Deriative_r { get { return Data.Sp.Deriative_r; } set { Data.Sp.Deriative_r = value; OnPropertyChanged("Uniform_Deriative_r"); } }
        public double[] SplineGrid { get; set; }
        public double[] SplineMeasured { get; set; }

        public string IntInfo { get { return Data.IntInfo; } }

        public ObservableCollection<string>? Str { get { return Data.Str; } }
        public bool SetErr()
        {
            return Data.Md.SetErr() || Data.Sp.SetErr();
        }

        public bool IsZeros()
        {
            return Data.Md.Iszeros;
        }
        public ViewModelData()
        {
            Data = new SplinesData();
            SpfList = new SpfList();
            Chart = new ChartData();
            SplineGrid = new double[1];
            SplineMeasured = new double[1];
        }

        private RelayCommand measuredDataCommand;
        public RelayCommand MeasuredDataCommand
        {
            get
            {
                return measuredDataCommand ??
                    (measuredDataCommand = new RelayCommand(obj =>
                    {
                        Data.Md.Func = SpfList.selectedFunc.func;
                        Data.Md.SetGrid();
                        Chart.AddPlot(Uneven_Grid, Uneven_Measured, 2, "Points");
                        OnPropertyChanged(nameof(Str));
                        OnPropertyChanged(nameof(Data));
                        OnPropertyChanged(nameof(Chart));
                    },
                    (obj) => !SetErr()));
            }
        }
        private RelayCommand splinesCommand;
        public RelayCommand SplinesCommand
        {
            get
            {
                return splinesCommand ??
                    (splinesCommand = new RelayCommand(obj =>
                    {
                        double a = 123;
                        double[] Int = new double[1];
                        double[] r = Data.Splain(ref a, ref Int);
                        SplineGrid = new double[Uniform_N];
                        for (int i = 0; i < Uniform_N; i++)
                            SplineGrid[i] = Uneven_Start + i * (Uneven_End - Uneven_Start) / (Uniform_N - 1);
                        SplineMeasured = new double[Uniform_N];
                        for (int i = 0; i < SplineMeasured.Length; i++)
                            SplineMeasured[i] = r[0 + 3 * i];
                        Chart.AddPlot(SplineGrid, SplineMeasured, 1, "Spline");
                        OnPropertyChanged(nameof(IntInfo));
                        OnPropertyChanged(nameof(Data));
                        OnPropertyChanged(nameof(Chart));
                    },
                    (obj) => !SetErr() && !IsZeros()));
            }
        }
        private RelayCommand clearCommand;
        public RelayCommand ClearCommand
        {
            get
            {
                return clearCommand ??
                    (clearCommand = new RelayCommand(obj =>
                    {
                        Data.Integral = null; Data.Der_l = null; Data.Der_r = null;
                        for (int i = 0; i < Data.Md.N; i++)
                        {
                            Data.Md.Grid[i] = 0; Data.Md.Measured[i] = 0;
                        }
                        Chart.Clear();
                        OnPropertyChanged(nameof(Str));
                        OnPropertyChanged(nameof(IntInfo));
                        OnPropertyChanged(nameof(Data));
                    }));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

       
        public string this[string columnName]
        {
            get
            {
                string err = "";
                switch (columnName)
                {
                    case "Uneven_N":
                        if (Uneven_N <= 2)
                            err = "Число точек должно быть больше 2";
                        break;
                    case "Uneven_End":
                        if (Uneven_Start >= Uneven_End || !(Uneven_Start <= Uniform_Int_Start && Uniform_Int_Start < Uniform_Int_End && Uniform_Int_End <= Uneven_End))
                            err = "Правый конец меньше левого";
                        break;
                    case "Uneven_Start":
                        if (Uneven_Start >= Uneven_End || !(Uneven_Start <= Uniform_Int_Start && Uniform_Int_Start < Uniform_Int_End && Uniform_Int_End <= Uneven_End))
                            err = "Правый конец меньше левого";
                        break;
                    case "Uniform_N":
                        if (Uniform_N <= 2)
                            err = "Число точек должно быть больше 2";
                        break;
                    case "Uniform_Int_Start":
                        if (Uniform_Int_Start >= Uniform_Int_End || !(Uneven_Start <= Uniform_Int_Start && Uniform_Int_Start < Uniform_Int_End && Uniform_Int_End <= Uneven_End))
                            err = "Неверный отрезок для интеграла";
                        break;
                    case "Uniform_Int_End":
                        if (Uniform_Int_Start >= Uniform_Int_End || !(Uneven_Start <= Uniform_Int_Start && Uniform_Int_Start < Uniform_Int_End && Uniform_Int_End <= Uneven_End))
                            err = "Неверный отрезок для интеграла";
                        break;
                    default:
                        err = "";
                        break;
                }
                return err;
            }
        }
        public string Error => throw new NotImplementedException();

    }

    public class ChartData
    {
        public SeriesCollection Sc { get; set; }
        public Func<double, string> Form { get; set; }
        public ChartData()
        {
            Sc = new();
            Form = value => value.ToString("F4");
        }

        public void AddPlot(double[] grid, double[] measured, int mode, string title)
        {
            ChartValues<ObservablePoint> Values = new ChartValues<ObservablePoint>();
            for (int i = 0; i < measured.Length; i++)
            {
                Values.Add(new(grid[i], measured[i]));
            }
            if (mode == 1) //splain
                Sc.Add(new LineSeries { Title = title, Values = Values, PointGeometry = null });
            else
                Sc.Add(new ScatterSeries { Title = title, Values = Values });
        }
        public void Clear()
        {
            Sc.Clear();
        }
    }
}