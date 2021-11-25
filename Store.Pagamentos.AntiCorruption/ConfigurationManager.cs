using System;
using System.Linq;

namespace Store.Pagamentos.AntiCorruption
{
    public class ConfigurationManager : IConfigurationManager
    {
        public string GetValue(string node)
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVXZWY0123456789", 10).Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}
