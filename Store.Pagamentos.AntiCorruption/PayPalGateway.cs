using System;
using System.Linq;

namespace Store.Pagamentos.AntiCorruption
{
    public class PayPalGateway : IPayPalGateway
    {
        public bool CommitTransaction(string cardHashKey, string orderID, decimal amount)
        {
            return new Random().Next(maxValue: 2) == 0;
        }

        public string GetCardHashKey(string serviceKey, string cartaoCredito)
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVXZWY0123456789", 10).Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public string GetPayPalServiceKey(string apiKey, string encriptionKey)
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVXZWY0123456789", 10).Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}
