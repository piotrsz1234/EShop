using EShop.Core.Infrastructure.Repositories;
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
    }
}