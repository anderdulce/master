using static WebApplication1.Controllers.UsuarioController;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Connections;
using WebApplication1.Modelss;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace WebApplication1.Repository
{
    public class ADO_Usuario
    {
        public static List<Usuario> DevolverUsuarios()
        {
            var listaUsuarios = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "SELECT * FROM usuario";
                var reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    var usuario = new Usuario();

                    usuario.Id = Convert.ToInt32(reader2.GetValue(0));
                    usuario.Nombre = reader2.GetValue(1).ToString();
                    usuario.Apellido = reader2.GetValue(2).ToString();
                    usuario.NombreUsuario = reader2.GetValue(3).ToString();
                    usuario.Contraseña = reader2.GetValue(4).ToString();
                    usuario.Mail = reader2.GetValue(5).ToString();

                    listaUsuarios.Add(usuario);

                }
                reader2.Close();
                connection.Close();

            }
            return listaUsuarios;
        }

        public static  Usuario TraerUsuario(int id)

        {
            var usuario = new Usuario();
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from Usuario where Id = @IdUsuario";
                cmd.Parameters.Add(new SqlParameter("@IdUsuario", id));
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader.GetValue(0));
                    usuario.Nombre = reader.GetValue(1).ToString();
                    usuario.Apellido = reader.GetValue(2).ToString();
                    usuario.NombreUsuario = reader.GetValue(3).ToString();
                    usuario.Contraseña = reader.GetValue(4).ToString();
                    usuario.Mail = reader.GetValue(5).ToString();

                }
                reader.Close();
                connection.Close();

            }
            return usuario;
        }
        

      
        public static int CrearUsuario(Usuario usu)
        {
            int id;
            string connectionString = General.connectionString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Usuario(Nombre,Apellido,NombreUsuario,Contraseña,Mail) VALUES (@Nombre,@Apellido,@NombreUsuario,@Contraseña,@Mail); Select scope_identity()", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar)).Value = usu.Nombre;
                cmd.Parameters.Add(new SqlParameter("Apellido", SqlDbType.VarChar)).Value = usu.Apellido;
                cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar)).Value = usu.NombreUsuario;
                cmd.Parameters.Add(new SqlParameter("Contraseña", SqlDbType.VarChar)).Value = usu.Contraseña;
                cmd.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar)).Value = usu.Mail;
                id = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return id;
        }

        public static int  ModificarUsuario(Usuario usu)
        {
            int modificar;
            string connectionString = General.connectionString();
            using (SqlConnection conn = new SqlConnection(General.connectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Usuario SET  Nombre = @Nombre, Apellido = @Apellido, NombreUsuario = @NombreUsuario, Contraseña = @Contraseña, Mail = @Mail WHERE Id = @id", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt)).Value = usu.Id;
                cmd.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar)).Value = usu.Nombre;
                cmd.Parameters.Add(new SqlParameter("Apellido", SqlDbType.VarChar)).Value = usu.Apellido;
                cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar)).Value = usu.NombreUsuario;
                cmd.Parameters.Add(new SqlParameter("Contraseña", SqlDbType.VarChar)).Value = usu.Contraseña;
                cmd.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar)).Value = usu.Mail;
                modificar= Convert.ToInt32(cmd.ExecuteNonQuery());
                conn.Close();

            }
            return modificar;
        }
        public static void EliminarUsuario(int id)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Delete FROM usuario where Id = @idUsu";

                var param = new SqlParameter();
                param.ParameterName = "idUsu";
                param.SqlDbType = SqlDbType.BigInt;
                param.Value = id;

                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }


        public static Usuario InicioSesion(string nombreUsuario, string password)
        {
            Usuario usuario = new Usuario();
            string connectionString = General.connectionString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id ,Nombre ,Apellido ,NombreUsuario ,Contraseña ,Mail FROM Usuario WHERE NombreUsuario like @NombreUsuario and  Contraseña like @password", conn);
                adapter.SelectCommand.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar)).Value = nombreUsuario;
                adapter.SelectCommand.Parameters.Add(new SqlParameter("password", SqlDbType.VarChar)).Value = password;
                conn.Open();
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);
                if (tabla.Rows.Count > 0)
                {
                    DataRow dr = tabla.Rows[0];
                    usuario.Id = Convert.ToInt32(dr["Id"]);
                    usuario.Apellido = dr["Apellido"].ToString();
                    usuario.Nombre = dr["Nombre"].ToString();
                    usuario.NombreUsuario = dr["NombreUsuario"].ToString();
                    usuario.Contraseña = dr["Contraseña"].ToString();
                    usuario.Mail = dr["Mail"].ToString();
                }

                conn.Close();
            }
            return usuario;

        }
    }
}
