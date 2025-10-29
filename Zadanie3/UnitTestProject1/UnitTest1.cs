using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Zadanie3;

[TestClass]
public class LogProcessorTests
{
    [TestMethod]
    public void TestDateFormatter_GoodDate()
    {
        string input = "15.05.2023";
        string result = FormateDate(input);
        Assert.AreEqual("15-05-2023", result);
    }
    [TestMethod]
    public void TestDateFormatter_BadDate()
    {
        string input = "это не дата";
        string result = FormateDate(input);
        Assert.AreEqual("Не удалось распарсить дату", result);
    }
    [TestMethod]
    public void TestProcessFile_EmptyString()
    {
        string input = "";
        string result = ProcessFile(input);
        Assert.AreEqual("Пустая строка", result);
    }

    [TestMethod]
    public void TestProcessFile_SpaceFormat()
    {
        string input = "10.03.2025 15:14:49.523 INFORMATION Тест сообщение";

        string result = ProcessFile(input);

        Assert.IsTrue(result.Contains("10-03-2025"));
        Assert.IsTrue(result.Contains("INFO"));
        Assert.IsTrue(result.Contains("DEFAULT"));
    }
    [TestMethod]
    public void TestProcessFile_PipeFormat()
    {
        string input = "10.03.2025|15:14:49.523|INFORMATION|Тест сообщение";
        string result = ProcessFile(input);
        Assert.IsTrue(result.Contains("10-03-2025"));
        Assert.IsTrue(result.Contains("INFO"));
    }
}