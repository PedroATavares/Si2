using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace App
{
    class InserirAluguer
    {
        private static Handler handler;
        private static int numEmp, niff, duracaoo, idAluguerr;
        private static string dI, dF, moradaa, nomee;
        private static SqlParameter dataI, dataF, duracao, numEmpregado, codigoCliente, nif, idAluguer, morada, nome;

        public static void InserirAluguerSemCliente(Handler h)
        {
            if (handler == null) handler = h;
            using (SqlConnection con = new SqlConnection())
            {
                
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("InserirAluguerSemCliente",con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        printQuestoesAluguerSemCliente();
                        initParametrosAluguerSemCliente(cmd);     

                        //cmd.CommandText = "declare @idCliente int; declare @idAluguer int; exec InserirAluguerSemCliente @NIF, @Nome, @Morada, @idCliente output, @Duracao, @NumEmpregado, @DataInicial, @DataFinal, @idAluguer output";

                        int i = cmd.ExecuteNonQuery();

                        Console.WriteLine(i + "tuplo(s) afetado(s)");

                        //Console.ReadLine();
                    }
                }
                catch (DbException ex)
                {
                    Console.WriteLine("E R R O : " + ex.Message);
                    Console.ReadLine();

                }
            }
        }

        private static void printQuestoesAluguerSemCliente()
        {
            Console.WriteLine("Dados do novo Cliente  -----------------");
            Console.WriteLine("\n NIF do Cliente :");
            niff = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n Nome do Cliente :");
            nomee = Console.ReadLine();
            Console.WriteLine("\n Morada do Cliente :");
            moradaa = Console.ReadLine();
            printQuestoesAluguerComCliente();
        }

        private static void initParametrosAluguerSemCliente(SqlCommand cmd)
        {
            nif = new SqlParameter("@NIF", SqlDbType.Int);
            nome = new SqlParameter("@Nome", SqlDbType.VarChar, 50);
            morada = new SqlParameter("@Morada", SqlDbType.VarChar, 100);
            dataI = new SqlParameter("@DataInicial", SqlDbType.Date);
            dataF = new SqlParameter("@DataFinal", SqlDbType.Date);
            duracao = new SqlParameter("@Duracao", SqlDbType.Int);
            numEmpregado = new SqlParameter("@NumEmpregado", SqlDbType.Int);

            nif.Value = niff;
            nome.Value = nomee;
            morada.Value = moradaa;
            dataI.Value = dI;
            dataF.Value = dF;
            duracao.Value = duracaoo;
            numEmpregado.Value = numEmp;

            cmd.Parameters.Add(nif);
            cmd.Parameters.Add(nome);
            cmd.Parameters.Add(morada);
            cmd.Parameters.Add(dataI);
            cmd.Parameters.Add(dataF);
            cmd.Parameters.Add(duracao);
            cmd.Parameters.Add(numEmpregado);

        }

        //------------------ INSERIR ALUGUER COM CLIENTE  ----------------------------------------------------------


        public static void InserirAluguerComCliente(Handler h)
        {
            if (handler == null) handler = h;
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        printClientes(cmd);

                        Console.WriteLine("\nEscolha um dos Clientes (codigo NIF):");
                        niff = Convert.ToInt32(Console.ReadLine());

                        if (niff <= 0)
                        {
                            Console.WriteLine("O NIF que colocou esta incorrecto, volte a tentar");
                            printClientes(cmd);
                        }

                        printQuestoesAluguerComCliente();
                        initParametrosAluguerComCliente();

                        cmd.Parameters.Add(nif);
                        cmd.CommandText = "select Codigo from Cliente where Cliente.Nif = @NIF ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                                codigoCliente.Value = dr["Codigo"];
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand("InserirAluguerComCliente", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(dataI);
                        cmd.Parameters.Add(dataF);
                        cmd.Parameters.Add(duracao);
                        cmd.Parameters.Add(numEmpregado);
                        cmd.Parameters.Add(codigoCliente);

                        //cmd.CommandText = "declare @id int; exec InserirAluguerComCliente @DataInicial, @DataFinal, @Duracao, @NumEmpregado, @CodigoCliente,@id output; select @id";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                                Console.Write("Id do novo Aluguer: " + dr[0] + "\n");
                        }
                        Console.ReadLine();
                    }
                }

                catch (DbException ex)
                {
                    Console.WriteLine("E R R O : " + ex.Message);
                    Console.ReadLine();

                }
            }
        }

        private static void printClientes(SqlCommand cmd)
        {
            cmd.CommandText = "select * from Cliente";
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                Console.WriteLine("Estes sao os Clientes existentes -------------------\nCODIGO|  NIF   |     NOME   |      MORADA");
                while (dr.Read())
                    if (!dr["nif"].Equals(0))
                        Console.Write(dr["codigo"] + " | " + dr["nif"] + " | " + dr["nome"] + " |  " + dr["morada"] + "\n");
            }
        }



        private static void printQuestoesAluguerComCliente()
        {
            Console.WriteLine("Dados do novo Aluguer  -----------------");
            Console.WriteLine("\n Coloque a Data Inicial");
            dI = Console.ReadLine();
            Console.WriteLine("\n Coloque a Data Final");
            dF = Console.ReadLine();
            Console.WriteLine("\n Coloque a Duracao");
            duracaoo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n Coloque o Nº Empregado");
            numEmp = Convert.ToInt32(Console.ReadLine());
        }

        private static void initParametrosAluguerComCliente()
        {
            
            dataI = new SqlParameter("@DataInicial", SqlDbType.Date);
            dataF = new SqlParameter("@DataFinal", SqlDbType.Date);
            duracao = new SqlParameter("@Duracao", SqlDbType.Int);
            numEmpregado = new SqlParameter("@NumEmpregado", SqlDbType.Int);
            codigoCliente = new SqlParameter("@CodigoCliente", SqlDbType.Int);
            nif = new SqlParameter("@NIF", SqlDbType.Int);

            dataI.Value = dI;
            dataF.Value = dF;
            duracao.Value = duracaoo;
            numEmpregado.Value = numEmp;
            nif.Value = niff;
        }
    
    }
}
