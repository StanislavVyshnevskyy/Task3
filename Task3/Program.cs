using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Task3
{
    class Program
    {
        private static string[] _a;
        private static Byte[] _key = new Byte[128]; 

        static void Main(string[] args)
        {
            if (ArgsEvaluator(args)) {

                GenerateKey();
                var _myMove = DoMove(_a.Length);
                var _HMAC = GenerateHMAC(_a[_myMove], _key);
                Console.WriteLine($"HMAC: {_HMAC}");
                PrintMoves(_a);
                var _input = Console.ReadLine();
                if (MoveEvaluator(_input, out int _n)) {
                    Console.WriteLine($"Your move: {_a[_n]}");
                    Console.WriteLine($"Computer move: {_a[_myMove]}");
                    Console.WriteLine(MovesComparator(_n, _myMove));
                    Console.WriteLine($"HMAC key: {BitConverter.ToString(_key).Replace("-", "").ToLower()}");
                    Console.ReadLine();
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
                    PrintWarningMsg();
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


        private static string MovesComparator(int _userMove, int _myMove) {
            string _result;
            if (_userMove == _myMove)
            {
                _result = "draw game :)";
            }
            else if (_userMove > _myMove)
            {
                _result = _userMove - _myMove <= _a.Length / 2 ? "You win!" : "You lose :(";
            }
            else {
                _result = _myMove - _userMove > _a.Length / 2 ? "You win!" : "You lose :(";
            }
            return _result;
        }
        private static void PrintMoves(string[] _a) {
            Console.WriteLine("Available moves:");
            for (int i = 0; i < _a.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {_a[i]}");
            }
            Console.WriteLine("0 - Exit");
            Console.WriteLine("Enter your move: ");
        }

        private static void PrintWarningMsg() {
            string _msg = "Number of parameters should be odd and not less than 3.\n" +
                          "Parameters should be unique and separeted by space.\n" +
                          "Please provide valid number of parameters for starting the game.\n" +
                          "Or type '0' for exit";
            Console.WriteLine(_msg);
        }

        private static void GenerateKey() {
            using (var _csp = new RNGCryptoServiceProvider())
            {
                _csp.GetNonZeroBytes(_key);
            }
        }

        private static int DoMove(int _maxValue) {
            var _move = RandomNumberGenerator.GetInt32(0, _maxValue);
            return _move;
        } 

        private static bool MoveEvaluator(string _move, out int _n) {
            var _result = false;
            _n = -1;
            while (!_result)
            {
                if (_move != "0")
                {
                    _result = Int32.TryParse(_move, out _n) && _n > 0 && _n <= _a.Length;
                    if (!_result)
                    {
                        Console.WriteLine($"Your move isn't valid.\nPlease, type number from 1 to {_a.Length}");
                        _move = Console.ReadLine();
                    }
                }
                // player typed "0" and wants to exit game
                else {
                    return false;
                }
            }
            _n = _n - 1;
            return _result;
        }

        private static string GenerateHMAC(string _str, Byte[] _k) {
            using (var _sha = new HMACSHA256(_k)) {
                var _hmac = _sha.ComputeHash(Encoding.ASCII.GetBytes(_str));
                return BitConverter.ToString(_hmac).Replace("-", "").ToLower();
            }
        }
    }
}
