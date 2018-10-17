using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Interface Segregation Principle: states that no client should be forced to depend on methods it does not use. 
 * ISP splits interfaces that are very large into smaller and more specific ones so that clients will only have to know about the 
 * methods that are of interest to them. Shrunken interfaces are called role interfaces. ISP is intended to keep a system decoupled 
 * and thus easier to refactor, change, and redeploy.
 */

namespace DesignPattern.Patterns.Principles
{
    class InterfaceSegregationPrinciple
    {
        public class Document
        {
        }

        public interface IMachine
        {
            void Print(Document d);
            void Fax(Document d);
            void Scan(Document d);
        }

        // ok if you need a multifunction machine
        public class MultiFunctionPrinter : IMachine
        {
            public void Print(Document d)
            {
                //
            }

            public void Fax(Document d)
            {
                //
            }

            public void Scan(Document d)
            {
                //
            }
        }

        //Problematic because OldFashionedPrinter can't support Fax/Scan but they have to be implemented.
        public class OldFashionedPrinter : IMachine
        {
            public void Print(Document d)
            {
                //
            }

            public void Fax(Document d)
            {
                throw new NotImplementedException();
            }

            public void Scan(Document d)
            {
                throw new NotImplementedException();
            }
        }

        public interface IPrinter
        {
            void Print(Document d);
        }

        public interface IScanner
        {
            void Scan(Document d);
        }

        public class Photocopier : IPrinter, IScanner
        {
            public void Print(Document d)
            {

            }
            public void Scan(Document d)
            {

            }
        }

        public interface IMultiFunctionDevice : IScanner, IPrinter //etc
        {

        }

        public class MultFunctionMachine : IMultiFunctionDevice
        {
            private IPrinter printer;
            private IScanner scanner;

            public MultFunctionMachine(IPrinter printer, IScanner scanner)
            {
                if (printer == null)
                {
                    throw new ArgumentNullException(paramName: nameof(printer));
                }
                if (scanner == null)
                {
                    throw new ArgumentNullException(paramName: nameof(scanner));
                }
                this.printer = printer;
                this.scanner = scanner;
            }
            //Decorator pattern
            public void Print(Document d)
            {
                printer.Print(d);
            }
            public void Scan(Document d)
            {
                scanner.Scan(d);
            }
        }
    }
}
