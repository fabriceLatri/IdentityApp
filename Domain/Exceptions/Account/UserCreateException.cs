using System.Collections.Generic;
using System.Linq;

namespace Domain.Exceptions.Account
{
	public class UserCreateException : Exception
    {
        public IEnumerable<object> Errors { get; }

        public UserCreateException(IEnumerable<object> errors)
            : base($"User creation failed: {string.Join(", ", errors.Select(e => e.ToString()))}")
        {
            Errors = errors;
        }
    }
}

