using System;
using ZipStock.Server;

namespace ZipStock.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter email: ");
            Console.WriteLine();
            string email = Console.ReadLine();
            Verification verification = new Verification();
            var response = verification.VerificateEmail(email);
            Console.Write("Enter message code: ");
            string code = Console.ReadLine();
            var response1 = verification.VerificateCode(code, email);
            if(response1.StatusCode == System.Net.HttpStatusCode.OK && response1.Server == 200)
            {
                Console.WriteLine(response1.Oauth);
            }
            else
            {
                Console.WriteLine(response1.ErrorMessage);
            }
            Console.ReadLine();
        }
    }
}

