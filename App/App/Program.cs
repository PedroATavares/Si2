using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace App
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Handlers handlers = new Handlers(@"Server=localhost;Database=TestesSI2;User=jdbcuser;Password=jdbcuser;");
            String key;
            do
            {
                Console.WriteLine("1 - Inserir Promoção");
                Console.WriteLine("2 - Remover Promoção");
                Console.WriteLine("3 - Alterar Promoção existente");
                Console.WriteLine("4 - Inserir Aluguer com Cliente Novo");
                Console.WriteLine("5 - Inserir Aluguer com Cliente Existente");
                Console.WriteLine("6 - Remover Aluguer");
                Console.WriteLine("7 - Alterar Preçário");
                Console.WriteLine("8 - Listar todos os equipamentos livres, para um determinado tempo e tipo");
                Console.WriteLine("9 - Listar os equipamentos sem alugueres na última semana");
                Console.WriteLine("10 - Sair");
                Console.WriteLine("Numbero correspondente á ação?");
                Console.Write(">");
                key = Console.ReadLine();
                switch (key)
                {
                    case "1": handlers.inserirPromoção(); break;
                    case "2": handlers.removerPromoção();  break;
                    case "3": break;
                    case "4": break;
                    case "5": break;
                    case "6": break;
                    case "7": break;
                    case "8": break;
                    case "9": break;
                    default: Console.WriteLine("Por favor insira um numero valido!"); break;
                }
            } while (key != "10");
        }

        
    }
}


