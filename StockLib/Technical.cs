using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockLib
{
    public class Technical
    {
        //取引比率
        static public IEnumerable<double?> TorihikiHiritu(IEnumerable<long> volumes, IEnumerable<long> outstandingStock)
        {
            if (volumes.Count() != outstandingStock.Count())
            {
                yield return null;
            }
            else
            {
                var _volumes = volumes.ToList();
                var _outstandingStock = outstandingStock.ToList();
                for (int i = 0; i < _volumes.Count(); i++)
                {
                    yield return _volumes[i] / _outstandingStock[i];
                }
            }            
        }

        //TRの計算(日付を全部合わせて入れる)
        static public IEnumerable<double?> TR(IEnumerable<double?> highPrices, IEnumerable<double?> lowPrices, IEnumerable<double?> lateClosePrices)
        {
            var highs = highPrices.ToList();
            var lows = lowPrices.ToList();
            var lateCloses = lateClosePrices.ToList();

            for (int i = 0; i < highs.Count(); i++)
            {
                if (highs[i] == null || lows[i] == null || lateCloses[i] == null)
                {
                    yield return null;
                    continue;
                }

                var TRList = new List<double?>
                {
                    highs[i] - lows[i],
                    highs[i] - lateCloses[i],
                    lateCloses[i] - lows[i]
                };
                yield return TRList.Max();
            }
        }

        //移動平均の作成
        static public IEnumerable<double?> MovingAverage(IEnumerable<double?> prices, int span)
        {
            //MAの数が足りない分はnullを返す
            for(int i = 0; i < span-1; i++)
            {
                yield return null;
            }
            //直近値に置き換えれるnullは全部修正
            var _toLatestPrices = NullToLatestPrice(prices);
            foreach(var spanLatestPrices in EachCons(_toLatestPrices, span))
            {
                double? total = 0;
                foreach(var price in spanLatestPrices)
                {
                    total += price;
                }
                yield return total / span;
            }
        }
        //テクニカル作成用のspan分IEnumerable作成
        static public IEnumerable<IEnumerable<double?>> EachCons(IEnumerable<double?> prices, int span)
        {
            for (int i = 0; i <= prices.Count() - span; i++)
            {
                yield return prices.Skip(i).Take(span);
            }
        }

        //移動平均用の価格IEnumerableの作成
        //価格が無い日(null)は直近の値を返す
        static public IEnumerable<double?> NullToLatestPrice(IEnumerable<double?> prices)
        {
            var latestPrice = prices.FirstOrDefault();
            foreach (var price in prices)
            {
                if(price == null)
                {
                    //直近値を返す
                    yield return latestPrice;
                }
                else
                {
                    //価格を返す
                    latestPrice = price;
                    yield return price;
                }
            }
        }
    }
}
