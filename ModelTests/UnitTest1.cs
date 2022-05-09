using Xunit;
using System;
using System.Linq;

namespace ModelTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var md = new Model.MeasuredData(3, 0.001, 1, Model.Spf.Linear);
            md.SetGrid();
            Assert.Equal(0.001, md.Start, 10);
            Assert.Equal(1, md.End, 10);
            Assert.Equal(3, md.N);
            Assert.Equal(md.Grid[0], md.Measured[0], 10);
            Assert.Equal(md.Grid[1], md.Measured[1], 10);
            Assert.Equal(md.Grid[2], md.Measured[2], 10);
        }
        [Fact]
        public void Test2()
        {
            var md = new Model.MeasuredData(3, 0.001, 1, Model.Spf.Cubic);
            md.SetGrid();
            Assert.Equal(0.001, md.Start, 10);
            Assert.Equal(1, md.End, 10);
            Assert.Equal(3, md.N);
            Assert.Equal(Math.Pow(md.Grid[0],3), md.Measured[0], 10);
            Assert.Equal(Math.Pow(md.Grid[1],3), md.Measured[1], 10);
            Assert.Equal(Math.Pow(md.Grid[2],3), md.Measured[2], 10);
        }
        [Fact]
        public void Test3()
        {
            var md = new Model.MeasuredData(3, 0.001, 1, Model.Spf.Cubic);
            var sp = new Model.SplineParameters();
            var sd = new Model.SplinesData(md, sp);
            double a = 0;
            double[] Int = new double[1];
            sd.Splain(ref a, ref Int);
            Assert.Equal(0, a, 10);
        }
    }
}