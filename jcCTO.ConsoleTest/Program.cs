using System;
using System.Collections.Generic;

using jcCTO.PCL;
using jcCTO.Tests.PCL;

namespace jcCTO.ConsoleTest {
    class Program {
        public class Testing {
            public async void Run(int num) {
                var httpClient = new CTOWebAPIHandler("http://192.168.1.212/api/");

                var data = await httpClient.Get<List<UserListingResponseItem>>("User");

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