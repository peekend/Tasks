using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

[TestClass]
public class ServerTests
{
    [TestInitialize]
    public void TestInitialize()
    {
        // Сбрасываем счётчик перед каждым тестом для чистоты
        Server.Reset();
    }

    [TestMethod]
    public void TestInitialCount()
    {
        // Проверяем, что начальное значение счётчика равно 0
        Assert.AreEqual(0, Server.GetCount());
    }

    [TestMethod]
    public void TestAddToCount()
    {
        // Добавляем 5 и проверяем результат
        Server.AddToCount(5);
        Assert.AreEqual(5, Server.GetCount());

        // Добавляем ещё 3
        Server.AddToCount(3);
        Assert.AreEqual(8, Server.GetCount());
    }

    [TestMethod]
    public void TestDecreaseCount()
    {
        // Сначала добавляем 10
        Server.AddToCount(10);

        // Уменьшаем на 3
        Server.DecreaseCount(3);
        Assert.AreEqual(7, Server.GetCount());

        // Уменьшаем ещё на 2
        Server.DecreaseCount(2);
        Assert.AreEqual(5, Server.GetCount());
    }

    [TestMethod]
    public void TestDecreaseBelowZero()
    {
        // Добавляем 5
        Server.AddToCount(5);

        // Уменьшаем на 10 (больше, чем есть), должно стать 0
        Server.DecreaseCount(10);
        Assert.AreEqual(0, Server.GetCount());
    }

    [TestMethod]
    public void TestReset()
    {
        // Добавляем значение
        Server.AddToCount(100);

        // Сбрасываем
        Server.Reset();
        Assert.AreEqual(0, Server.GetCount());
    }

    [TestMethod]
    public void TestConcurrentAddToCount()
    {
        // Простой тест на многопоточность: несколько задач добавляют значения
        // Ожидаем, что итоговое значение будет корректным благодаря ReaderWriterLockSlim
        Parallel.Invoke(
            () => Server.AddToCount(10),
            () => Server.AddToCount(20),
            () => Server.AddToCount(30)
        );

        Assert.AreEqual(60, Server.GetCount());
    }

    [TestMethod]
    public void TestConcurrentDecreaseCount()
    {
        // Сначала устанавливаем начальное значение
        Server.AddToCount(100);

        // Многопоточное уменьшение
        Parallel.Invoke(
            () => Server.DecreaseCount(10),
            () => Server.DecreaseCount(20),
            () => Server.DecreaseCount(30)
        );

        Assert.AreEqual(40, Server.GetCount());
    }

    [TestMethod]
    public void TestConcurrentMixedOperations()
    {
        // Смешанные операции: добавление и уменьшение в параллели
        Parallel.Invoke(
            () => Server.AddToCount(50),
            () => Server.DecreaseCount(10),
            () => Server.AddToCount(20),
            () => Server.DecreaseCount(5)
        );

        Assert.AreEqual(55, Server.GetCount()); // 50 + 20 - 10 - 5 = 55
    }
}
