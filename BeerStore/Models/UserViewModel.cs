using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BeerStore.Models
{
    public class UserViewModel
    {
        public UserViewModel(DbSet<User> users, DisplayParams displayParams)
        {
            Users = users;
            Filter(displayParams.ToSearch);
            Sort(displayParams.SortOrder);
            Pagination(displayParams.Page);
        }

        public IEnumerable<User> Users { get; set; }
        public PaginationViewModel PaginationModel { get; set; }
        public FilterViewModel FilterModel { get; set; }
        public SortViewModel SortModel { get; set; }
        private void Filter(string toSearch)
        {
            if (!string.IsNullOrEmpty(toSearch))
                Users = Users.Where(user => user.Fullname.Contains(toSearch) || user.Login.Contains(toSearch));
            FilterModel = new FilterViewModel(toSearch);
        }
        private void Sort(SortState sortOrder)
        {
            Users = sortOrder switch
            {
                SortState.NameDesc => Users.OrderByDescending(user => user.Fullname),
                SortState.UserLoginAsc => Users.OrderBy(user => user.Login),
                SortState.UserLoginDesc => Users.OrderByDescending(user => user.Login),
                _ => Users.OrderBy(user => user.Fullname),
            };
            SortModel = new SortViewModel(sortOrder);
        }
        private void Pagination(int page)
        {
            const int pageSize = 8;
            int count = Users.Count();
            Users = Users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PaginationModel = new PaginationViewModel(count, page, pageSize);
        }

    }
}
