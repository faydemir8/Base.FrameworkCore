using System.Linq;
using AutoMapper;

namespace Base.Framework.Core.Mapper
{
    public static class Mapper
    {
        private static IMapper _instance;
        public static void Init(IMapper mapper) => _instance = mapper;

        public static TDestinationType Map<TDestinationType>(this object source) => _instance.Map<TDestinationType>(source);
        public static TDestinationType Map<TDestinationType>(this object source, TDestinationType destination) => _instance.Map(source, destination);
        public static IQueryable<TDestinationType> ProjectTo<TDestinationType>(this IQueryable source) => _instance.ProjectTo<TDestinationType>(source);
    }
}
