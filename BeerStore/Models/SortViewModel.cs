namespace BeerStore.Models
{
    public enum SortState
    {
        NameAsc,
        NameDesc,
        BeerPriceAsc,
        BeerPriceDesc,
        UserLoginAsc, 
        UserLoginDesc
    }
    public class SortViewModel
    {
        public SortState NameSort { get; private set; }
        public SortState BeerPriceSort { get; private set; }
        public SortState UserLoginSort { get; private set; }
        public SortState Current { get; private set; }
        public SortViewModel(SortState order)
        {
            NameSort = order == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            UserLoginSort = order == SortState.UserLoginAsc ? SortState.UserLoginDesc: SortState.UserLoginAsc;
            BeerPriceSort = order == SortState.BeerPriceAsc ? SortState.BeerPriceDesc : SortState.BeerPriceAsc;
            Current = order;            
        }
    }

}
