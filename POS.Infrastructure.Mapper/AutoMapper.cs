using AutoMapper;
using POS.Domain.EntityModels;
using POS.Domain.ViewModels;

namespace POS.Infrastructure.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<Department, DepartmentModel>().ReverseMap();
            CreateMap<DbSupplier, DbSupplierModel>().ReverseMap();
        }
    }
}