using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;

using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace jcCTO.PCL {
    public class CTOResponseItem<T> {
        private readonly T _obj;

        public bool InError { get; set; }

        private readonly string _errorMessage;

        public CTOResponseItem(T obj, bool inError = false, string errorMessage = "") {
            InError = inError;

            _obj = obj;

            _errorMessage = errorMessage;
        }

        public T GetValue() {
            return _obj;
        }

        public string GetErrorMessage() {
            return _errorMessage;
        }

        public byte[] ToBytes(bool compress = true) {
            var size = Marshal.SizeOf(_obj);
            var arr = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(_obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);

            if (compress) {
                return compressBytes(arr);
            }

            return arr;
        }

        private byte[] compressBytes(byte[] raw) {
            using (var memory = new MemoryStream()) {
                using (var gzip = new GZipStream(memory, CompressionMode.Compress, true)) {
                    gzip.Write(raw, 0, raw.Length);
                }

                return memory.ToArray();
            }
        }

        public string ToJson() {
            return JsonConvert.SerializeObject(_obj);
        }

        public byte[] ToBSON() {
            var ms = new MemoryStream();
            var serializer = new JsonSerializer();

            var writer = new BsonWriter(ms);

            serializer.Serialize(writer, _obj);

            return ms.ToArray();
        }
    }
}