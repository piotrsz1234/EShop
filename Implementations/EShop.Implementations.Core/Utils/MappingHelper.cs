using AutoMapper;
using EShop.Core.Entities;
using EShop.Dtos.Product.Models;

namespace EShop.Implementations.Core.Utils
{
    public static class MappingHelper
    {
        private static IMapper _mapper;
        
        public static IMapper Mapper {
            get {
                if (_mapper is null) Mapper = new Mapper(GetConfiguration());

                return _mapper;
            }
            set => _mapper = value;
        }
        
        private static MapperConfiguration GetConfiguration()
        {
            return new MapperConfiguration(cfg => {
                cfg.CreateMap<AddEditProductModel, Product>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
            });
        }
    }
}