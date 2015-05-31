using System.Web.Http;

using jcCTO.PCL;

namespace jcCTO.WebAPI.Controllers {
    public class BaseAPIController : ApiController {
        public CTOResponseItem<T> Return<T>(T obj) {
            return new CTOResponseItem<T>(obj, compress:false);
        }
    }
}