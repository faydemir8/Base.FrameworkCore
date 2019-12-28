using System;
using AutoMapper;

namespace Base.Framework.Core.Mapper
{
    public class MappingConfig : MapperConfiguration
    {
        public MappingConfig(Action<IMapperConfigurationExpression> configure) : base(configure) { }
    }
}
