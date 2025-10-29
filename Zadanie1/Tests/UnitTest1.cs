using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace Tests
{
    [TestClass]
    public class StringCompressionTests
    {
        [TestMethod]
        public void Test_EmptyString_ReturnsEmpty()
        {
            Assert.AreEqual("", CompressString(""));
            Assert.AreEqual("", CompressString(null));
        }

        [TestMethod]
        public void Test_SingleCharacter_ReturnsSame()
        {
            Assert.AreEqual("a", CompressString("a"));
            Assert.AreEqual("z", CompressString("z"));
        }

        [TestMethod]
        public void Test_NoRepeatingCharacters_ReturnsSame()
        {
            Assert.AreEqual("abc", CompressString("abc"));
            Assert.AreEqual("xyz", CompressString("xyz"));
        }

        [TestMethod]
        public void Test_RepeatingCharacters_SingleOccurrence()
        {
            // "aa" -> "a2"
            Assert.AreEqual("a2", CompressString("aa"));
            // "bbb" -> "b3"
            Assert.AreEqual("b3", CompressString("bbb"));
        }

        [TestMethod]
        public void Test_RepeatingCharacters_Mixed()
        {
            // "aabcc" -> "a2b1c2"
            Assert.AreEqual("a2bc2", CompressString("aabcc"));
            // "aaabbb" -> "a3b3"
            Assert.AreEqual("a3b3", CompressString("aaabbb"));
        }

        [TestMethod]
        public void Test_LongRepeatingSequence()
        {
            var input = new string('x', 100);
            Assert.AreEqual("x100", CompressString(input));
        }

        private string CompressString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            StringBuilder result = new StringBuilder();
            int count = 1;
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == str[i - 1])
                {
                    count++;
                }
                else
                {
                    result.Append(str[i - 1]);
                    if (count > 1)
                        result.Append(count);
                    count = 1;
                }
            }
            result.Append(str[str.Length - 1]);
            if (count > 1)
                result.Append(count);

            return result.ToString();
        }
    }
}
