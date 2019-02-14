using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockLib
{
    public class Technical
    {
        //移動平均の作成
        public IEnumerable<double?> MovingAverage(IEnumerable<double?> prices, int span)
        {
            return null;
        }
        //テクニカル作成用のspan分IEnumerable作成
        public static IEnumerable<IEnumerable<double?>> EachCons<T>(IEnumerable<double?> prices, int span)
        {
            for (int i = 0; i <= prices.Count() - span; i++)
            {
                yield return prices.Skip(i).Take(span);
            }
        }

        //移動平均用の価格IEnumerableの作成
        //価格が無い日(null)は直近の値を返す
        public IEnumerable<double?> NullToLatestPrice(IEnumerable<double?> prices)
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
