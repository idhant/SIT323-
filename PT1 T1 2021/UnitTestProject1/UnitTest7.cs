using Microsoft.VisualStudio.TestTools.UnitTesting;
using PT1;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace UnitTestProject1
{
    [TestClass]
    public class ValidateCffFileErrors
    {
        [TestMethod]
        public void ValidateCFFFileErrors_FirstErrorDetected_ReturnsTrue()
        {
            // Arrange.
            // Getting the Configuration object
            PT1.Configuration configuration = new Configuration();

            // Getting the filelines to be passed to the Validate method
            string pathOfFile = @"Programming Task 1 - Data Files\PT1 - Test4.cff";
            string currentPath = Directory.GetCurrentDirectory();
            Console.WriteLine("Current Path: " + currentPath);
            Console.WriteLine("Path of File: " + pathOfFile);

            Regex pathRegex = new Regex("bin.*");

            string rootPath = pathRegex.Replace(currentPath, "");
            Console.WriteLine("Retrieved root path: " + rootPath);

            pathOfFile = rootPath + pathOfFile;
            Console.WriteLine("Retrieved file path: " + pathOfFile);

            // storing the fillines in a string array
            configuration.StoreCFFLines(pathOfFile);

            string[] filelines = configuration.GetCFFFileLines();

            // expected error
            string expectedError = "TOTAL-RAM=987";
            Console.WriteLine("Expected Error: " + expectedError);

            // Validating the file, if errors are found they are stored in the list
            configuration.ValidateCFFFile(filelines);

            // Act.
            bool errorFound = configuration.ValidateCFFFileErrors(expectedError);

            // Assert.
            Assert.IsTrue(errorFound, "Expected error " + expectedError + " not found");

        }

        [TestMethod]
        public void ValidateCFFFileErrors_SecondErrorDetected_ReturnsTrue()
        {
            // Arrange.
            // Getting the Configuration object
            PT1.Configuration configuration = new Configuration();

            // Getting the filelines to be passed to the Validate method
            string pathOfFile = @"Programming Task 1 - Data Files\PT1 - Test4.cff";
            string currentPath = Directory.GetCurrentDirectory();
            Console.WriteLine("Current Path: " + currentPath);
            Console.WriteLine("Path of File: " + pathOfFile);

            Regex pathRegex = new Regex("bin.*");

            string rootPath = pathRegex.Replace(currentPath, "");
            Console.WriteLine("Retrieved root path: " + rootPath);

            pathOfFile = rootPath + pathOfFile;
            Console.WriteLine("Retrieved file path: " + pathOfFile);

            // storing the fillines in a string array
            configuration.StoreCFFLines(pathOfFile);

            string[] filelines = configuration.GetCFFFileLines();

            // expected error
            string expectedError = "Gbps=123";
            Console.WriteLine("Expected Error: " + expectedError);


            // Validating the file, if errors are found they are stored in the list
            configuration.ValidateCFFFile(filelines);

            // Act.
            bool errorFound = configuration.ValidateCFFFileErrors(expectedError);

            // Assert.
            Assert.IsTrue(errorFound, "Expected error " + expectedError + " not found");

        }

    }
}
