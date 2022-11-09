using Microsoft.Data.SqlClient;
using ProyectoModeradores.Controllers;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace ProyectoModeradores.Models.Connection
{
    public class ModeradorDB
    {
        public static string GETSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public static bool UpdatePassMod(ProyectoModeradores.Models.Moderador e)
        {
            

            try
            {

                Connections con = new Connections();

                string sql = "EXEC	dbo.ModeradoresUpdatePass " 
                    + "@AreaId1='" + e.Area1.ToString() + "',"
                    + "@AreaId2='" + e.Area2.ToString() + "',"
                    + "@password='" + e.Password + "',"
                    + "@id_Moderador=" + e.Id.ToString();



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

        public static bool UpdateMod(ProyectoModeradores.Models.Moderador e)
        {

            try
            {

                Connections con = new Connections();

                string sql = "EXEC	dbo.ModeradoresUpdate " + "@Name='" + e.Name.ToString() + "',"
                    + "@StatusId= 1,"
                    + "@AreaId1='" + e.Area1.ToString() + "',"
                    + "@AreaId2='" + e.Area2.ToString() + "',"
                    + "@email='" + e.email.ToString() + "',"
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
                    + "@email='" + e.email.ToString() + "',"
                    + "@AreaId1='" + e.Area1.ToString() + "',"
                    + "@AreaId2='" + e.Area2.ToString() + "',"
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

                string sql = "EXEC dbo.ModeradoresSelectAllByName @Name='"+Nombre+"'";



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
