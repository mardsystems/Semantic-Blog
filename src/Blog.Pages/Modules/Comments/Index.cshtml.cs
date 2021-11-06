using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Modules.Comments
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IEnumerable<Comment> Comments { get; set; }

        private readonly ICommentsQuery commentsQuery;

        public IndexModel(ICommentsQuery commentsQuery)
        {
            this.commentsQuery = commentsQuery;
        }

        public void OnGet()
        {
            Comments = commentsQuery.GetComments();
        }
    }
}
