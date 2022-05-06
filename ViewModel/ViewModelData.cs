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

namespace ViewModel 
{
    public class ViewModelData : INotifyPropertyChanged, IDataErrorInfo
    {
        public SplinesData Data { get; set; }
        public SpfList SpfList { get; set; }

        public int Uneven_N { get { return Data.Md.N; } set { Data.Md.N = value; OnPropertyChanged("Uneven_N"); } }
        public double Uneven_Start { get { return Data.Md.Start; } set { Data.Md.Start = value; OnPropertyChanged("Uneven_Start"); } }
        public double Uneven_End { get { return Data.Md.End; } set { Data.Md.End = value; OnPropertyChanged("Uneven_End"); } }
        public double[] Grid { get { return Data.Md.Grid; } set { Data.Md.Grid = value; OnPropertyChanged("Grid"); } }
        public double[] Measured { get { return Data.Md.Measured; } set { Data.Md.Measured = value; OnPropertyChanged("Measured"); } }
        public Spf Func { get { return Data.Md.Func; } set { Data.Md.Func = value; OnPropertyChanged("Func"); } }


        public int Uniform_N{ get { return Data.Sp.N; } set { Data.Sp.N = value; OnPropertyChanged("Uniform_N"); } }
        public double Uniform_Int_Start { get { return Data.Md.Int_Start; } set { Data.Md.Int_Start = value; OnPropertyChanged("Uniform_Int_Start"); } }
        public double Uniform_Int_End { get { return Data.Md.Int_End; } set { Data.Md.Int_End = value; OnPropertyChanged("Uniform_Int_End"); } }
        public double Uniform_Deriative_l { get { return Data.Sp.Deriative_l; } set { Data.Sp.Deriative_l = value; OnPropertyChanged("Uniform_Deriative_l"); } }
        public double Uniform_Deriative_r { get { return Data.Sp.Deriative_r; } set { Data.Sp.Deriative_r = value; OnPropertyChanged("Uniform_Deriative_r"); } }

        public bool SetErr()
        {
            return Data.Md.SetErr() || Data.Sp.SetErr();
        }

        public ViewModelData()
        {
            Data = new SplinesData();
            SpfList = new SpfList();
        }
        public bool Changed
        {
            get { return Data.Md.Changed; }
            set { Data.Md.Changed = value; }
        }

        public void MdSetGrid()
        {
            Data.Md.SetGrid();
            OnPropertyChanged(nameof(Data));
        }
        public double[] Splain(ref double a, ref double[] Int)
        {
            double[] r = Data.Splain(ref a, ref Int);
            OnPropertyChanged(nameof(Data));
            return r;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void Clear()
        {
            Data.Integral = null; Data.Der_l = null; Data.Der_r = null;
            for (int i = 0; i < Data.Md.N; i++)
            {
                Data.Md.Grid[i] = 0; Data.Md.Measured[i] = 0;
            }
            OnPropertyChanged(nameof(Data));
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
}