using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace singleton
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Type type = typeof(Program);
            type.GetType();

            

        }
    }

    sealed class SingleTon
    {
        private static volatile SingleTon _singleTon;
        private static readonly object _lock = new object();

        private SingleTon() { }

        public static SingleTon Instance
        {
            get
            {
                if (_singleTon == null)
                {
                    lock (_lock)
                    {
                        if (_singleTon == null)
                        {
                            _singleTon = new SingleTon();
                        }
                    }
                }

                return _singleTon;
            }
        }
    }
}
