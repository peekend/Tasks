using System.Globalization;
using System.Text;

namespace Zadanie3;
public class Program
{
    public static void MakeFile(string str)
    {
        string filePath = @"C:\Users\nanos\source\repos\Zadanie3\Zadanie3\Problems\";
        string content = str;
        File.WriteAllText(filePath, content);
    }
    public static string FormateDate(string str)
    {
        try
        {
            if (DateTime.TryParse(str, out DateTime date))
            {
                string formattedDate = date.ToString("dd-MM-yyyy");
                return formattedDate;
            }
            else
            {
                MakeFile(str);
                throw new Exception("Ошибка в данных");
            }
        }
        catch
        {
            MakeFile(str);
            return "Не удалось распарсить дату";
        }
    }
    public static string ProcessFile(string str)
    {
        string[] strArray = null;
        string result = null;
        if (string.IsNullOrEmpty(str))
            return "Пустая строка";
        // Проверяем оба формата
        bool hasSpaceFormat = str.Contains(" ") && str.Split(' ').Length >= 3;
        bool hasPipeFormat = str.Contains("|") && str.Split('|').Length >= 4;
        bool hasDate = System.Text.RegularExpressions.Regex.IsMatch(str, @"\d{2}\.\d{2}\.\d{4}");

        try
        {
            if ((hasSpaceFormat || hasPipeFormat) && hasDate)
            {
                strArray = str.Split('|', ' ');
                strArray[0] = FormateDate(strArray[0]);
                if (strArray[2] == "INFORMATION")
                    strArray[2] = "INFO";
                else if (strArray[2] == "WARNING")
                    strArray[2] = "WARN";
                if (strArray[3] != "ProcessFile")
                    strArray[3] = "DEFAULT";
                result = string.Join("\t", strArray);
                return result;

            }
        }
        catch (Exception ex)
        {
            MakeFile(str + ex.Message);
            return "Создан файл!";
        }
        return result;
    }
    static void Main(string[] args)
    {
        string test1 = ProcessFile("10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'");
        string test2 = ProcessFile("2025-03-10 15:14:51.5882| INFO|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'");
        Console.WriteLine(test1);
    }
}


