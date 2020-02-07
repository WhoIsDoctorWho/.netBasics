namespace BeerStore.Models
{
    public enum SortState
    {
        NameAsc,
        NameDesc,
        PriceAsc,
        PriceDesc,
    }
    public class SortViewModel
    {
        public SortState NameSort { get; private set; }
        public SortState PriceSort { get; private set; }
        public SortState Current { get; private set; }
        public SortViewModel(SortState order)
        {
            NameSort = order == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            PriceSort = order == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            Current = order;            
        }
    }

}
