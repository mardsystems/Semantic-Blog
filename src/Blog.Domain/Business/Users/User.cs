using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Users
{
    public class User
    {
        public UserId Id { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }
        
        public string ImageUrl { get; private set; }

        public User(
            UserId id,
            string name,
            string email,
            string imageUrl)
        {
            Id = id;

            Name = name;

            Email = email;

            ImageUrl = imageUrl;
        }
    }

    public class UserId : ValueObject
    {
        public string Value { get; private set; }

        public UserId(string value)
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

    public interface IUsersRepository
    {
        void Add(User user);
    }
}
