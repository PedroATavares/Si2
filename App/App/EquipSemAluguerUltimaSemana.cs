using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace App
{
    class EquipSemAluguerUltimaSemana
    {
        public static void ExecProcedure()
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = @"Server=CAROLINA;Database=Si2;User=ls;Password=ls;";
                    con.Open();
                    //aqui em vez de con.createcommand, inicias o objecto explicitamente com o nome do procedimento e com a con
                    using (SqlCommand cmd = new SqlCommand("EquipamentosSemAluguerUltimaSemana", con))
                    {
                        //depois defines o tipo do comando como Stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            Console.WriteLine("Equipamentos sem alugueres na ultima semana:\n");
                            while (dr.Read())
                            {
                                Console.Write("Código:" + dr["Codigo"] + "\t");
                                Console.Write("Descriçao:" + dr["Descricao"] + "\t");
                                Console.Write("Tipo:" + dr["Tipo"] + "\n");
                            }
                            Console.WriteLine("***********************************************************************");
                        }
                    }

                }
                catch (DbException ex)
                {
                    Console.WriteLine("E R R O : " + ex.Message);
                    Console.WriteLine("***********************************************************************");
                }

            }
        }

        public static void GetParamsFromConsole()
        {
            Console.WriteLine("***********************************************************************");
            ExecProcedure();

        }
    }
}
