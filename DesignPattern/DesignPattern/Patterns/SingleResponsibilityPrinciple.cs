using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns
{
    /*
     * Single Responsibility Principle
     * 1 Class focus should be on 1 Responsibility
     * All method should relate to Journal
     */
    public class Journal
    {
        private readonly List<string> entries = new List<string>();

        private static int count = 0;
        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count; // memento
        }

        public void RemoveEntry(int index)
        {
            // Not a stable way to remove entries because remaining entries indexies become invalid
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

        //forces responsibility of persistance on Journal Violates Single Responsibility Principle
        public void save(string filename)
        {
            File.WriteAllText(filename, ToString());
        }
        //forces responsibility of persistance on Journal Violates Single Responsibility Principle
        public static Journal Load(string filename)
        {
            Journal j = new Journal();
            return j;
        }
        //forces responsibility of persistance on Journal Violates Single Responsibility Principle
        public void Load(Uri uri)
        {

        }
    }

    /*
     * Single Responsibility Principle
     * Class focuses on the Presistence of the a given class
     */
    public class Persistence
    {
        public void SaveToFile(Journal j, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
                File.WriteAllText(filename, j.ToString());
        }
    }
    public class SingleResponsibilityPrinciple
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I cried today");
            j.AddEntry("I ate a bug");
            WriteLine(j);

            var p = new Persistence();
            var filename = @"c:\temp\journal.txt";
            p.SaveToFile(j, filename, true);
            Process.Start(filename);

        }
    }
}