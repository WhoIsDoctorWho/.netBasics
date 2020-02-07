namespace BeerStore.Models
{
    public class DisplayParams
    {
        public string ToSearch { get; set; }
        public int Page { get; set; } = 1;
        public SortState SortOrder { get; set; } = SortState.NameAsc;
    }
}
