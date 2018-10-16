using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

/*
 * Single Responsibility Principle: Every module or class should have responsibility over a single part of the functionality. 
 * That responsibility should be entirely encapsulated by the class. 
 */
namespace DesignPattern.Patterns.Principles
{
    class SingleResponsibilityPrinciple
    {
        // just stores a couple of journal entries and ways of
        // working with them
        public class Journal
        {
            private readonly List<string> entries = new List<string>();

            private static int count = 0;

            public int AddEntry(string text)
            {
                entries.Add($"{++count}: {text}");
                return count; // memento pattern!
            }

            public void RemoveEntry(int index)
            {
                entries.RemoveAt(index); // not very stable ie if you remove 1 entry all other entries index may be off.
            }

            public override string ToString()
            {
                return string.Join(Environment.NewLine, entries);
            }

            // breaks single responsibility principle DO NOT DO
            public void Save(string filename, bool overwrite = false)
            {
                File.WriteAllText(filename, ToString());
            }

            public static Journal Load(string filename)
            {
                Journal j = new Journal();
                return j;
            }

            public void Load(Uri uri)
            {

            }
        }
        // handles the responsibility of persisting objects
        public class Persistence
        {
            public void SaveToFile(Journal journal, string filename, bool overwrite = false)
            {
                if (overwrite || !File.Exists(filename))
                    File.WriteAllText(filename, journal.ToString());
            }
        }
    }
}
