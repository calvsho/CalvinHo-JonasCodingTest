﻿using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMapper();
        }

        private void CreateMapper()
        {
            CreateMap<DataEntity, BaseInfo>();
            CreateMap<Company, CompanyInfo>();
            CreateMap<Employee, EmployeeInfo>();
            CreateMap<ArSubledger, ArSubledgerInfo>();
        }
    }

}