using Microsoft.Data.SqlClient;
using ProyectoModeradores.Controllers;
using System.Data;


namespace ProyectoModeradores.Models.Connection
{
    public class AreaDB
    {
        public static System.Data.DataTable ViewArea()
        {

            try
            {
                Connections con = new Connections();
                string sql = "EXEC dbo.AreaSelectAll ";
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
        public static bool ImportArea(DataTable datos)
        {
            bool ret = true;
            Connections con = new Connections();

            using (SqlBulkCopy s = new SqlBulkCopy(con.conectar()))
            {
                s.ColumnMappings.Add("Codigo", "Code");
                s.ColumnMappings.Add("Description", "Description");

                s.DestinationTableName = "dbo.Area";
                try
                {
                    s.WriteToServer(datos);
                }
                catch (Exception ex)
                {
                    string st = ex.Message;
                    ret = false;
                }
            }
            return ret;
        }
    }
}
