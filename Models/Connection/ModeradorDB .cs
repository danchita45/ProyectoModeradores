using Microsoft.Data.SqlClient;
using ProyectoModeradores.Controllers;
using System.Data;

namespace ProyectoModeradores.Models.Connection
{
    public class ModeradorDB
    {
        public static bool UpdateMod(ProyectoModeradores.Models.Moderador e)
        {

            try
            {

                Connections con = new Connections();

                string sql = "EXEC	dbo.ModeradoresUpdate " + "@Name='" + e.Name.ToString() + "',"
                    + "@StatusId= 1,"
                    + "@AreaId1='" + e.Area1.ToString() + "',"
                    + "@AreaId2='" + e.Area2.ToString() + "',"
                    + "@InstitucionId='" + e.InstitucionId.ToString() + "',"
                    +"@id_Moderador="+e.Id.ToString();



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

        public static bool DeleteMod(int id)
        {
            try
            {

                Connections con = new Connections();

                string sql = "EXEC	dbo.ModeradoresDelete " + "@id_Moderador=" + id.ToString();




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
        public static bool SaveMod(ProyectoModeradores.Models.Moderador e)
        {
           
            try
            {

                Connections con = new Connections();

                string sql = "EXEC	dbo.ModeradoresInsert " + "@Name='" + e.Name.ToString() + "',"

                    + "@StatusId= 1,"
                    + "@InstitucionId='" + e.InstitucionId.ToString() + "'";

                

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


        
        public static DataTable ViewMods()
        {
            try
            {
                Connections con = new Connections();
                string sql = "EXEC dbo.ModeradoresSelectAll";
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
        public static DataTable SelectModByName(string Nombre)
        {
            try
            {

                Connections con = new Connections();

                string sql = "SELECT * FROM dbo.Moderadores WHERE Nombre LIKE '%"+
                    Nombre+
                    "%'";



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

        public static DataTable SelectMod(int id)
        {

            try
            {

                Connections con = new Connections();

                string sql = "EXEC dbo.ModeradoresSelectById @ModeradorId="+id;



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

        public static bool ImportMod(DataTable datos)
        {
            bool ret = true;
            Connections con = new Connections();
            
            using (SqlBulkCopy s = new SqlBulkCopy(con.conectar()))
            {
                s.ColumnMappings.Add("Moderadores","ApellidoP");
                s.ColumnMappings.Add("ApellidoM","ApellidoM");
                s.ColumnMappings.Add("ApellidoM","ApellidoM");
                s.ColumnMappings.Add("Correo","email");
                s.DestinationTableName = "dbo.Moderadores";
                try
                {
                    s.WriteToServer(datos);
                }catch (Exception ex)
                {
                    string st = ex.Message; 
                    ret = false;
                }
            }
            return ret;
        }
    }
}
