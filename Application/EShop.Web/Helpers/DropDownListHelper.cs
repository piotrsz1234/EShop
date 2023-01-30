using EShop.Core.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EShop.Web.Helpers
{
    public static class DropDownListHelper
    {
        public static IEnumerable<SelectListItem> GetCategories(long? originalCategory)
        {
            var repository = AutofacConfig.Resolve<ICategoryRepository>();

            var items = repository.GetAllNonDepended(originalCategory);

            var result = items.Select(x => new SelectListItem() {
                Value = x.Id.ToString(),
                Text = x.Name.ToString()
            }).ToList();

            result.Insert(0, new SelectListItem() {
                Text = "None",
                Selected = true,
            });
            
            return result;
        }

        public static IEnumerable<SelectListItem> GetAddresses(long userId)
        {
            var repository = AutofacConfig.Resolve<IAddressRepository>();
            var data= repository.GetAll(x => x.UserId == userId && x.IsDeleted == false);

            return data.Select(x => new SelectListItem() {
                Value = x.Id.ToString(),
                Text = x.Address1
            });
        }
        
        public static IEnumerable<SelectListItem> GetShippingMethods()
        {
            var repository = AutofacConfig.Resolve<IShippingMethodRepository>();
            var data= repository.GetAll(x => x.IsDeleted == false);

            return data.Select(x => new SelectListItem() {
                Value = x.Id.ToString(),
                Text = x.Name
            });
        }
    }
}