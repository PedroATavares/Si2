using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace App
{
    class ListarEquipamentos
    {
        public static void ExecProcedure(string dataI, string dataF, string type)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = @"Server=CAROLINA;Database=Si2;User=ls;Password=ls;";

                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        SqlParameter validadeI = new SqlParameter("@ValidadeI", SqlDbType.Date);
                        validadeI.Value = dataI;
                        cmd.Parameters.Add(validadeI);
                        SqlParameter validadeF = new SqlParameter("@ValidadeF", SqlDbType.Date);
                        validadeF.Value = dataF;
                        cmd.Parameters.Add(validadeF);
                        SqlParameter tipo = new SqlParameter("@tipo", SqlDbType.VarChar, 50);
                        tipo.Value = type;
                        cmd.Parameters.Add(tipo);

                        cmd.CommandText = "exec listarEquipamentos @ValidadeI,@ValidadeF,@tipo";
                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            Console.WriteLine(type + " livres entre " + dataI + " e " + dataF + ":\n");
                            while (dr.Read())
                            {
                                Console.Write("Código:" + dr["Codigo"] + "\t");
                                Console.Write("Descriçao:" + dr["Descricao"] + "\t");
                                Console.Write("Tipo:" + dr["Tipo"] + "\n");
                            }
                            Console.WriteLine("***********************************************************************"); ;

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
            Console.WriteLine("Insira a Data Inicial");
            string dataI = Console.ReadLine();
            Console.WriteLine("Insira a Data Final");
            string dataF = Console.ReadLine();
            Console.WriteLine("Insira o Tipo do Equipamento");
            string tipo = Console.ReadLine();
            ExecProcedure(dataI, dataF, tipo);
        }

    }
}