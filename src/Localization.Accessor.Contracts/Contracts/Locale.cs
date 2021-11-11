namespace Localization.Accessor.Contracts.Contracts
{
    public class Locale
    {
        public string Identifier { get; set; }
        
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Identifier ?? "Null identifier" } - {Name ?? "Null name"}";
        }
    }
}