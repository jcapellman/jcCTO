using System.Collections.Generic;

using jcCTO.PCL;
using jcCTO.Tests.PCL;

namespace jcCTO.WebAPI.Controllers {
    public class UserController : BaseAPIController {
        public CTOResponseItem<List<UserListingResponseItem>> Get() {
            var tmpList = new List<UserListingResponseItem>();

            for (var x = 0; x < 100; x++) {
                tmpList.Add(new UserListingResponseItem {ID = x, Name = x.ToString()});
            }

            return Return(tmpList);
        }
    }
}