using DotnetCoreFirst.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreFirst.Services
{
    public class Greeter : IGreeter
    {
        public string Greet()
        {
            return "Hello world!";
        }
    }
}
