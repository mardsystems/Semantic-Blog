using System;
using System.Collections.Generic;

namespace Blog.Modules.Categories
{
    public class Category
    {
        public CategoryId Id { get; private set; }

        public string Description { get; private set; }

        public int TotalArticles { get; private set; }

        public Category(CategoryId id, string description)
        {
            Id = id;

            Description = description;

            TotalArticles = 0;
        }

        public void IncrementTotalArticles()
        {
            TotalArticles++;
        }
    }

    public class CategoryId : ValueObject
    {
        public string Value { get; private set; }

        public CategoryId(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }

    public interface ICategoriesRepository
    {
        void Add(Category category);
        
        Category[] GetCategoriesBy(CategoryId[] ids);
    }
}
