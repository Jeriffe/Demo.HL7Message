using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.EnterpriseLibrarySamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var result2 = SampleDal.CheckReturnValueByReturnCaluse();


            var result1 = SampleDal.CheckSingleValueByADONET();

            var result = SampleDal.CheckReturnValueBySelectCaluse();


        }
    }
}
