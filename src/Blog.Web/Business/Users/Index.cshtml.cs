using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Business.Users
{
    public class IndexModel : PageModel
    {
        public IEnumerable<User> Users { get; set; }

        private readonly IUsersQuery usersQuery;

        public IndexModel(IUsersQuery usersQuery)
        {
            this.usersQuery = usersQuery;
        }

        public void OnGet()
        {
            Users = usersQuery.GetUsers();
        }
    }
}
