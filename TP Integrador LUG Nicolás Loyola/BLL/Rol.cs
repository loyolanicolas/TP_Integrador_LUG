using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    namespace BLL.Clases
    {
        public class Rol_BLL
        {
            public List<BLL.Entidades.Rol_Ent> ListarTodo()
            {
                List<BLL.Entidades.Rol_Ent> lista = new List<BLL.Entidades.Rol_Ent>();

                BLL.Map.Rol_Map rol = new Map.Rol_Map();
                lista = rol.TraerTodo();
                return lista;
            }
        }
    }

    namespace BLL.Entidades
    {
        public class Rol_Ent
        {
            public int Id { get; set; }
            public string Detalle_Rol { get; set; }
        }
    }

    namespace BLL.Map
    {
        class Rol_Map
        {
            public List<BLL.Entidades.Rol_Ent> TraerTodo()
            {
                DAL.sqlserver sqlServer = new DAL.sqlserver();
                SqlConnection ocnn = new SqlConnection();
                ocnn = sqlServer.abrirConexion();

                SqlCommand ocmd = new SqlCommand();
                ocmd.Connection = ocnn;
                ocmd.CommandText = "Roles_TraerTodo";
                ocmd.CommandType = CommandType.StoredProcedure;

                DataTable ds = sqlServer.ejecutarSQL_DT(ocmd);
                List<BLL.Entidades.Rol_Ent> Lista = new List<BLL.Entidades.Rol_Ent>();
                Lista = castDStoEnt(ds);
                sqlServer.cerrarConexion(ocnn);

                return Lista;
            }
            private List<BLL.Entidades.Rol_Ent> castDStoEnt(DataTable dt)
            {
                List<BLL.Entidades.Rol_Ent> list_roles = new List<Entidades.Rol_Ent>();
                foreach (DataRow fila in dt.Rows)
                {
                    BLL.Entidades.Rol_Ent rol = new BLL.Entidades.Rol_Ent();
                    rol.Id = int.Parse(fila["id_rol"].ToString());
                    rol.Detalle_Rol = fila["detalle_rol"].ToString();
                    
                    list_roles.Add(rol);
                }
                return list_roles;
            }
        }
    }
}
