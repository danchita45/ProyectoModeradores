using Microsoft.Data.SqlClient;
using ProyectoModeradores.Controllers;
using System.Data;


namespace ProyectoModeradores.Models.Connection
{
    public class StatusDB
    {
        public static System.Data.DataTable ViewStatus()
        {

            try
            {

                Connections con = new Connections();

                string sql = "EXEC dbo.StatusSelectAll ";



                SqlCommand command = new SqlCommand(sql, con.conectar());
                SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);

                con.desconectar();
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
