using Microsoft.VisualStudio.TestTools.UnitTesting;
using PT1;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace UnitTestProject1
{
    [TestClass]
    public class ValidateTaffFileFormat
    {
        [TestMethod]
        public void ValidateTAFFFile_FirstFile_ReturnsTrue()
        {
            // Arange.
            // Getting the TaskAllocation object
            PT1.TaskAllocations taskAllocations = new TaskAllocations();

            // Getting the filelines to be passed to the Validate method
            string pathOfFile = @"Programming Task 1 - Data Files\PT1 - Test1.taff";
            string currentPath = Directory.GetCurrentDirectory();
            Console.WriteLine("Current Path: " + currentPath);
            Console.WriteLine("Path of File: " + pathOfFile);

            Regex pathRegex = new Regex("bin.*");

            string rootPath = pathRegex.Replace(currentPath, "");
            Console.WriteLine("Retrieved root path: " + rootPath);

            pathOfFile = rootPath + pathOfFile;
            Console.WriteLine("Retrieved file path: " + pathOfFile);

            // storing the fillines in a string array
            taskAllocations.StoreTAFFLines(pathOfFile);

            string[] filelines = taskAllocations.getTAFFFileLines();
            
            // the expected result
            bool expectedResult = true;

            // Act.
            bool actualResult = taskAllocations.ValidateTAFFFile(filelines);

            // Assert.
            Assert.AreEqual(expectedResult, actualResult, "The actual value doesn`t match the expected value");
        }

        [TestMethod]
        public void ValidateTAFFFile_SecondFile_ReturnsTrue()
        {
            // Arange.
            // Getting the TaskAllocation object
            PT1.TaskAllocations taskAllocations = new TaskAllocations();

            // Getting the filelines to be passed to the Validate method
            string pathOfFile = @"Programming Task 1 - Data Files\PT1 - Test2.taff";
            string currentPath = Directory.GetCurrentDirectory();
            Console.WriteLine("Current Path: " + currentPath);
            Console.WriteLine("Path of File: " + pathOfFile);

            Regex pathRegex = new Regex("bin.*");

            string rootPath = pathRegex.Replace(currentPath, "");
            Console.WriteLine("Retrieved root path: " + rootPath);

            pathOfFile = rootPath + pathOfFile;
            Console.WriteLine("Retrieved file path: " + pathOfFile);

            // storing the fillines in a string array
            taskAllocations.StoreTAFFLines(pathOfFile);

            string[] filelines = taskAllocations.getTAFFFileLines();

            // the expected result
            bool expectedResult = true;

            // Act.
            bool actualResult = taskAllocations.ValidateTAFFFile(filelines);

            // Assert.
            Assert.AreEqual(expectedResult, actualResult, "The actual value doesn`t match the expected value");

        }

        [TestMethod]
        public void ValidateTAFFFile_ThirdFile_ReturnsTrue()
        {
            // Arange.
            // Getting the TaskAllocation object
            PT1.TaskAllocations taskAllocations = new TaskAllocations();

            // Getting the filelines to be passed to the Validate method
            string pathOfFile = @"Programming Task 1 - Data Files\PT1 - Test3.taff";
            string currentPath = Directory.GetCurrentDirectory();
            Console.WriteLine("Current Path: " + currentPath);
            Console.WriteLine("Path of File: " + pathOfFile);

            Regex pathRegex = new Regex("bin.*");

            string rootPath = pathRegex.Replace(currentPath, "");
            Console.WriteLine("Retrieved root path: " + rootPath);

            pathOfFile = rootPath + pathOfFile;
            Console.WriteLine("Retrieved file path: " + pathOfFile);

            // storing the fillines in a string array
            taskAllocations.StoreTAFFLines(pathOfFile);

            string[] filelines = taskAllocations.getTAFFFileLines();

            // the expected result
            bool expectedResult = true;

            // Act.
            bool actualResult = taskAllocations.ValidateTAFFFile(filelines);

            // Assert.
            Assert.AreEqual(expectedResult, actualResult, "The actual value doesn`t match the expected value");

        }

        [TestMethod]
        public void ValidateTAFFFile_FourthFile_ReturnsFalse()
        {
            // Arange.
            // Getting the TaskAllocation object
            PT1.TaskAllocations taskAllocations = new TaskAllocations();

            // Getting the filelines to be passed to the Validate method
            string pathOfFile = @"Programming Task 1 - Data Files\PT1 - Test4.taff";
            string currentPath = Directory.GetCurrentDirectory();
            Console.WriteLine("Current Path: " + currentPath);
            Console.WriteLine("Path of File: " + pathOfFile);

            Regex pathRegex = new Regex("bin.*");

            string rootPath = pathRegex.Replace(currentPath, "");
            Console.WriteLine("Retrieved root path: " + rootPath);

            pathOfFile = rootPath + pathOfFile;
            Console.WriteLine("Retrieved file path: " + pathOfFile);

            // storing the fillines in a string array
            taskAllocations.StoreTAFFLines(pathOfFile);

            string[] filelines = taskAllocations.getTAFFFileLines();

            // the expected result
            bool expectedResult = false;

            // Act.
            bool actualResult = taskAllocations.ValidateTAFFFile(filelines);

            // Assert.
            Assert.AreEqual(expectedResult, actualResult, "The actual value doesn`t match the expected value");

        }
    }
}
