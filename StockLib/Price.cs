using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockLib
{
    public class Price
    {
        //分割・併合Dicを作成
        static public double MakeBunkatuHeigouCorrection(double mae, double ato)
        {
            return mae / ato;
        }
        //分割併合適応(補正値は分割前)
        static public IEnumerable<BunkatuHeigouPrice> BunkatuHeigou(Dictionary<DateTime, double?> dicPrices , Dictionary<DateTime, double> dicBunkatuHeigouCorrections = null)
        {
            //null又はデータが空の場合、補正値1.0で返す
            if(dicBunkatuHeigouCorrections == null || dicBunkatuHeigouCorrections.Count == 0)
            {
                foreach(var price in dicPrices)
                {
                    yield return new BunkatuHeigouPrice
                    {
                        Datetime = price.Key,
                        Price = price.Value,
                        Correction = 1.0
                    };
                }
            }
            //分割・併合補正処理
            else
            {
                foreach(var price in dicPrices)
                {
                    var correct = 1.0;
                    foreach(var correction in dicBunkatuHeigouCorrections)
                    {
                        //分割併合の適用日以前なら補正値を掛ける
                        if(price.Key < correction.Key)
                        {
                            correct = correct * correction.Value;
                        }
                    }
                    yield return new BunkatuHeigouPrice
                    {
                        Datetime = price.Key,
                        Price = price.Value,
                        Correction = correct
                    };
                }
            }            
        }

    }
    //分割クラス
    public class BunkatuHeigouPrice
    {
        public DateTime Datetime { get; set; }
        public double? Price { get; set; }
        public double Correction { get; set; }
    }
}
