using AutoMapper;
using DepositApi.BLL.DTO;
using DepositApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
