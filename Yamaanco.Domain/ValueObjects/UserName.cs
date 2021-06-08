using System.Collections.Generic;
using Yamaanco.Domain.Common;

namespace Yamaanco.Domain.ValueObjects
{
    public class UserName : ValueObject
    {
        private string FirstName { get; set; }
        private string LastName { get; set; }

        public UserName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return $"{FirstName}.{LastName}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}