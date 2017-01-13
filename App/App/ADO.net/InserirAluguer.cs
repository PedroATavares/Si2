using System;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace App
{
    class InserirAluguer
    {
        private static Handler handler;
        private static int numEmp, nif, cod, duracao, idAluguer;
        private static float precoTotal;
        private static string dI, dF, morada, nome;
        private static int[] promos;

        public static void AluguerSemCliente(Handler h)
        {
            if (handler == null) handler = h;
            PrintsSemCliente();
            GetPromocoes();
            AplicarTempoExtra();
            InserirAluguerSemCliente();
            InserirPromos();
            AplicarEquipamentos();
            Console.WriteLine("Total a pagar: " + precoTotal);
            Console.WriteLine("Tempo total de Aluguer:" + duracao);
        }

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
                        SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                        id.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(id);
                        cmd.ExecuteNonQuery();
                        idAluguer = Int32.Parse(id.Value.ToString());
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

        private static void PrintsSemCliente()
        {
            Console.WriteLine("********************************************************** \n");
            Console.WriteLine("Dados do novo Cliente  -----------------");
            Console.WriteLine("\n NIF do Cliente :");
            nif = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n Nome do Cliente :");
            nome = Console.ReadLine();
            Console.WriteLine("\n Morada do Cliente :");
            morada = Console.ReadLine();
            QuestoesAluguerComCliente();
        }

        private static void InitParametrosSemCliente(SqlCommand cmd)
        {
            SqlParameter nif = new SqlParameter("@NIF", SqlDbType.Int);
            SqlParameter nome = new SqlParameter("@Nome", SqlDbType.VarChar, 50);
            SqlParameter morada = new SqlParameter("@Morada", SqlDbType.VarChar, 100);

            nif.Value = InserirAluguer.nif;
            nome.Value = InserirAluguer.nome;
            morada.Value = InserirAluguer.morada;

            cmd.Parameters.Add(nif);
            cmd.Parameters.Add(nome);
            cmd.Parameters.Add(morada);
            InitParametrosComCliente(cmd);
        }
        //------------------------ METODOS PARTILHADOS --------------------------------------------------------------

        private static void GetPromocoes()
        {
            Console.WriteLine("Aplicar Promoçao? (S/N)");
            String result = Console.ReadLine();
            promos = new int[3] {0,0,0};
            if (result.Equals("S") || result.Equals("s"))
            {
                EntitiesUtilsADO.PrintPromocoesLivres(dI,dF,handler);
                Console.WriteLine("Insira o Id de uma Promoção do tipo Desconto, caso nao queira aplicar, insira 0: ");
                promos[0] = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Insira o Id de uma Promoção do tipo Tempo Extra, caso nao queira aplicar, insira 0: ");
                promos[1] = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Insira o Id de uma Promoção do tipo Desconto e TempoExtra, caso nao queira aplicar, insira 0: ");
                promos[2] = Int32.Parse(Console.ReadLine());
            }
        }

        private static void AplicarTempoExtra()
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    int tempoExtra = 0;
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "select TempoExtra from TempoExtra where Id=" + promos[1] + " or Id=" +
                                          promos[2];
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                tempoExtra += Int32.Parse(dr["TempoExtra"].ToString());
                            }
                        }
                    }
                    duracao += tempoExtra;
                }
                catch (DbException e)
                {
                    Console.WriteLine("E R R O" + e.Message);

                }
            }
        }

        private static void InserirPromos()
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("AplicarPromo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i < promos.Length; i++)
                        {
                            if (promos[i] != 0)
                            {
                                SqlParameter aluguer = new SqlParameter("@NumAluguer", SqlDbType.Int);
                                SqlParameter promo = new SqlParameter("@CodPromo", SqlDbType.Int);

                                promo.Value = promos[i];
                                aluguer.Value = idAluguer;

                                cmd.Parameters.Add(promo);
                                cmd.Parameters.Add(aluguer);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (DbException e)
                {
                    Console.WriteLine("E R R O" + e.Message);
                }
            }
        }

        private static void AplicarEquipamentos()
        {
            int idEq;
            do
            {
                EntitiesUtilsADO.PrintEquipamentosLivres(dI, dF, handler);
                Console.WriteLine("Que equipamentos quer adicionar ao Alguer criado ? (para sair escreva -> 0)");
                idEq = Int32.Parse(Console.ReadLine());
                if (idEq == 0) break;
                EntitiesUtilsADO.PrintPrecarioParaEquipamento(dI, dF, idEq, handler);
                Console.WriteLine("Indique a duração do Preço pretendido:");
                int duracao = Int32.Parse(Console.ReadLine());
                float preco = EntitiesUtilsADO.PrintAndGetValor(dI, dF, idEq, duracao, handler);
                InserirEquipamento(preco, idEq);
            } while (idEq != 0);




        }

        private static void InserirEquipamento(float valor, int idEquip)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    float percentagem = 0;
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "select Percentagem from Descontos where Id=" + promos[0] + " or Id=" +
                                          promos[2];
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                percentagem += float.Parse(dr["Percentagem"].ToString());
                            }
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand("InserirAluguerEquipamentos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                       
                                SqlParameter preco = new SqlParameter("@Preco", SqlDbType.SmallMoney);
                                SqlParameter aluguer = new SqlParameter("@NumAluguer", SqlDbType.Int);
                                SqlParameter equipId = new SqlParameter("@CodEquip", SqlDbType.Int);

                                float precoEquip = ((100 - percentagem) / 100)*valor;

                                preco.Value = precoEquip;
                                aluguer.Value = idAluguer;
                                equipId.Value = idEquip;
                                precoTotal += precoEquip;

                                cmd.Parameters.Add(preco);
                                cmd.Parameters.Add(aluguer);
                                cmd.Parameters.Add(equipId);

                                cmd.ExecuteNonQuery();
                            
                    }
                }
                catch (DbException e)
                {
                    Console.WriteLine("E R R O" + e.Message);
                }
            }
        }


        //------------------ INSERIR ALUGUER COM CLIENTE  ----------------------------------------------------------

        public static void AluguerComCliente(Handler h)
        {
            if (handler == null) handler = h;
            QuestoesAluguerComCliente();
            GetPromocoes();
            AplicarTempoExtra();
            InserirAluguerComCliente();
            InserirPromos();
            AplicarEquipamentos();
            Console.WriteLine("Total a pagar: " + precoTotal);
            Console.WriteLine("Tempo total de Aluguer:"+duracao);
        }

        public static void InserirAluguerComCliente()
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    EntitiesUtilsADO.PrintClientes(con);
                    Console.WriteLine("\nEscolha um dos Clientes (codigo NIF):");
                    nif = Convert.ToInt32(Console.ReadLine());
                    if (nif <= 0)
                     {
                        Console.WriteLine("O NIF que colocou esta incorrecto, volte a tentar");
                        EntitiesUtilsADO.PrintClientes(con);
                     }
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "select Codigo from Cliente where Cliente.Nif="+nif;
 
                         using (SqlDataReader dr = cmd.ExecuteReader())
                         {
                             while (dr.Read())
                                cod=Int32.Parse(dr["Codigo"].ToString());
                         }
                    }
                    using (SqlCommand cmd = new SqlCommand("InserirAluguerComCliente", con))
                    {
                        cmd.CommandType= CommandType.StoredProcedure;
                        InitParametrosComCliente(cmd);
                        SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                        id.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(id);
                        cmd.ExecuteNonQuery();
                        idAluguer = Int32.Parse(id.Value.ToString());
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

        private static void QuestoesAluguerComCliente()
        {
            Console.WriteLine("Dados do novo Aluguer  -----------------");
            Console.WriteLine("\n Coloque a Data Inicial (AAAA-MM-DD HH:MM:SS):");
            dI = Console.ReadLine();
            Console.WriteLine("\n Coloque a Data Final(AAAA-MM-DD HH:MM:SS):");
            dF = Console.ReadLine();
            Console.WriteLine("\n Coloque a Duração (em minutos):");
            duracao = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n Coloque o Nº Empregado:");
            numEmp = Convert.ToInt32(Console.ReadLine());
        }

        private static void InitParametrosComCliente(SqlCommand cmd)
        {
            SqlParameter dataI = new SqlParameter("@DataI", SqlDbType.DateTime);
            SqlParameter dataF = new SqlParameter("@DataF", SqlDbType.DateTime);
            SqlParameter duracao = new SqlParameter("@Duracao", SqlDbType.Int);
            SqlParameter numEmpregado = new SqlParameter("@NumEmp", SqlDbType.Int);
            SqlParameter codigoCliente = new SqlParameter("@CodCli", SqlDbType.Int);

            dataI.Value = dI;
            dataF.Value = dF;
            duracao.Value = InserirAluguer.duracao;
            numEmpregado.Value = numEmp;
            codigoCliente.Value = cod;
            
            cmd.Parameters.Add(dataI);
            cmd.Parameters.Add(dataF);
            cmd.Parameters.Add(duracao);
            cmd.Parameters.Add(numEmpregado);
            cmd.Parameters.Add(codigoCliente);
        }
    
    }
}
