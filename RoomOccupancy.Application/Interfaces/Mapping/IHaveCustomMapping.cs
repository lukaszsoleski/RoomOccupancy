using AutoMapper;

namespace RoomOccupancy.Application.Interfaces
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}
