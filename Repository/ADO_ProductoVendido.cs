
using System;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Modelss;


namespace WebApplication1.Repository
{
    public class ADO_ProductoVendido
    {
        public static List<ProductoVendido> DevolverProductosVendidos()
        {
            var listaProductosVendido = new List<ProductoVendido>();
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "SELECT Id,Stock,IdProducto,IdVenta FROM dbo.ProductoVendido";
                var reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    var productoven = new ProductoVendido();
                    productoven.Id = Convert.ToInt32(reader2.GetValue(0));
                    productoven.Stock = Convert.ToInt32(reader2.GetValue(1).ToString());
                    productoven.IdProducto = Convert.ToInt32(reader2.GetValue(2));
                    productoven.IdVenta = Convert.ToInt32(reader2.GetValue(3));

                    listaProductosVendido.Add(productoven);

                }
                reader2.Close();
                connection.Close();

            }
            return listaProductosVendido;
        }


        public static List<ProductoVendido> DevolverProductoVendidoPorUsuario(int idUsuario)
        {
            var listaProductosVendidoIdUsuario = new List<ProductoVendido>();
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                using (SqlCommand comando = new SqlCommand(" select * from ProductoVendido pv inner join Producto p on pv.IdProducto = p.Id where p.IdUsuario = @idusuario;", connection))
                {
                    var parametro = new SqlParameter();
                    parametro.ParameterName = "idusuario";
                    parametro.SqlDbType = SqlDbType.BigInt;
                    parametro.Value = idUsuario;

                    comando.Parameters.Add(parametro);

                    connection.Open();
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var productovendido = new ProductoVendido();
                                productovendido.Id = Convert.ToInt32(dr["Id"]);
                                productovendido.Stock = Convert.ToInt32(dr["Stock"]);
                                productovendido.IdProducto = Convert.ToInt32(dr["IdProducto"]);
                                productovendido.IdVenta = Convert.ToInt32(dr["IdVenta"]);

                                listaProductosVendidoIdUsuario.Add(productovendido);
                            }
                        }
                        connection.Close();
                    }

                }
            }
            return listaProductosVendidoIdUsuario;
        }   
        
    }
}
