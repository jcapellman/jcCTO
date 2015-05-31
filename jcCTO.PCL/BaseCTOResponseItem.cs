using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace jcCTO.PCL {
    public struct BaseCTOResponseItem {
        public bool InError { get; set; }

        private readonly string _errorMessage;
        
        public string GetErrorMessage() {
            return _errorMessage;
        }
        
        public byte[] ToBytes() {
            var size = Marshal.SizeOf(this);
            var arr = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(this, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }

        public string ToJson() {
            return JsonConvert.SerializeObject(this);
        }

        public byte[] ToBSON() {
            var ms = new MemoryStream();
            var serializer = new JsonSerializer();

            var writer = new BsonWriter(ms);

            serializer.Serialize(writer, this);

            return ms.ToArray();
        }
    }
}
