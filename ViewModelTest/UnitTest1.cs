using Xunit;
using ViewModel;

namespace ViewModelTests
{
    public class UnitTest2
    {
        [Fact]
        public void Test1()
        {
            var vmd = new ViewModelData();
            vmd.ClearCommand.Execute(1);
            bool b = true;
            for (int i = 0; i < vmd.Uneven_N; i++)
            {
                if (!((vmd.Uneven_Grid[i] == 0) && (vmd.Uneven_Measured[i] == 0)))
                    b = b && false;
            }
            Assert.True(b);
        }
        [Fact]
        public void Test2()
        {
            var vmd = new ViewModelData();
            vmd.Uneven_N = 5;
            Assert.Equal(5, vmd.Data.Md.N);
            vmd.Uneven_Start = 12;
            Assert.Equal(12, vmd.Data.Md.Start);
            vmd.Uneven_End = 100;
            Assert.Equal(100, vmd.Data.Md.End);
            vmd.Uniform_N = 100;
            Assert.Equal(100, vmd.Data.Sp.N);
        }
        [Fact]
        public void Test3()
        {
            var vmd = new ViewModelData();
            vmd.Uneven_Start = 12;
            Assert.Equal(12, vmd.Data.Md.Start);
            vmd.Uneven_End = 100;
            Assert.Equal(100, vmd.Data.Md.End);
            vmd.Uniform_Int_Start = 1;
            Assert.Equal(1, vmd.Data.Md.Int_Start);
            vmd.Uniform_Int_End = 3;
            Assert.Equal(3, vmd.Data.Md.Int_End);
            Assert.True(vmd.SetErr());
        }
        [Fact]
        public void Test4()
        {
            var vmd = new ViewModelData();
            vmd.Uneven_N = 2;
            Assert.False(vmd.MeasuredDataCommand.CanExecute(1));
            Assert.False(vmd.SplinesCommand.CanExecute(1));
        }
        [Fact]
        public void Test5()
        {
            var vmd = new ViewModelData();
            vmd.Uneven_N = 20;
            Assert.True(vmd.MeasuredDataCommand.CanExecute(1));
            Assert.True(vmd.SplinesCommand.CanExecute(1));
        }
    }
}