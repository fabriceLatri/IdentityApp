using System;
namespace Domain.Models.Mapper
{
	public interface IMapper
	{
		public U MapTo<T, U>(T from);

		public T MapFrom<U, T>(U to);
	}
}

