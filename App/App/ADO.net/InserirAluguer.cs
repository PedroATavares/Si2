using System;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace App
{
    class InserirAluguer
    {
        private static Handler handler;
        private static int numEmp, niff, cod, duracaoo;
        private static string dI, dF, moradaa, nomee;

        private static void InserirAluguerSemCliente()
        {
            using (SqlConnection con = new SqlConnection())
            {
                
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("InserirAluguerSemCliente",con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        InitParametrosSemCliente(cmd);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (DbException ex)
                {
                    Console.WriteLine("E R R O : " + ex.Message);
                    Console.ReadLine();

                }
            }
        }

        public static void PrintsSemCliente(Handler h)
        {
            if (handler == null) handler = h;
            Console.WriteLine("********************************************************** \n");
            Console.WriteLine("Dados do novo Cliente  -----------------");
            Console.WriteLine("\n NIF do Cliente :");
            niff = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n Nome do Cliente :");
            nomee = Console.ReadLine();
            Console.WriteLine("\n Morada do Cliente :");
            moradaa = Console.ReadLine();
            PrintsComCliente();
            InserirAluguerSemCliente();
        }

        private static void InitParametrosSemCliente(SqlCommand cmd)
        {
            SqlParameter nif = new SqlParameter("@NIF", SqlDbType.Int);
            SqlParameter nome = new SqlParameter("@Nome", SqlDbType.VarChar, 50);
            SqlParameter morada = new SqlParameter("@Morada", SqlDbType.VarChar, 100);

            nif.Value = niff;
            nome.Value = nomee;
            morada.Value = moradaa;

            cmd.Parameters.Add(nif);
            cmd.Parameters.Add(nome);
            cmd.Parameters.Add(morada);
            InitParametrosComCliente(cmd);

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
                    PrintsComCliente();
                    PrintEntities.PrintClientes(con);
                    using (SqlCommand cmd = new SqlCommand("InserirAluguerComCliente", con))
                    {
                        cmd.CommandType= CommandType.StoredProcedure;
                        InitParametrosComCliente(cmd);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Inserido com sucesso");
                        Console.WriteLine("********************************************************************\t");
                    }
                }

                catch (DbException ex)
                {
                    Console.WriteLine("E R R O : " + ex.Message);
                    Console.ReadLine();

                }
            }
        }

        public static void PrintsComCliente()
        {
            Console.WriteLine("Dados do novo Aluguer  -----------------");
            Console.WriteLine("\n Coloque a Data Inicial (AAAA-MM-DD HH:MM:SS):\n>");
            dI = Console.ReadLine();
            Console.WriteLine("\n Coloque a Data Final(AAAA-MM-DD HH:MM:SS):\n>");
            dF = Console.ReadLine();
            Console.WriteLine("\n Coloque a Duração (em minutos): \n>");
            duracaoo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n Coloque o Nº Empregado: \n>");
            numEmp = Convert.ToInt32(Console.ReadLine());
        }

        private static void InitParametrosComCliente(SqlCommand cmd)
        {
            SqlParameter dataI = new SqlParameter("@DataI", SqlDbType.DateTime);
            SqlParameter dataF = new SqlParameter("@DataF", SqlDbType.DateTime);
            SqlParameter duracao = new SqlParameter("@Duracao", SqlDbType.Int);
            SqlParameter numEmpregado = new SqlParameter("@NumEmp", SqlDbType.Int);
            SqlParameter codigoCliente = new SqlParameter("@CodCli", SqlDbType.Int);
            SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
            id.Direction = ParameterDirection.Output;

            dataI.Value = dI;
            dataF.Value = dF;
            duracao.Value = duracaoo;
            numEmpregado.Value = numEmp;
            codigoCliente.Value = cod;
            
            cmd.Parameters.Add(dataI);
            cmd.Parameters.Add(dataF);
            cmd.Parameters.Add(duracao);
            cmd.Parameters.Add(numEmpregado);
            cmd.Parameters.Add(codigoCliente);
            cmd.Parameters.Add(id);

        }
    
    }
}
