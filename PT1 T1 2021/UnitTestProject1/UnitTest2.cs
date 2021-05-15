using Microsoft.VisualStudio.TestTools.UnitTesting;
using PT1;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class ValidateDownloadRequired
    {
        [TestMethod]
        public void IsDownloadSufficient_Greaterthan_ReturnsTrue()
        {
            // Arrange.
            int processorDownloadAvailable = 200;
            int taskDownloadRequired = 100;
            PT1.Processor processor = new Processor();
            processor.setProcessorDownload(processorDownloadAvailable);
            PT1.Task task = new Task();
            task.setTaskDownload(taskDownloadRequired);
            Console.WriteLine("Processor Download: " + processorDownloadAvailable);
            Console.WriteLine("Task Download: " + taskDownloadRequired);

            // Act.
            bool actualResult = processor.IsDownloadSufficient(task);

            // Assert.
            Assert.IsTrue(actualResult, "Actual result doesn`t match expected result");
        }

        [TestMethod]
        public void IsDownloadSufficient_Equals_ReturnsTrue()
        {
            // Arrange.
            int processorDownloadAvailable = 200;
            int taskDownloadRequired = 200;
            PT1.Processor processor = new Processor();
            processor.setProcessorDownload(processorDownloadAvailable);
            PT1.Task task = new Task();
            task.setTaskDownload(taskDownloadRequired);
            Console.WriteLine("Processor Download: " + processorDownloadAvailable);
            Console.WriteLine("Task Download: " + taskDownloadRequired);

            // Act.
            bool actualResult = processor.IsDownloadSufficient(task);

            // Assert.
            Assert.IsTrue(actualResult, "Actual result doesn`t match expected result");
        }

        [TestMethod]
        public void IsDownloadSufficient_Lessthan_ReturnsTrue()
        {
            // Arrange.
            int processorDownloadAvailable = 200;
            int taskDownloadRequired = 300;
            PT1.Processor processor = new Processor();
            processor.setProcessorDownload(processorDownloadAvailable);
            PT1.Task task = new Task();
            task.setTaskDownload(taskDownloadRequired);
            Console.WriteLine("Processor Download: " + processorDownloadAvailable);
            Console.WriteLine("Task Download: " + taskDownloadRequired);

            // Act.
            bool actualResult = processor.IsDownloadSufficient(task);

            // Assert.
            Assert.IsFalse(actualResult, "Actual result doesn`t match expected result");
        }
    }
}
