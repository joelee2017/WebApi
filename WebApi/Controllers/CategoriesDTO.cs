using WebApi.Models;

namespace WebApi.Controllers
{
    public class CategoriesDTO 
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}