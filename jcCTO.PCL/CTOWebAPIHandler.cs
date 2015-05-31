using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace jcCTO.PCL {
    public class CTOWebAPIHandler {
        private readonly string _baseAddress;

        public CTOWebAPIHandler(string baseAddress) {
            _baseAddress = baseAddress;
        }

        private Uri getURL(string relativeURL) {
            return new Uri(String.Format(_baseAddress + "{0}", relativeURL));
        }

        private T convertToT<T>(byte[] data) {
            var t = (T)Activator.CreateInstance(typeof(T), null);
            
            var size = Marshal.SizeOf(t);
            var ptr = Marshal.AllocHGlobal(size);

            Marshal.Copy(data, 0, ptr, size);

            t = (T)Marshal.PtrToStructure(ptr, t.GetType());
            Marshal.FreeHGlobal(ptr);

            return t;
        }

        public async Task<T> Get<T>(string url) {
            var httpClient = new HttpClient();
            
            var data = await httpClient.GetStringAsync(getURL(url));

            var responseItem = JsonConvert.DeserializeObject<CTOResponseItem<T>>(data);
            
            return convertToT<T>(responseItem.Data);
        }
    }
}
