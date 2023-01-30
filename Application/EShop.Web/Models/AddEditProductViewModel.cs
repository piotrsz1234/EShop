using System.ComponentModel.DataAnnotations;

namespace EShop.Web.Models
{
    public class AddEditProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public string[] FileNames { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public string Price { get; set; }
    }
}