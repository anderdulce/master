using System;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;
using WebApplication1.Modelss;
using static WebApplication1.Controllers.ProductoController;

namespace WebApplication1.Repository
{
    public class ADO_Producto
    {
        public static List<Producto> DevolverProductos()
        {
            var listaProductos = new List<Producto>();

            
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "SELECT * FROM producto";
                var reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    var producto = new Producto();

                    producto.Id = Convert.ToInt32(reader2.GetValue(0));
                    producto.Descripciones = reader2.GetValue(1).ToString();
                    producto.Costo = Convert.ToInt32(reader2.GetValue(2));
                    producto.PrecioVenta = Convert.ToInt32(reader2.GetValue(3));
                    producto.Stock = Convert.ToInt32(reader2.GetValue(4));
                    producto.IdUsuario = Convert.ToInt32(reader2.GetValue(5));

                    listaProductos.Add(producto);

                }
                reader2.Close();
                connection.Close();

            }
            return listaProductos;
        }

        public static Producto TraerProducto(int id)

        {
            Producto producto = new Producto();
            string connectionString = General.connectionString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand("select * from Producto where Id= @idProducto;", conn))
                {
                    comando.Parameters.Add(new SqlParameter("idProducto", id));

                    conn.Open();
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {

                                producto.Id = Convert.ToInt32(dr["Id"]);
                                producto.Descripciones = dr["Descripciones"].ToString();
                                producto.Costo = Convert.ToInt64(dr["Costo"]);
                                producto.PrecioVenta = Convert.ToInt64(dr["PrecioVenta"]);
                                producto.Stock = Convert.ToInt32(dr["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);

                            }

                        }
                    }
                    return producto;
                }
            }
        }

        public static List<Producto>TraerProductoIdUsuario(int idUsuario)

        {
            var productousuario = new List<Producto>();
            string connectionString = General.connectionString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand("select * from Producto where IdUsuario= @usuario;", conn))
                {
                    comando.Parameters.Add(new SqlParameter("usuario", idUsuario));

                    conn.Open();
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var Productoidusuario = new Producto();

                                Productoidusuario.Id = Convert.ToInt32(dr["Id"]);
                                Productoidusuario.Descripciones = dr["Descripciones"].ToString();
                                Productoidusuario.Costo = Convert.ToInt64(dr["Costo"]);
                                Productoidusuario.PrecioVenta = Convert.ToInt64(dr["PrecioVenta"]);
                                Productoidusuario.Stock = Convert.ToInt32(dr["Stock"]);
                                Productoidusuario.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);

                                productousuario.Add(Productoidusuario);

                            }

                        }
                    }
                    return productousuario;
                }
            }
        }


        public static int CrearProducto(Producto prod)
        {
            int id;
            string connectionString = General.connectionString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Producto (Descripciones,Costo,PrecioVenta,Stock,IdUsuario) VALUES (@Descripciones,@Costo,@PrecioVenta,@Stock,@IdUsuario); Select scope_identity()", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("Descripciones", SqlDbType.VarChar)).Value = prod.Descripciones;
                cmd.Parameters.Add(new SqlParameter("Costo", SqlDbType.Float)).Value = prod.Costo;
                cmd.Parameters.Add(new SqlParameter("PrecioVenta", SqlDbType.Float)).Value = prod.PrecioVenta;
                cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int)).Value = prod.Stock;
                cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.Int)).Value = prod.IdUsuario;
                id = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return id;

        }

        public static int ModificarProducto(Producto prod)
        {
            int modificado;
            string connectionString = General.connectionString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Producto  SET Descripciones = @Descripciones ,Costo = @Costo ,PrecioVenta = @PrecioVenta, Stock = @Stock, IdUsuario = @idUsuario WHERE Id = @id ", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt)).Value = prod.Id;
                cmd.Parameters.Add(new SqlParameter("Descripciones", SqlDbType.VarChar)).Value = prod.Descripciones;
                cmd.Parameters.Add(new SqlParameter("Costo", SqlDbType.Float)).Value = prod.Costo;
                cmd.Parameters.Add(new SqlParameter("PrecioVenta", SqlDbType.Float)).Value = prod.PrecioVenta;
                cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int)).Value = prod.Stock;
                cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt)).Value = prod.IdUsuario;
                modificado = Convert.ToInt32(cmd.ExecuteNonQuery());
                conn.Close();
            }
            return modificado;
        }

        public static void EliminarProducto(int idProducto)

        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Delete FROM Producto where Id = @idProducto";

                var param = new SqlParameter();
                param.ParameterName = "idProducto";
                param.SqlDbType = SqlDbType.BigInt;
                param.Value = idProducto;

                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        
        }
    } 
}
