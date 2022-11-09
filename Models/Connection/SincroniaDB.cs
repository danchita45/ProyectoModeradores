using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using ProyectoModeradores.Controllers;

namespace ProyectoModeradores.Models.Connection
{
    public class SincroniaDB
    {

        public static void SaveData(int SalaId, int ModeradorId)
        {
            try
            {

                Connections con = new Connections();

                string sql = "EXEC	dbo.SalaUpdateMod " + "@SalaId='" + SalaId.ToString() + "',"
                    + "@ModeradorId='" + ModeradorId.ToString() + "'";
                SqlCommand command = new SqlCommand(sql, con.conectar());
                int cantidad = command.ExecuteNonQuery();
                if (cantidad == 1)
                {
                    
                }
                else
                {
                    
                }
                con.desconectar();

            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
