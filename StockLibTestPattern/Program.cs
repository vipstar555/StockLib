using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockLibTestPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //テクニカルテスト
            TestTechnical();
            Console.Read();
        }

        static private void TestTechnical()
        {
            List<double?> testList = new List<double?>()
            {
                null,
                10.0,
                15.0,
                20.0,
                30.0,
                null,
                45.0,
                30.0,
                null,
                null,
                45.0,
                43.0,
            };
            //MA計算の確認
            var a = StockLib.Technical.MovingAverage(testList, 5);
            foreach(var aa in a)
            {
                if(aa == null)
                {
                    Console.WriteLine("null");
                }
                else
                {
                    Console.WriteLine(aa);
                }                
            }
        }
    }
}
