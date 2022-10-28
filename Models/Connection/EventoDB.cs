using Microsoft.Data.SqlClient;
using ProyectoModeradores.Controllers;
using System.Data;

namespace ProyectoModeradores.Models.Connection
{
    public class EventoDB
    {
        public static DataTable ViewEvents()
        {

            try
            {

                Connections con = new Connections();

                string sql = "EXEC dbo.spEventoSelectAll";

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

        public static bool SaveEvent(ProyectoModeradores.Models.Evento e)
        {

            try
            {

                Connections con = new Connections();

                string sql = "EXEC	dbo.spEventoInsert " + "@Name='" + e.Name.ToString() + "',"
                    + "@Code='" + e.Code.ToString() + "',"
                    + "@Description='" + e.Description.ToString() + "',"
                    + "@StatusId= 1,"
                    + "@ModeradorId='" + e.ModeradorId.ToString() + "',"
                    + "@SalaId='" + e.SalaId.ToString() + "',"
                    + "@AreaId='" + e.AreaId.ToString() + "'";



                SqlCommand command = new SqlCommand(sql, con.conectar());
                int cantidad = command.ExecuteNonQuery();
                if (cantidad == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                con.desconectar();

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}