using System.Data.SqlClient;

namespace WebApplication1.Repository
{
    public class General
    {
        public static string connectionString()
        {
            SqlConnectionStringBuilder conecctionbuilder = new SqlConnectionStringBuilder();
            conecctionbuilder.DataSource = "DESKTOP-CD3K2IK\\JHOSMAN";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;
            var cs = conecctionbuilder.ConnectionString;
            return (cs);
        }
    }
}
