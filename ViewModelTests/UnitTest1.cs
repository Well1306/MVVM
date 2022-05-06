using Xunit;

namespace ViewModelTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var vmd = new ViewModel.ViewModelData();
            vmd.Clear();
            Assert.True(vmd.Data.Md.Iszeros);
        }
        [Fact]
        public void Test2()
        {
            var vmd = new ViewModel.ViewModelData();
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
            var vmd = new ViewModel.ViewModelData();
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

            
    }
}