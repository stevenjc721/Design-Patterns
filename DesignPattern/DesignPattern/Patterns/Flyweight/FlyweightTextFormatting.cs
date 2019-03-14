﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Patterns.Flyweight
{
    class FlyweightTextFormatting
    {
        public class FormattedText
        {
            private readonly string plainText;
            private bool[] capitalize;

            public FormattedText(string plainText)
            {
                this.plainText = plainText;
                capitalize = new bool[plainText.Length];
            }

            public void Capitalize(int start, int end)
            {
                for (int i = start; i <= end; i++)
                    capitalize[i] = true;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                for (var i = 0; i < plainText.Length; i++)
                {
                    var c = plainText[i];
                    sb.Append(capitalize[i] ? char.ToUpper(c) : c);
                }
                return sb.ToString();
            }
        }

        public class BetterFormattedText
        {
            private string plainText;
            private List<TextRange> formatting = new List<TextRange>();

            public BetterFormattedText(string plainText)
            {
                this.plainText = plainText;
            }
            public TextRange GetRange(int start, int end)
            {
                var range = new TextRange { Start = start, End = end };
                formatting.Add(range);
                return range;
            }

            public class TextRange
            {
                public int Start, End;
                public bool Capitalize, Bold, Italic;

                public bool Covers(int position)
                {
                    return position >= Start && position <= End;
                }
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                for(var index = 0; index < plainText.Length; index++)
                {
                    var c = plainText[index];
                    foreach (var range in formatting)
                        if (range.Covers(index) && range.Capitalize)
                            c = char.ToUpper(c);
                    sb.Append(c);
                }
                return sb.ToString();
            }
        }
    }
}
