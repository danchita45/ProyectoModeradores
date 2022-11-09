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
                string sql;
                if (e.Description != null)
                {
                    sql = "EXEC	dbo.SalaInsert " + "@Code='" + e.Code.ToString() + "',"
                    + "@Description='" + e.Description.ToString() + "',"
                    + "@AreaId='" + e.AreaId.ToString() + "',"
                     + "@Fecha='" + e.Fecha.ToString() + "',"
                    + "@Bloque" + e.Bloque.ToString() + "',"
                    + "@Salon" + e.Salon.ToString() + "',"
                    + "@Ubicacion" + e.Ubicacion.ToString()
                    + "@StatusId= 1";
                }
                sql = "EXEC	dbo.SalaInsert " + "@Code='" + e.Code.ToString() + "',"
                + "@Description='Desconocido',"
                + "@AreaId='" + e.AreaId.ToString() + "',"
                 + "@Fecha='" + e.Fecha.ToString() + "',"
                + "@Bloque='" + e.Bloque.ToString() + "',"
                + "@Salon='" + e.Salon.ToString() + "',"
                + "@Ubicacion='" + e.Ubicacion.ToString()+"',"
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

        public static int serchArea(string AreaD,string Code)
        {
            int areaId = 0;
            string Codes = "";
            if(AreaD=="Área V")
            {
                areaId = 1;
            }
            try
            {

                Connections con = new Connections();

                string sql = "EXEC dbo.SalaAsignArea  @ST='" + AreaD + "'";



                SqlCommand command = new SqlCommand(sql, con.conectar());
                SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                
                
                DataTable dt = new DataTable();
                dt.Load(dr);
                foreach(DataRow lRow in dt.Rows)
                {
                    areaId= Convert.ToInt32(lRow["AreaId"].ToString());
                    break;
                }
                con.desconectar();
                return areaId;
            }
            catch (
            Exception ex)
            {
                Code=Codes;
                return 0;
            }
        }
    }
}
