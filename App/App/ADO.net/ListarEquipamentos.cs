using System;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace App
{
    class ListarEquipamentos
    {
        static Handler handler;
        public static void ExecProcedure(string dataI, string dataF, string type)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("listarEquipamentos",con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter validadeI = new SqlParameter("@ValidadeI", SqlDbType.Date);
                        validadeI.Value = dataI;
                        cmd.Parameters.Add(validadeI);
                        SqlParameter validadeF = new SqlParameter("@ValidadeF", SqlDbType.Date);
                        validadeF.Value = dataF;
                        cmd.Parameters.Add(validadeF);
                        SqlParameter tipo = new SqlParameter("@tipo", SqlDbType.VarChar, 50);
                        tipo.Value = type;
                        cmd.Parameters.Add(tipo);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            Console.WriteLine(type + " livres entre " + dataI + " e " + dataF + ":");
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

        public static void GetParamsFromConsole(Handler h)
        {
            if (handler == null) handler = h;
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