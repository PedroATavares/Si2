using System;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace App
{
    class Program
    {
        private static int  numEmp, niff, duracaoo, idAluguerr;
        private static string dI, dF, moradaa, nomee;
        private static SqlParameter dataI, dataF, duracao, numEmpregado, codigoCliente, nif, idAluguer, morada, nome;

        static void Main(string[] args)
        {
            //procInserirAluguerComCliente();
            procInserirAluguerSemCliente();
            //procRemoverAluger();
        }

        //------------------ INSERIR ALUGUER SEM CLIENTE  ----------------------------------------------------------

        private static void procInserirAluguerSemCliente()
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = @"Server=localhost;Database=SI2.SInverno;User=sa;Password=plastico.2";
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        con.Open();

                        Console.WriteLine("Dados do novo Cliente  -----------------");
                        printQuestoesAluguerSemCliente();
                        Console.WriteLine("Dados do novo Aluguer  -----------------");
                        printQuestoesAluguerComCliente();

                        initParametrosAluguerSemCliente(cmd);     

                        cmd.CommandText = "declare @idCliente int; declare @idAluguer int; exec InserirAluguerSemCliente @NIF, @Nome, @Morada, @idCliente output, @Duracao, @NumEmpregado, @DataInicial, @DataFinal, @idAluguer output";

                        using (SqlDataReader dr = cmd.ExecuteReader()) ;

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

        private static void printQuestoesAluguerSemCliente()
        {
            Console.WriteLine("\n NIF do Cliente :");
            niff = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n Nome do Cliente :");
            nomee = Console.ReadLine();
            Console.WriteLine("\n Morada do Cliente :");
            moradaa = Console.ReadLine();
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

        //------------------ REMOVE ALUGUER ----------------------------------------------------------

        private static void procRemoverAluger()
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = @"Server=localhost;Database=SI2.SInverno;User=sa;Password=plastico.2";
                    using (SqlCommand cmd = con.CreateCommand()){

                        con.Open();
                        printAluguer(cmd);

                        Console.WriteLine("Escolha o Aluguer (id) que deseja eliminar : ");
                        idAluguerr = Convert.ToInt32(Console.ReadLine());

                        idAluguer = new SqlParameter("@idAluguer", SqlDbType.Int);
                        idAluguer.Value = idAluguerr;
                        cmd.Parameters.Add(idAluguer);

                        cmd.CommandText = " exec RemoverAluger @idAluguer";

                        using (SqlDataReader dr = cmd.ExecuteReader()) ;
                        //printAluguer(cmd);

                        Console.ReadLine();
                    };
                }
                catch (DbException ex)
                {
                    Console.WriteLine("E R R O : " + ex.Message);
                    Console.ReadLine();

                }
            }
        }

        private static void printAluguer(SqlCommand cmd)
        {
            cmd.CommandText = "select * from Aluguer";
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                Console.WriteLine("Estes sao os Alugueres existentes -------------------\nNum | DataInicio  | DataFim   |  Duracao | Nº Empregado | Codigo Cliente ");
                while (dr.Read())
                        Console.Write(dr["Num"] + " | " + dr["DataInicio"] + " | " + dr["DataFim"] + " |  " + dr["Duracao"] + " |  " + dr["NumEmp"] + " |  " + dr["CodCli"] + "\n");
            }
        }


        //------------------ INSERIR ALUGUER COM CLIENTE  ----------------------------------------------------------

        private static void procInserirAluguerComCliente()
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = @"Server=localhost;Database=SI2.SInverno;User=sa;Password=plastico.2";
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        con.Open();
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

                        cmd.Parameters.Add(dataI);
                        cmd.Parameters.Add(dataF);
                        cmd.Parameters.Add(duracao);
                        cmd.Parameters.Add(numEmpregado);
                        cmd.Parameters.Add(codigoCliente);

                        cmd.CommandText = "declare @id int; exec InserirAluguerComCliente @DataInicial, @DataFinal, @Duracao, @NumEmpregado, @CodigoCliente,@id output; select @id";

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

        private static void printQuestoesAluguerComCliente()
        {
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

        

    }
}
