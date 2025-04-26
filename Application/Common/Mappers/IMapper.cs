using System;

namespace Application.Common.Mappers;

public interface IMapper
{
  TDestination Map<TDestination>(object source);
}
