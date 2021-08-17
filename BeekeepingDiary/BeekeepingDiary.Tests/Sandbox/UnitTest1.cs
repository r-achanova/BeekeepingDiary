using System;
using Xunit;

namespace BeekeepingDiary.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var a = 2;
            var b = 5;
            var sum = a + b;
            Assert.Equal(7, sum);
        }
    }
}
