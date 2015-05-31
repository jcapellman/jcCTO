using System.IO;
using System.Runtime.InteropServices;

using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace jcCTO.PCL {
    public static class Extensions {
        public static byte[] ToBytes<T>(T obj) {
            var size = Marshal.SizeOf(obj);
            var arr = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }

        public static string ToJson<T>(T obj) {
            return JsonConvert.SerializeObject(obj);
        }

        public static byte[] ToBSON<T>(T obj) {
            var ms = new MemoryStream();
            var serializer = new JsonSerializer();

            var writer = new BsonWriter(ms);

            serializer.Serialize(writer, obj);

            return ms.ToArray();
        }
    }
}