using Microsoft.VisualStudio.TestTools.UnitTesting;
using PT1;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class ValidateUploadRequired
    {
        [TestMethod]
        public void IsUploadSufficient_Greaterthan_ReturnsTrue()
        {
            // Arrange.
            int processorUploadAvailable = 200;
            int taskUploadRequired = 100;
            PT1.Processor processor = new Processor();
            processor.setProcessorUpload(processorUploadAvailable);
            PT1.Task task = new Task();
            task.setTaskUpload(taskUploadRequired);
            Console.WriteLine("Processor Upload: " + processorUploadAvailable);
            Console.WriteLine("Task Upload: " + taskUploadRequired);

            // Act.
            bool actualResult = processor.IsUploadSufficient(task);

            // Assert.
            Assert.IsTrue(actualResult, "Actual result doesn`t match expected result");
        }

        [TestMethod]
        public void IsUploadSufficient_Equals_ReturnsTrue()
        {
            // Arrange.
            int processorUploadAvailable = 200;
            int taskUploadRequired = 200;
            PT1.Processor processor = new Processor();
            processor.setProcessorUpload(processorUploadAvailable);
            PT1.Task task = new Task();
            task.setTaskUpload(taskUploadRequired);
            Console.WriteLine("Processor Upload: " + processorUploadAvailable);
            Console.WriteLine("Task Upload: " + taskUploadRequired);

            // Act.
            bool actualResult = processor.IsUploadSufficient(task);

            // Assert.
            Assert.IsTrue(actualResult, "Actual result doesn`t match expected result");
        }

        [TestMethod]
        public void IsUploadSufficient_Lessthan_ReturnsTrue()
        {
            // Arrange.
            int processorUploadAvailable = 200;
            int taskUploadRequired = 300;
            PT1.Processor processor = new Processor();
            processor.setProcessorUpload(processorUploadAvailable);
            PT1.Task task = new Task();
            task.setTaskUpload(taskUploadRequired);
            Console.WriteLine("Processor Upload: " + processorUploadAvailable);
            Console.WriteLine("Task Upload: " + taskUploadRequired);

            // Act.
            bool actualResult = processor.IsUploadSufficient(task);

            // Assert.
            Assert.IsFalse(actualResult, "Actual result doesn`t match expected result");
        }
    }
}
