using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using AutocompletionEngine;

namespace Search
{
    internal sealed class Program
    {
        static void Main(String[] args)
        {
            Searcher s = new Searcher(LoadFile(Directory.GetCurrentDirectory() + "\\test.in"));
            foreach (var str in s.SearchInSource(Console.ReadLine()).Take(10))
                Console.WriteLine(str.Split(' ')[0]);
        }

        static List<String> LoadFile(string path)
        {
            List<String> Possible = new List<String>();
            StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                Possible.Add(sr.ReadLine());
            }
            return Possible;
        }
    }

    public class Searcher
    {
        private readonly List<String> Source = new List<String>();

        public Searcher(List<String> Source)
        {
            this.Source = Source;
        }

        public List<String> SearchInSource(string Search)
        {
            Processor p = new Processor(Source);
            return OrderByWeight(p.Search(Search));
        }

        private List<String> OrderByWeight(IList<String> Source)
        {
            return Source.OrderByDescending(x => CheckWeight(x)).ToList();
        }

        private Int32 CheckWeight(String s)
        {
            Int32 w = 0;
            if (s.Split(' ').Length > 1)
                w = Int32.Parse(s.Split(' ')[1]);
            return w;
        }
    }
}