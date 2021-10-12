using AutoMapper;
using DepositApi.BLL.DTO;
using DepositApi.Models;

namespace DepositApi.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DepositCalculationDTO, DepositCalculationModel>();
            CreateMap<DepositCalculationModel, DepositCalculationDTO>();
            CreateMap<DepositDTO, DepositModel>();
            CreateMap<DepositModel, DepositDTO>();
        }
    }
}
