using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PromoToEvents.Logic.DataBase;
using PromoToEvents.Models;

namespace PromoToEvents.Controllers.Api
{
    public class CategoryInfoController : ApiController
    {

        private readonly CategoriaRepository _categoriaRepo = CategoriaRepository.GetInstance;
        // GET api/<controller>
        public HttpResponseMessage GetCategories()
        {
            var results = _categoriaRepo.Filter(x => x.statusCategoria).Select(y => new ApiCategoryInfo
            {
                IdCategoria = y.idCategoria,
                NombreCategoria = y.nombreCategoria
            });

            var response = Request.CreateResponse(HttpStatusCode.OK, results);
            response.Headers.Add(Constants.AccessControlAllowOrigin, "*");
            return response;
        }

        // GET api/<controller>/5
        public HttpResponseMessage GetCategory(int id)
        {
            var category = _categoriaRepo.Filter(x => x.idCategoria == id).First();

            var response = Request.CreateResponse(HttpStatusCode.OK, new ApiCategoryInfo
            {
                IdCategoria = category.idCategoria,
                NombreCategoria = category.nombreCategoria
            });

            response.Headers.Add(Constants.AccessControlAllowOrigin, "*");
            return response;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}