using AutoMapper;
using HouseholdManager.Common.Contracts;

namespace HouseholdManager.Common
{
    public class MappingService : IMapingService
    {
        public T Map<T>(object source)
        {
            return Mapper.Map<T>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map<TSource, TDestination>(source, destination);
        }
    }
}
