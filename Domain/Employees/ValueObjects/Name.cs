

using Domain.Base;
using System.Collections.Generic;

namespace Domain.Employees.ValueObjects
{
    public sealed class Name : ValueObject
    {
        public string FirstName;
        public string SecondName;
        private Name(string firstName, string secondName)
        {
            FirstName = firstName;
            SecondName = secondName;
        }
        public static Result<Name> Create(string firstName, string secondName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return Result.Failure<Name>("FirstName should not be empty");
            }

            if (string.IsNullOrWhiteSpace(secondName))
            {
                return Result.Failure<Name>("SecondName should not be empty");
            }



            return Result.Success(new Name(firstName, secondName));
        }
    

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return SecondName;
        }
    }
}
