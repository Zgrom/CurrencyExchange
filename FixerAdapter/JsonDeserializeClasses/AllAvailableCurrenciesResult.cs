using System.Collections.Generic;

namespace FixerAdapter.JsonDeserializeClasses
{
    public sealed class AllAvailableCurrenciesResult
    {
        public bool success { get; set; }
        public Dictionary<string,string> symbols { get; set; }
    }
}