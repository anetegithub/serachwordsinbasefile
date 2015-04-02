using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

namespace SearchService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class SearchServiceMain : ISearchServiceMain
    {
        public static List<String> Source;
        public static String PathToFile;

        public String GetData(String prefix)
        {
            //magic string
            //return String.Join("\r\n", new Search.Searcher(Source ?? LoadFile(PathToFile)).SearchInSource(prefix).Take(10).ToArray());

            if (Source == null)
                if (PathToFile != "")
                    Source = LoadFile(PathToFile);
                else
                    return "Data loading trouble";

            Search.Searcher S = new Search.Searcher(Source);

            String[] ArrResult = S.SearchInSource(prefix).Take(10).ToArray();

            String StrResult = String.Join("\r\n", ArrResult);

            return StrResult;
        }

        public String SetPath(String Path)
        {
            PathToFile = Path;
            return "Source path set succefull!";
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
}
