using Microsoft.Data.SqlClient;
using ProyectoModeradores.Controllers;

namespace ProyectoModeradores.Models.Connection
{
    public class ModeradorDB
    {

        public static bool SaveMod(ProyectoModeradores.Models.Moderador e)
        {
           
            try
            {

                Connections con = new Connections();
                string sql = "EXEC	[dbo].[ModeradoresInsert]" + "@Name='" + e.Name.ToString() + "',"
                    + "@ApellidoP='" + e.ApellidoP.ToString() + "',"
                    + "@ApellidoM='" + e.ApellidoM.ToString() + "',"
                    + "@StatusId= 1,"
                    + "@AreaId1='" + e.Area1.ToString() + "',"
                    + "@AreaId2='" + e.Area2.ToString() + "',"
                    + "@InstitucionId='" + e.InstitucionId.ToString() + "',";

                SqlCommand command = new SqlCommand(sql,con.conectar());
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
