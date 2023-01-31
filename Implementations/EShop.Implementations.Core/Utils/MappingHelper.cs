using AutoMapper;
using EShop.Core.Common.Enums;
using EShop.Core.Entities;
using EShop.Dtos.Product.Dtos;
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
                    .ForMember(dest => dest.VatValue, opt => opt.MapFrom(src => src.VatValue))
                    .ForMember(dest => dest.Id, opt => opt.Ignore());

                cfg.CreateMap<Product, ProductDto>()
                    .ForMember(dest => dest.BigImageId,
                        opt => opt.MapFrom(
                            src => src.ProductFiles.Any(x => x.File.Type == FileType.MainImage)
                                ? (long?)src.ProductFiles.First(x => x.File.Type == FileType.MainImage).FileId
                                : null))
                    .ForMember(dest => dest.SmallImageId,
                        opt => opt.MapFrom(
                            src => src.ProductFiles.Any(x => x.File.Type == FileType.SmallImage)
                                ? (long?)src.ProductFiles.First(x => x.File.Type == FileType.SmallImage).FileId
                                : null));
            });
        }
    }
}