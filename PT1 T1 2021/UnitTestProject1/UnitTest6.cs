using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PT1;
using System.IO;
using System.Text.RegularExpressions;

namespace UnitTestProject1
{
    [TestClass]
    public class ValidateTaffFileErrors
    {
        [TestMethod]
        public void ValidateTAFFFileErrors_FirstErrorDetected_ReturnsTrue()
        {
            // Arrange.
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

            // expected error
            string expectedError = "...";
            Console.WriteLine("Expected Error: " + expectedError);

            // assertion variable
            bool errorFound = false;

            // Validating the file, if errors are found they are stored in the list
            taskAllocations.ValidateTAFFFile(filelines);

            // Act.
            foreach(string error in taskAllocations.getErrorList())
            {
                if (error == expectedError)
                {
                    errorFound = true;
                }
            }

            // Assert.
            Assert.IsTrue(errorFound, "Expected error " + expectedError + " not found");
        }

        [TestMethod]
        public void ValidateTAFFFileErrors_SecondErrorDetected_ReturnsTrue()
        {
            // Arrange.
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

            // expected error
            string expectedError = "LOCATIONS";
            Console.WriteLine("Expected Error: " + expectedError);

            // assertion variable
            bool errorFound = false;

            // Validating the file, if errors are found they are stored in the list
            taskAllocations.ValidateTAFFFile(filelines);

            // Act.
            foreach (string error in taskAllocations.getErrorList())
            {
                if (error == expectedError)
                {
                    errorFound = true;
                }
            }

            // Assert.
            Assert.IsTrue(errorFound, "Expected error " + expectedError + " not found");
        }

        [TestMethod]
        public void ValidateTAFFFileErrors_ThirdErrorDetected_ReturnsTrue()
        {
            // Arrange.
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

            // expected error
            string expectedError = "ID=RICE";
            Console.WriteLine("Expected Error: " + expectedError);

            // assertion variable
            bool errorFound = false;

            // Validating the file, if errors are found they are stored in the list
            taskAllocations.ValidateTAFFFile(filelines);

            // Act.
            foreach (string error in taskAllocations.getErrorList())
            {
                if (error == expectedError)
                {
                    errorFound = true;
                }
            }

            // Assert.
            Assert.IsTrue(errorFound, "Expected error " + expectedError + " not found");
        }

    }
}
