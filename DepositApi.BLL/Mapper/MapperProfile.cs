using AutoMapper;
using DepositApi.BLL.DTO;
using DepositApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DepositApi.BLL.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<DepositDTO, Deposit>();
            CreateMap<Deposit, DepositDTO>();
            CreateMap<DepositCalculationDTO, DepositCalculation>();
            CreateMap<DepositCalculation, DepositCalculationDTO>();
        }
    }
}
