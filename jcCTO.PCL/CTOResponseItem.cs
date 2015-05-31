using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace jcCTO.PCL {
    public struct CTOResponseItem<T> {
        public byte[] Data { get; set; }
        
        public bool InError { get; set; }

        private readonly string _errorMessage;

        public CTOResponseItem(T obj, bool inError = false, string errorMessage = "", bool compress = true) : this() {
            InError = inError;

            Data = getBytes<T>(obj, compress);

            _errorMessage = errorMessage;
        }
        
        public string GetErrorMessage() {
            return _errorMessage;
        }

        private byte[] getBytes<T>(T obj, bool compress = true) {
            var size = Marshal.SizeOf(obj);
            var arr = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(obj, ptr, true);
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
    }
}