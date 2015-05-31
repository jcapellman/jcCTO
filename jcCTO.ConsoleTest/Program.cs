using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using jcCTO.PCL;
using jcCTO.Tests.PCL;

namespace jcCTO.ConsoleTest {
    class Program {
        public class Testing
        {
            public async void Run(int num)
            {
                var httpClient = new CTOWebAPIHandler();

                var data = await httpClient.Get<List<UserListingResponseItem>>($"http://localhost:13833/api/User?num={num}");

                foreach (var item in data) {
                    Console.WriteLine($"ID:{item.ID}|Name:{item.Name}");
                }
            }
        }
        
        static void Main(string[] args) {
            var test = new Testing();

            test.Run(100);

            Console.ReadKey();
        }
    }
}