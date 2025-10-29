using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading;

/*
 * Есть "сервер" в виде статического класса.
У него есть переменная count (тип int) и два метода, которые позволяют эту переменную
читать и писать: GetCount() и AddToCount(int value).
К классу–"серверу" параллельно обращаются множество клиентов, которые в основном
читают, но некоторые добавляют значение к count.
Нужно реализовать статический класс с методами GetCount / AddToCount так, чтобы:
читатели могли читать параллельно, не блокируя друг друга;
писатели писали только последовательно и никогда одновременно;
пока писатели добавляют и пишут, читатели должны ждать окончания записи.
*/
/*
 * Задача на многопоточное программирование и необходимо читать ресурсы параллельно,но блокировать потоки пока не закончится запись.
 * Я изучил и выяснил,что с этой задачой справится класс ReaderWriterLockSlim
*/
namespace Zadanie2
{
    internal static class Server
    {
        private static int count_;
        private static readonly ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
        public static int GetCount()
        {
            // Чтение — допускается параллельно всеми читателями
            rwLock.EnterReadLock();
            try
            {
                return count_;
            }
            finally
            {
                rwLock.ExitReadLock();
            }
        }
        public static void AddToCount(int value)
        {
            // Запись — блокирует всех, кроме текущего потока
            rwLock.EnterWriteLock();
            try
            {
                count_ += value;
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }
        public static void DecreaseCount(int value)
        {
            rwLock.EnterWriteLock();
            try
            {
                count_ -= value;
                if (count_ < 0)
                {
                    count_ = 0; // предотвращение отрицательного значения, по необходимости
                }
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }
        public static void Reset()
        {
            rwLock.EnterWriteLock();
            try
            {
                count_ = 0;
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }
    }
}
