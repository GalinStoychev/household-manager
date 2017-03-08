using AutoMapper;

namespace HouseholdManager.Common.Contracts
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IConfigurationProvider configuration);
    }
}
