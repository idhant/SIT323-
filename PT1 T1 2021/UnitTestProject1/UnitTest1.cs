using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PT1;

namespace UnitTestProject1
{
    [TestClass]
    public class ValidateRamRequired
    {
        [TestMethod]
        public void IsRamSufficient_Greaterthan_ReturnsTrue()
        {
            // Arrange.
            int processorRamAvailable = 8;
            int taskRamRequired = 4;
            PT1.Processor processor = new Processor();
            processor.setProcessorRam(processorRamAvailable);
            PT1.Task task = new Task();
            task.setTaskRam(taskRamRequired);
            Console.WriteLine("Processor Ram: " + processorRamAvailable);
            Console.WriteLine("Task Ram: " + taskRamRequired);

            // Act.
            bool actualResult = processor.IsRamSufficient(task);

            // Assert.
            Assert.IsTrue(actualResult, "Actual result doesn`t match expected result");
        }

        [TestMethod]
        public void IsRamSufficient_Equals_ReturnsTrue()
        {
            // Arrange.
            int processorRamAvailable = 8;
            int taskRamRequired = 8;
            PT1.Processor processor = new Processor();
            processor.setProcessorRam(processorRamAvailable);
            PT1.Task task = new Task();
            task.setTaskRam(taskRamRequired);
            Console.WriteLine("Processor Ram: " + processorRamAvailable);
            Console.WriteLine("Task Ram: " + taskRamRequired);

            // Act.
            bool actualResult = processor.IsRamSufficient(task);

            // Assert.
            Assert.IsTrue(actualResult, "Actual result doesn`t match expected result");
        }

        [TestMethod]
        public void IsRamSufficient_Lessthan_ReturnsTrue()
        {
            // Arrange.
            int processorRamAvailable = 8;
            int taskRamRequired = 16;
            PT1.Processor processor = new Processor();
            processor.setProcessorRam(processorRamAvailable);
            PT1.Task task = new Task();
            task.setTaskRam(taskRamRequired);
            Console.WriteLine("Processor Ram: " + processorRamAvailable);
            Console.WriteLine("Task Ram: " + taskRamRequired);

            // Act.
            bool actualResult = processor.IsRamSufficient(task);

            // Assert.
            Assert.IsFalse(actualResult, "Actual result doesn`t match expected result");
        }
    }
}
