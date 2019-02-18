using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockLibTestPattern
{
    public class TestBunkatuHeigou
    {
        static public void Test()
        {
            //1→2分割
            var bunkatuCorrection = StockLib.Price.MakeBunkatuHeigouCorrection(2, 1);
            //価格Dic
            var dicPrices = new Dictionary<DateTime, double?>();
            dicPrices.Add(DateTime.Parse("2019-02-08"), 2000);
            dicPrices.Add(DateTime.Parse("2019-02-09"), 1900);
            dicPrices.Add(DateTime.Parse("2019-02-12"), 1570);//1→2分割
            dicPrices.Add(DateTime.Parse("2019-02-13"), 800);
            dicPrices.Add(DateTime.Parse("2019-02-14"), null);
            dicPrices.Add(DateTime.Parse("2019-02-15"), 900);
            dicPrices.Add(DateTime.Parse("2019-02-16"), 850); //3→1併合
            dicPrices.Add(DateTime.Parse("2019-02-17"), 2700);
            dicPrices.Add(DateTime.Parse("2019-02-18"), 2600);
            dicPrices.Add(DateTime.Parse("2019-02-19"), 2400);
            //分割併合Dic
            var dicHeigouBunkatu = new Dictionary<DateTime, double>();
            dicHeigouBunkatu.Add(DateTime.Parse("2019-02-13"), StockLib.Price.MakeBunkatuHeigouCorrection(1, 2));
            dicHeigouBunkatu.Add(DateTime.Parse("2019-02-17"), StockLib.Price.MakeBunkatuHeigouCorrection(3, 1));

            var testa = StockLib.Price.BunkatuHeigou(dicPrices, dicHeigouBunkatu);
            var testb = StockLib.Price.BunkatuHeigou(dicPrices);
            foreach(var item in testa)
            {
                Console.WriteLine($"{item.Datetime} {item.Price} {item.Correction}");
            }
            Console.WriteLine("");
            foreach (var item in testb)
            {
                Console.WriteLine($"{item.Datetime} {item.Price} {item.Correction}");
            }
            Console.WriteLine("");
            foreach (var item in StockLib.Price.DoubleBunkatuHeigou(dicPrices, dicHeigouBunkatu))
            {
                Console.WriteLine($"{item}");
            }
            Console.WriteLine("");
        }
    }
}
