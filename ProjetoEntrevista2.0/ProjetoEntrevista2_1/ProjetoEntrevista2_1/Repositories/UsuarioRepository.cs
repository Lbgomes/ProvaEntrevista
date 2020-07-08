using ProjetoEntrevista2_1.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEntrevista2_1.Repositories
{
    public class UsuarioRepository
    {
        private string StringConexao = "Data Source=DESKTOP-CQTIV89\\SQLEXPRESS; initial catalog=Entrevista; user id=sa; pwd=sa@132;";


        public List<UsuarioDomain>Listar()
        {
            List<UsuarioDomain> Usuarios = new List<UsuarioDomain>();
            using (SqlConnection con = new SqlConnection(StringConexao))
            {


                //Declara a instrução a ser executada
                // string QueryaSerExecutada = "SELECT IdFuncionario, Nome, Sobrenome FROM Funcionarios";
                string QueryaSerExecutada = "SELECT * FROM Usuario";

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
                        UsuarioDomain Usuario = new UsuarioDomain
                        {
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"]),
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"].ToString()),
                            NomeUsuario = rdr["NomeUsuario"].ToString(),
                            NumeroDocumento = rdr["NumeroDocumento"].ToString(),
                            Telefone = Convert.ToInt32(rdr["Telefone"])
                        };
                        Usuarios.Add(Usuario);
                    }
                }
            }
            return Usuarios;
        }
        public UsuarioDomain BuscarPorDocumento(string documento)
        {
            // string QuerySelect = "SELECT IdFuncionario, Nome, Sobrenome FROM Funcionarios WHERE IdFuncionario = @IdFuncionario";
            string QuerySelect = "SELECT * FROM Usuario WHERE NumeroDocumento = @NumeroDocumento";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(QuerySelect, con))
                {
                    cmd.Parameters.AddWithValue("@NumeroDocumento", documento);
                    sdr = cmd.ExecuteReader();

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            UsuarioDomain Usuario = new UsuarioDomain
                            {
                                IdUsuario = Convert.ToInt32(sdr["IdUsuario"]),
                                IdTipoUsuario = Convert.ToInt32(sdr["IdTipoUsuario"].ToString()),
                                NomeUsuario = sdr["NomeUsuario"].ToString(),
                                NumeroDocumento = sdr["NumeroDocumento"].ToString(),
                                Telefone = Convert.ToInt32(sdr["Telefone"])
                            };
                            return Usuario;
                        }
                    }
                    return null;
                }
            }
        }

        public void Cadastrar(UsuarioDomain UsuarioDomain)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a query passando o valor como parametro
                // string QueryASerExecutada = "INSERT INTO Funcionarios (Nome, Sobrenome) VALUES (@Nome, @Sobrenome)";
                string QueryASerExecutada = "INSERT INTO Usuario (NomeUsuario, IdTipoUsuario, NumeroDocumento, Telefone)" +
                                            "VALUES(@NomeUsuario, @IdTipoUsuario, @NumeroDocumento, @Telefone)";
                //Declara o command passando a query e a conexão
                SqlCommand cmd = new SqlCommand(QueryASerExecutada, con);
                //Passa o valor do parametro
                cmd.Parameters.AddWithValue("@NomeUsuario", UsuarioDomain.NomeUsuario);
                cmd.Parameters.AddWithValue("@IdTipoUsuario", UsuarioDomain.IdTipoUsuario);
                cmd.Parameters.AddWithValue("@NumeroDocumento", UsuarioDomain.NumeroDocumento);
                cmd.Parameters.AddWithValue("@Telefone", UsuarioDomain.Telefone);
                //abre a conexão
                con.Open();
                //Executa o comando
                cmd.ExecuteNonQuery();
            }
        }

        public void Alterar(UsuarioDomain Usuario)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // string Query = "UPDATE Funcionarios SET Nome = @Nome, Sobrenome = @Sobrenome WHERE IdFuncionario = @IdFuncionario";
                string Query = "UPDATE Usuario SET NomeUsuario = @NomeUsuario, IdTipoUsuario = @IdTipoUsuario, NumeroDocumento = @NumeroDocumento, Telefone = @Telefone" +
                                "where NumeroDocumento = @NumeroDocumento";

                SqlCommand cmd = new SqlCommand(Query, con);
                //Passa o valor do parametro
                cmd.Parameters.AddWithValue("@NomeUsuario", Usuario.NomeUsuario);
                cmd.Parameters.AddWithValue("@NumeroDocumento", Usuario.NumeroDocumento);
                cmd.Parameters.AddWithValue("@IdTipoUsuario", Usuario.IdTipoUsuario);
                cmd.Parameters.AddWithValue("@Telefone", Usuario.Telefone);

            }
        }

        public void Deletar(string documento)
        {
            string QueryDelete = "DELETE FROM Usuario WHERE NumeroDocumento = @NumeroDocumento";
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                using (SqlCommand cmd = new SqlCommand(QueryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@NumeroDocumento", documento);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }
        /// <summary>
        /// Buscar todos os funcionários que possuam um determinado nome
        /// </summary>
        /// <returns>Lista de Funcionários</returns>
        public List<UsuarioDomain> BuscarPorNome(string nomeProcurado)
        {
            List<UsuarioDomain> Usuarios = new List<UsuarioDomain>();

            //Declaro a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a instrução a ser executada
                // string QueryaSerExecutada = "SELECT IdFuncionario, Nome, Sobrenome FROM Funcionarios";
                string QueryaSerExecutada = "SELECT * FROM Usuario WHERE NomeUsuario LIKE '%' + '@NomeUsuario' + '%'";
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
                        UsuarioDomain Usuario = new UsuarioDomain
                        {
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"]),
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"].ToString()),
                            NomeUsuario = rdr["NomeUsuario"].ToString(),
                            NumeroDocumento = rdr["NumeroDocumento"].ToString(),
                            Telefone = Convert.ToInt32(rdr["Telefone"])
                        };

                        Usuarios.Add(Usuario);
                    }
                }
            }

            return Usuarios;
        }
    }
}
