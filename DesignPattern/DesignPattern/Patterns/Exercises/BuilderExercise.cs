using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Patterns.Exercises
{
    class BuilderExercise
    {
        public class CodeElement
        {
            public string FieldName, FieldType;
            public List<CodeElement> Elements = new List<CodeElement>();
            private const int indentSize = 2;

            public CodeElement()
            {

            }

            public CodeElement(string fieldName, string fieldType) {
                FieldName = fieldName ?? throw new ArgumentNullException(paramName: nameof(fieldName));
                FieldType = fieldType ?? throw new ArgumentNullException(paramName: nameof(fieldType));
            }

            private string ToStringImpl(int indent)
            {
                var sb = new StringBuilder();
                var i = new string(' ', indentSize * indent);
                if (string.IsNullOrEmpty(FieldType) && !string.IsNullOrEmpty(FieldName)) {
                    sb.AppendLine($"{i}public class {FieldName}");
                    sb.AppendLine($"{i}" + "{");

                    foreach (var e in Elements)
                    {
                        sb.Append(e.ToStringImpl(indent + 1));
                    }

                    sb.AppendLine($"{i}" + "}");
                }

                if (!string.IsNullOrEmpty(FieldType) && !string.IsNullOrEmpty(FieldName))
                {
                    sb.AppendLine($"{i} public {FieldType} {FieldName};");
                }
                
                return sb.ToString();
            }

            public override string ToString()
            {
                return ToStringImpl(0);
            }
        }

        public class CodeBuilder {

            private readonly string rootName;
            CodeElement root = new CodeElement();

            public CodeBuilder(string rootName)
            {
                this.rootName = rootName;
                root.FieldName = rootName;
            }
            public CodeBuilder AddField(string fieldName, string fieldType)
            {
                var e = new CodeElement(fieldName, fieldType);
                root.Elements.Add(e);
                return this;
            }

            public override string ToString()
            {
                return root.ToString();
            }
        }
    }
}
