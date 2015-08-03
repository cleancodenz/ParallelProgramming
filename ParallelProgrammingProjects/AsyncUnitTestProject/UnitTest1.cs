
using System;
using System.Threading.Tasks;
using AsyncClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AsyncUnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        /**
         * Using Task.Wait and Task.Result, wrong way to expect exception, it is aggregateexception
         * **/
        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void WrongWay1()
        {
            Task<int> task = MyClass.Divide(4, 0);
         
            task.Wait();
         
            Assert.AreEqual(2, task.Result);
        }


        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public async Task  RightWayForWrongWay1()
        {
            await MyClass.Divide(4, 0);
        }

        /**
       * Using await, the task continuation on another thread, as unit test is like console app, but it works in latest vs
        async Task instead if async void which is not picked up by task runner
       *  * **/
        [TestMethod]
        public async void WrongWay2()
        {
            int result = await MyClass.Divide(4, 2);
            Assert.AreEqual(13, result);

        }

        [TestMethod]
        public async Task RightWayForWrongWay2()
        {
            int result = await MyClass.Divide(4, 2);
            Assert.AreEqual(13, result);

        }
        
    }
}
