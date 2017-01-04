using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace App
{
    class Program
    {
        private static readonly string CONNECTION_STRING = @"Server=localhost;Database=TestesSI2;User=jdbcuser;Password=jdbcuser;";

        static void Main(string[] args)
        {
            /*
                using (SqlConnection con = new SqlConnection())
                {
                    try
                    {
                        con.ConnectionString = @"Server=localhost;Database=TestesSI2;User=jdbcuser;Password=jdbcuser;";
                        using (SqlCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandText = "declare @id int; exec InserirAluguerComCliente '2016-11-20','2017-11-21',40,1,1,@id output; select @id;";
                            con.Open();

                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                    Console.Write(dr[0] + "\n");
                            }
                        }

                    }
                    catch (DbException ex)
                    {
                        Console.WriteLine("E R R O : " + ex.Message);
                    }
                    Console.ReadKey();*/

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
                    case "1": inserirPromoção(); break;
                    case "2": removerPromoção();  break;
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

        private static void removerPromoção()
        {
            int id;
            do
            {
                Console.Write("Id da Promoção a Remover:");
                id = Convert.ToInt32(Console.ReadLine());
            } while (id <= 0);
            removerPromoção(id);
        }

        private static void removerPromoção(int id) {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = CONNECTION_STRING;
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        SqlParameter ident = new SqlParameter("@id", SqlDbType.Int);
                        ident.Value = id;

                        cmd.Parameters.Add(ident);
                        
                        cmd.CommandText = "exec DeletePromocoes @id;";
                        con.Open();

                        int tuples = cmd.ExecuteNonQuery();
                        Console.WriteLine(tuples + " tuple(s) afetado(s)");
                        
                    }

                }
                catch (DbException ex)
                {
                    Console.WriteLine("E R R O : " + ex.Message);
                }
            }
        }

        private static void inserirPromoção()
        {
            Console.Write("Data de Inicio (AAAA-MM-DD):");
            String dataInicio = Console.ReadLine();
            Console.Write("Data de Fim (AAAA-MM-DD):");
            String dataFim = Console.ReadLine();
            Console.Write("Descrição (max 200 caracteres):");
            String desc = Console.ReadLine();
            inserirPromoção(dataInicio, dataFim, desc);
        }

        private static void inserirPromoção(String dataInicio, String dataFim, String desc)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = CONNECTION_STRING;
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        SqlParameter dataI = new SqlParameter("@DataInicio", SqlDbType.Date);
                        SqlParameter dataF = new SqlParameter("@DataFim", SqlDbType.Date);
                        SqlParameter descr = new SqlParameter("@Descricao", SqlDbType.VarChar,200);
                        dataI.Value = dataInicio;
                        dataF.Value = dataFim;
                        descr.Value = desc;

                        cmd.Parameters.Add(dataI);
                        cmd.Parameters.Add(dataF);
                        cmd.Parameters.Add(descr);
                        cmd.CommandText = "declare @id int; exec InsertPromocoes @DataInicio,@DataFim,@Descricao,@id output; select @id;";
                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                                Console.Write(dr[0] + "\n");
                        }
                    }

                }
                catch (DbException ex)
                {
                    Console.WriteLine("E R R O : " + ex.Message);
                }
            }
        }
    }
}


