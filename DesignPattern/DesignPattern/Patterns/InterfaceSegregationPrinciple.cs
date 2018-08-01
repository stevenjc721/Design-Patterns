using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Interface Segregation Principle
 * your Interface should be segregated so that you don't have to Implement methods you don't need.
 */
namespace DesignPattern.Patterns
{
    public class Document
    {

    }
    //Ok for multiFunctionPrinter but not for older devices
    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);

    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }

        public void Fax(Document d)
        {
            //
        }
    }
    /*
     * OldFashionPrinter can't scan or fax but you have to implement because all call are in the same Interface.
     */
    public class OldFashionPrinter : IMachine
    {
        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }

        public void Fax(Document d)
        {
            throw new NotImplementedException();
        }
    }
    /*
     * One solution is to have multiple interface and inherit from them all
     */
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
            //
        }
        public void Scan(Document d)
        {
            //
        }
    }

    /*
     * A Second Solution is to have a "Master" interface that inherits from other interfaces
     */
    public interface IMultiFunctionDevice : IScanner, IPrinter //....
    {
    }

    public class MultifunctionMachineDevice : IMultiFunctionDevice
    {
        /*
         * 1 Option of Implementation
         
        public void Print(Document d)
        {
            //
        }
        public void Scan(Document d)
        {
            //
        }
        */

        /*
         * Option 2 Delegating the calls Decorator Pattern
         */
        private IPrinter printer;
        private IScanner scanner;

        public MultifunctionMachineDevice(IPrinter printer, IScanner scanner)
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
        public void Print(Document d)
        {
            printer.Print(d);
        }// decorator
        public void Scan(Document d)
        {
            scanner.Scan(d);
        }// decorator
    }
    class InterfaceSegregationPrinciple
    {
    }
}
