using System;
using Application.Common.Mappers;
using AutoMapper;

namespace Presentation.Common.Mappers;

public class Mapper(AutoMapper.IMapper mapper) : Application.Common.Mappers.IMapper
{
  private readonly AutoMapper.IMapper _mapper = mapper;
  
  public TDestination Map<TDestination>(object source)
  {
    return _mapper.Map<TDestination>(source);
  }
}
