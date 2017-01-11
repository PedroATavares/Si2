using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class PrintEntities
    {

        public static void PrintClientes(SqlConnection con)
        {
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "select * from Cliente";
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    Console.WriteLine(
                        "Estes sao os Clientes existentes -------------------\nCODIGO|  NIF   |     NOME   |      MORADA");
                    while (dr.Read())
                        if (!dr["nif"].Equals(0))
                            Console.Write(dr["codigo"] + " | " + dr["nif"] + " | " + dr["nome"] + " |  " + dr["morada"] +
                                          "\n");
                }
                Console.WriteLine("Insira o código de Cliente pretendido:");
                int cod = Int32.Parse(Console.ReadLine());
            }
        }
    }
}
