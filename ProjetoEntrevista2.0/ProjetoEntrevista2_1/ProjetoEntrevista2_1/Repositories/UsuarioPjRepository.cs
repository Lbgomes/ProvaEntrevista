using ProjetoEntrevista2_1.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEntrevista2_1.Repositories
{
    public class UsuarioPJRepository
    {
        private string StringConexao = "Data Source=LAB08DESK2301\\SQLEXPRESS; initial catalog=Projeto; user id=sa; pwd=sa@132;";

        // declaracao do metodo que preciso criar
        public List<UsuarioPJDomains> Listar()
        {

            List<UsuarioPJDomains> UsuariosPJ = new List<UsuarioPJDomains>();
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a instrução a ser executada
                // string QueryaSerExecutada = "SELECT IdFuncionario, Nome, Sobrenome FROM Funcionarios";
                string QueryaSerExecutada = "SELECT * FROM UsuarioPJ";

                //Abre o banco de dados
                con.Open();

                //Declaro um SqlDataReader para percorrer a lista
                SqlDataReader rdr;

                //Declaro um command passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(QueryaSerExecutada, con))
                {
                    //Executa a query
                    rdr = cmd.ExecuteReader();

                    //Percorre os dados 
                    while (rdr.Read())
                    {
                        UsuarioPJDomains UsuarioPJ = new UsuarioPJDomains
                        {
                            IdUsuarioPj = Convert.ToInt32(rdr["IdUsuarioPJ"]),
                            NomeUsuario = rdr["NomeUsuario"].ToString(),
                            NumeroCnpj = Convert.ToInt32(rdr["NumeroCnpj"]),
                            Telefone = Convert.ToInt32(rdr["Telefone"]),
                            IdTipo = Convert.ToInt32(rdr["IdTipo"].ToString())
                        };
                        UsuariosPJ.Add(UsuarioPJ);
                    }
                }
            }
            return UsuariosPJ;
        }
        public UsuarioPJDomains BuscarPorCnpj(int cnpj)
        {
            // string QuerySelect = "SELECT IdFuncionario, Nome, Sobrenome FROM Funcionarios WHERE IdFuncionario = @IdFuncionario";
            string QuerySelect = "SELECT * FROM UsuarioPJ WHERE NumeroCnpj = @NumeroCnpj";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(QuerySelect, con))
                {
                    cmd.Parameters.AddWithValue("@NumeroCnpj", cnpj);
                    sdr = cmd.ExecuteReader();

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            UsuarioPJDomains UsuarioPJ = new UsuarioPJDomains
                            {
                                IdUsuarioPj = Convert.ToInt32(sdr["IdUsuarioPJ"]),
                                NomeUsuario = sdr["NomeUsuario"].ToString(),
                                NumeroCnpj = Convert.ToInt32(sdr["NumeroCnpj"]),
                                Telefone = Convert.ToInt32(sdr["Telefone"]),
                                IdTipo = Convert.ToInt32(sdr["IdTipo"].ToString())
                            };
                            return UsuarioPJ;
                        }
                    }
                    return null;
                }
            }
        }

        public void Cadastrar(UsuarioPJDomains UsuarioPJDomains)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a query passando o valor como parametro
                // string QueryASerExecutada = "INSERT INTO Funcionarios (Nome, Sobrenome) VALUES (@Nome, @Sobrenome)";
                string QueryASerExecutada = "INSERT INTO UsuarioPJ (NomeUsuario, IdTipo, NumeroCnpj, Telefone)" +
                                            "VALUES(@NomeUsuario, 1, @NumeroCnpj, @Telefone)";
                //Declara o command passando a query e a conexão
                SqlCommand cmd = new SqlCommand(QueryASerExecutada, con);
                //Passa o valor do parametro
                cmd.Parameters.AddWithValue("@NomeUsuario", UsuarioPJDomains.NomeUsuario);
                cmd.Parameters.AddWithValue("@NumeroCnpj", UsuarioPJDomains.NumeroCnpj);
                cmd.Parameters.AddWithValue("@IdTipo", UsuarioPJDomains.IdTipo);
                cmd.Parameters.AddWithValue("@Telefone", UsuarioPJDomains.Telefone);
                //abre a conexão
                con.Open();
                //Executa o comando
                cmd.ExecuteNonQuery();
            }
        }

        public void Alterar(UsuarioPJDomains UsuarioPJ)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // string Query = "UPDATE Funcionarios SET Nome = @Nome, Sobrenome = @Sobrenome WHERE IdFuncionario = @IdFuncionario";
                string Query = "UPDATE UsuarioPJ SET NomeUsuario = @NomeUsuario, IdTipo = '@TipoUsuario', NumeroCnpj = @NumeroCnpj, Telefone = @Telefone where NumeroCnpj = @NumeroCnpj";

                SqlCommand cmd = new SqlCommand(Query, con);
                //Passa o valor do parametro
                cmd.Parameters.AddWithValue("@NomeUsuario", UsuarioPJ.NomeUsuario);
                cmd.Parameters.AddWithValue("@NumeroCnpj", UsuarioPJ.NumeroCnpj);
                cmd.Parameters.AddWithValue("@IdTipo", UsuarioPJ.IdTipo);
                cmd.Parameters.AddWithValue("@Telefone", UsuarioPJ.Telefone);

            }
        }

        public void Deletar(int cnpj)
        {
            string QueryDelete = "DELETE FROM UsuarioPJ WHERE NumeroCnpj = @NumeroCnpj";
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                using (SqlCommand cmd = new SqlCommand(QueryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@NumeroCnpj", cnpj);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }
        /// <summary>
        /// Buscar todos os funcionários que possuam um determinado nome
        /// </summary>
        /// <returns>Lista de Funcionários</returns>
        public List<UsuarioPJDomains> BuscarPorNome(string nomeProcurado)
        {
            List<UsuarioPJDomains> UsuariosPJ = new List<UsuarioPJDomains>();

            //Declaro a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a instrução a ser executada
                // string QueryaSerExecutada = "SELECT IdFuncionario, Nome, Sobrenome FROM Funcionarios";
                string QueryaSerExecutada = "SELECT * FROM UsuarioPJ WHERE NomeUsuario LIKE '%' + @NomeUsuario + '%'";
                //Abre o banco de dados
                con.Open();
                //Declaro um SqlDataReader para percorrer a lista
                SqlDataReader rdr;
                //Declaro um command passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(QueryaSerExecutada, con))
                {
                    cmd.Parameters.AddWithValue("@NomeProcurado", nomeProcurado);

                    //Executa a query
                    rdr = cmd.ExecuteReader();

                    //Percorre os dados 
                    while (rdr.Read())
                    {
                        UsuarioPJDomains UsuarioPJ = new UsuarioPJDomains
                        {
                            IdUsuarioPj = Convert.ToInt32(rdr["IdUsuarioPJ"]),
                            NomeUsuario = rdr["NomeUsuario"].ToString(),
                            NumeroCnpj = Convert.ToInt32(rdr["NumeroCnpj"]),
                            Telefone = Convert.ToInt32(rdr["Telefone"]),
                            IdTipo = Convert.ToInt32(rdr["IdTipo"].ToString())
                        };

                        UsuariosPJ.Add(UsuarioPJ);
                    }
                }
            }

            return UsuariosPJ;
        }
    }
}
