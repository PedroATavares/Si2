using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class Handlers
    {
        private readonly string CONNECTION_STRING;
        public Handlers (String cs) {
            CONNECTION_STRING = cs;            
        }

        public void removerPromoção()
        {
            int id;
            do
            {
                Console.Write("Id da Promoção a Remover:");
                id = Convert.ToInt32(Console.ReadLine());
            } while (id <= 0);
            removerPromoção(id);
        }

        private void removerPromoção(int id)
        {
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


                    }

                }
                catch (DbException ex)
                {
                    Console.WriteLine("E R R O : " + ex.Message);
                }
            }
        }

        public void inserirPromoção()
        {
            Console.Write("Data de Inicio (AAAA-MM-DD):");
            String dataInicio = Console.ReadLine();
            Console.Write("Data de Fim (AAAA-MM-DD):");
            String dataFim = Console.ReadLine();
            Console.Write("Descrição (max 200 caracteres):");
            String desc = Console.ReadLine();
            inserirPromoção(dataInicio, dataFim, desc);
        }

        private void inserirPromoção(String dataInicio, String dataFim, String desc)
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
                        SqlParameter descr = new SqlParameter("@Descricao", SqlDbType.VarChar, 200);
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
