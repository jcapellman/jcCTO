using System.Collections.Generic;
using jcCTO.Tests.PCL;

namespace jcCTO.WebAPI.Controllers {
    public class UserController : BaseAPIController {
        public byte[] Get(int num) {
            var tmpList = new List<UserListingResponseItem>();

            for (var x = 0; x < num; x++) {
                tmpList.Add(new UserListingResponseItem {ID = x, Name = x.ToString()});
            }

            return Return(tmpList);
        }
    }
}