using System.Text;

namespace Zadanie1;
class Program
{
    /*
     Дана строка, содержащая n маленьких букв латинского алфавита. Требуется реализовать
    алгоритм компрессии этой строки, замещающий группы последовательно идущих
    одинаковых букв формой "sc" (где "s" – символ, "с" – количество букв в группе), а также
    алгоритм декомпрессии, возвращающий исходную строку по сжатой.
    */

    /*
       Данный метод я реализовал через StringBuilder т.к string это неизменяемый ссылочный тип данных,
       а строка в аргументе может иметь невероятно большой величины,что может привести к потери памяти и произодительности
    */
    public static string CompressString(string str)
    {
        if (string.IsNullOrEmpty(str)) // Проверяю если пустая строка или вообще null
            return "";

        StringBuilder result = new StringBuilder();
        int count = 1; //создаю счетчик для подсчета симловов подряд в группе

        for (int i = 1; i < str.Length; i++)
        {
            if (str[i] == str[i - 1])
            {
                count++;
            }
            else
            {
                result.Append(str[i - 1]);
                if (count == 1)
                    continue;
                else
                    result.Append(count);
                     count = 1;
            }
        }

        //После я добавляю последний символ и его количество
        result.Append(str[str.Length - 1]);
        if(count == 1)
            return result.ToString();   
        result.Append(count);

        return result.ToString();
    }
    static void Main(string[] args)
    {
        string test1 = CompressString("aaabbcce");
        string test2 = CompressString("ppooweuee ");
        string test3 = CompressString("abbeew ");
        Console.WriteLine(test1);
        Console.WriteLine(test2);
        Console.WriteLine(test3);
    }
}