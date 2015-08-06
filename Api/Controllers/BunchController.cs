using System.Linq;
using System.Web.Http;
using Api.Models;

namespace Api.Controllers
{
    public class BunchController : CustomApiController
    {
        private readonly BunchListModel _bunchList = new BunchListModel
        {
            new BunchModel {Id = 1, Slug = "tomato-soup", Name = "Tomato Soup"},
            new BunchModel {Id = 2, Slug = "yo-yo", Name = "Yo-yo"},
            new BunchModel {Id = 3, Slug = "hammer", Name = "Hammer"}
        };

        public BunchListModel GetAllProducts()
        {
            return _bunchList;
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = _bunchList.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }

    public abstract class CustomApiController : ApiController
    {
        
    }
}
