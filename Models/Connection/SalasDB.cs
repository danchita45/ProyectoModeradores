using Microsoft.Data.SqlClient;
using ProyectoModeradores.Controllers;
using System.Data;

namespace ProyectoModeradores.Models.Connection
{
    public class SalasDB 
    {

        public static bool DeleteSala(int id)
        {
            try
            {

                Connections con = new Connections();

                string sql = "EXEC	dbo.SalaDelete " + "@SalaId=" + id.ToString();



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
        public static bool UpdateSala(ProyectoModeradores.Models.Sala e)
        {
            try
            {

                Connections con = new Connections();

                string sql = "EXEC	dbo.SalaUpdate " + "@Code='" + e.Code.ToString() + "',"
                    + "@Description='" + e.Description.ToString() + "',"
                    + "@SalaId='" + e.SalaId.ToString() + "',"
                    + "@AreaId='" + e.AreaId.ToString() + "',"
                    + "@StatusId= 1";



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
        public static bool SaveSala(ProyectoModeradores.Models.Sala e)
        {

            try
            {

                Connections con = new Connections();

                string sql = "EXEC	dbo.SalaInsert " + "@Code='" + e.Code.ToString() + "',"
                    + "@Description='" + e.Description.ToString() + "',"
                    + "@AreaId='" + e.AreaId.ToString() + "',"
                    + "@StatusId= 1";



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

        public static DataTable ViewSala()
        {

            try
            {

                Connections con = new Connections();

                string sql = "EXEC dbo.SalaSelectAll ";



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
