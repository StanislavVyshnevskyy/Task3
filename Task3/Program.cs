using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Task3
{
    class Program
    {
        private static string[] _a;

        static void Main(string[] args)
        {
            if (ArgsEvaluator(args)) {

                foreach (var a in _a)
                {
                    Console.WriteLine(a);
                }


            }

        }


        //evaluates input parameters for the game
        //returns false if user wants to exit game
        //returns true if parameters are valid
        private static bool ArgsEvaluator(string[] _args) {
            var result = false;
            while (!result)
            {
                if (_args.Length < 3 || _args.Length % 2 == 0 || _args.Distinct().Count() != _args.Length)
                {
                    Console.WriteLine("Number of parameters should be odd and not less than 3.");
                    Console.WriteLine("Parameters should be unique and separeted by space.");
                    Console.WriteLine("Please provide valid number of parameters for starting the game.");
                    Console.WriteLine("Or type '0' for exit");

                    var _input = Console.ReadLine();
                    if (_input == "0") { return false; }

                    _args = _input.Split(' ');
                }
                else {
                    _a = new string[_args.Length];
                    Array.Copy(_args, _a, _a.Length);
                    result = true;
                }
            }
            return result;
        }

        

        private static string GenerateHMAC(string _) {


            return "";
        }
    }
}
