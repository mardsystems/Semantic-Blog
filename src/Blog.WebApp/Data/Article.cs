using System.Collections.Generic;

namespace Blog.Data
{
    public class Article
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }

    }
}
