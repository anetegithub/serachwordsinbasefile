using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using AutocompletionEngine;
using Search;
using System.Linq;

namespace SearchTest
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Search.Searcher s=new Searcher(new List<string>()
            {
                "5",
                "kare 10",
                "kanojo 20",
                "karetachi 1",
                "karosu 7",
                "sakura 3",
                "3",
                "k",
                "ka",
                "kar"
            });

            var searched= s.SearchInSource("ka");

            Assert.AreEqual(searched[0], "kanojo 20");
            Assert.AreEqual(searched[1], "kare 10");
            Assert.AreEqual(searched[2], "karosu 7");
            Assert.AreEqual(searched[3], "karetachi 1");
        }

        static Int32 CheckWeight(String s)
        {
            Int32 w = 0;
            if (s.Split(' ').Length > 1)
                w = Int32.Parse(s.Split(' ')[1]);
            return w;
        }
    }
}
