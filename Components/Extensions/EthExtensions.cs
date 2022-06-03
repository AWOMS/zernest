using System.Numerics;

namespace AWOMS.Zernest.Components.Extensions;

public static class EthExtensions
{
    // note: this does round up, e.g. 0.0159 becomes 0.016
    public static string WeiToEth(this string strWei, bool showFull = false)
    {
        long dblWei;
        if (Int64.TryParse(strWei, out dblWei))
        {
            // int64 gave better precision, but has an upper limit that could be hit eventually (> 9 something eth)
            double dblEth = dblWei * 0.000000000000000001;
            if (dblEth.ToString().Length > 10)
            {
                return showFull ? dblEth.ToString("#0.00000000") : dblEth.ToString("#0.0000");
            }
            return showFull ? dblEth.ToString("#0.00000000") : dblEth.ToString("#0.0000");
        }
        else
        {
            // this worked but had some minor rounding issues
            BigInteger bigWei;
            BigInteger.TryParse(strWei, out bigWei);

            BigInteger divider;
            BigInteger.TryParse("1000000000000000000", out divider);

            double dblEth = Math.Exp(BigInteger.Log(bigWei) - BigInteger.Log(divider));
            if (dblEth.ToString().Length > 10)
            {
                return showFull ? dblEth.ToString("#0.00000000") : dblEth.ToString("#0.0000");
            }
            return showFull ? dblEth.ToString("#0.00000000") : dblEth.ToString("#0.0000");
        }
    }
}