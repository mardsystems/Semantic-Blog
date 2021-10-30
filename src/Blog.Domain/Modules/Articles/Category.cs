using Blog.Modules.Categories;

namespace Blog.Modules.Articles
{
    public class Category
    {
        public CategoryId Id { get; private set; }

        public string Description { get; private set; }

        public Category(CategoryId id, string description)
        {
            Id = id;

            Description = description;
        }

        public static explicit operator Category(Categories.Category category)
        {
            return new Category(
                id: category.Id,
                description: category.Description);
        }
    }
}
