using System;
using System.Runtime.Serialization;
using jcCTO.PCL;

namespace jcCTO.ConsoleTest {
    class Program {
        [DataContract]
        public struct Users {
            [DataMember]
            public string Username { get; set; }
           
            [DataMember]
            public int ID { get; set; }
        }

        static void Main(string[] args) {
            var test = new Users {
                ID = 1,
                Username = "Testing"
            };
            
            var response = new CTOResponseItem<Users>(test);

            Console.WriteLine($"Binary: {response.ToBytes(false).Length}");
            Console.WriteLine($"Binary Gzip: {response.ToBytes().Length}");
            Console.WriteLine($"Bson: {response.ToBSON().Length}");
            Console.WriteLine($"Json: {response.ToJson().Length * sizeof(Char)}");
            
            Console.ReadKey();
        }
    }
}