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
        public class Usuario_BLL
        {
            public BLL.Entidades.Usuario_Ent TraerUnoPorUser(string usuario)
            {
                BLL.Map.Usuario_Map map = new Map.Usuario_Map();
                BLL.Entidades.Usuario_Ent entidad = map.Traer(usuario);
                map = null;

                return entidad;
            }
            public List<BLL.Entidades.Usuario_Ent> ListarTodo()
            {
                List<BLL.Entidades.Usuario_Ent> lista = new List<BLL.Entidades.Usuario_Ent>();

                BLL.Map.Usuario_Map user = new Map.Usuario_Map();
                lista = user.TraerTodo();
                return lista;
            }
            public bool Crear(BLL.Entidades.Usuario_Ent pNewUser)
            {
                BLL.Map.Usuario_Map crea = new Map.Usuario_Map();
                crea.Crear(pNewUser);
                return true;
            }

        }
    }

    namespace BLL.Entidades
    {
        public class Usuario_Ent
        {
            public int Id { get; set; }
            public DateTime Fecha_creacion { get; set; }
            public DateTime Fecha_u_mod { get; set; }
            public int Dni { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string Telefono { get; set; }
            public DateTime Fecha_nacimiento { get; set; }
            public string Usuario { get; set; }
            public string Password { get; set; }
            public int Estado { get; set; }
            public BLL.Entidades.Rol_Ent Rol { get; set; }
        }
    }

    namespace BLL.Map
    {
        class Usuario_Map
        {
            public BLL.Entidades.Usuario_Ent Traer(string usuario)
            {
                DAL.sqlserver sqlserver = new DAL.sqlserver();
                SqlConnection ocnn = new SqlConnection();
                ocnn = sqlserver.abrirConexion();
                SqlCommand ocmd = new SqlCommand();
                ocmd.CommandText = "Usuario_TraerPorUser";
                ocmd.CommandType = CommandType.StoredProcedure;
                ocmd.Connection = ocnn;

                SqlParameter p1 = new SqlParameter();
                p1.Value = usuario;
                p1.DbType = DbType.String;
                p1.ParameterName = "@usuario";
                p1.Direction = ParameterDirection.Input;
                ocmd.Parameters.Add(p1);

                DataTable dt = sqlserver.ejecutarSQL_DT(ocmd);
                List<BLL.Entidades.Usuario_Ent> Lista = new List<Entidades.Usuario_Ent>();
                Lista = castDStoEnt(dt);
                sqlserver.cerrarConexion(ocnn);

                return Lista[0];

            }
            public List<BLL.Entidades.Usuario_Ent> TraerTodo()
            {
                DAL.sqlserver sqlServer = new DAL.sqlserver();
                SqlConnection ocnn = new SqlConnection();
                ocnn = sqlServer.abrirConexion();

                SqlCommand ocmd = new SqlCommand();
                ocmd.Connection = ocnn;
                ocmd.CommandText = "Usuario_TraerTodo";
                ocmd.CommandType = CommandType.StoredProcedure;

                DataTable ds = sqlServer.ejecutarSQL_DT(ocmd);
                List<BLL.Entidades.Usuario_Ent> Lista = new List<BLL.Entidades.Usuario_Ent>();
                Lista = castDStoEnt(ds);
                sqlServer.cerrarConexion(ocnn);

                return Lista;
            }
            private List<BLL.Entidades.Usuario_Ent> castDStoEnt(DataTable dt)
            {
                List<BLL.Entidades.Usuario_Ent> list_usr = new List<Entidades.Usuario_Ent>();
                foreach (DataRow fila in dt.Rows)
                {
                    BLL.Entidades.Usuario_Ent usr = new BLL.Entidades.Usuario_Ent();
                    usr.Id = int.Parse(fila["id"].ToString());
                    usr.Fecha_creacion = DateTime.Parse(fila["fecha_creacion"].ToString()).Date;
                    usr.Fecha_u_mod = DateTime.Parse(fila["fecha_u_mod"].ToString()).Date;
                    usr.Dni = Convert.ToInt32(fila["dni"]);
                    usr.Nombres = Convert.ToString(fila["nombres"]);
                    usr.Apellidos = Convert.ToString(fila["apellidos"]);
                    usr.Telefono = Convert.ToString(fila["telefono"]);
                    usr.Fecha_nacimiento = DateTime.Parse(fila["fecha_nacimiento"].ToString()).Date;
                    usr.Usuario = Convert.ToString(fila["usuario"]);
                    usr.Password = Convert.ToString(fila["password"]);
                    usr.Estado = Convert.ToInt32(fila["estado"]);
                    usr.Rol = new BLL.Entidades.Rol_Ent();
                    usr.Rol.Id = int.Parse(fila["rol"].ToString());
                    usr.Rol.Detalle_Rol = fila["detalle_rol"].ToString();
                    list_usr.Add(usr);
                }
                return list_usr;
            }
            public bool Crear(BLL.Entidades.Usuario_Ent pNewUser)
            {
                DAL.sqlserver sqlServer = new DAL.sqlserver();
                SqlConnection ocnn = new SqlConnection();
                ocnn = sqlServer.abrirConexion();

                SqlCommand ocmd = new SqlCommand();
                ocmd.CommandText = "Crear_Usuario";
                ocmd.CommandType = CommandType.StoredProcedure;

                ocmd.Connection = ocnn;

                ocmd.Parameters.Add("@id", SqlDbType.Int);
                ocmd.Parameters["@id"].Value = pNewUser.Id;
                ocmd.Parameters.Add("@fecha_creacion", SqlDbType.Date);
                ocmd.Parameters["@fecha_creacion"].Value = pNewUser.Fecha_creacion;
                ocmd.Parameters.Add("@fecha_u_mod", SqlDbType.Date);
                ocmd.Parameters["@fecha_u_mod"].Value = pNewUser.Fecha_u_mod;
                ocmd.Parameters.Add("@dni", SqlDbType.Int);
                ocmd.Parameters["@dni"].Value = pNewUser.Dni;
                ocmd.Parameters.Add("@nombres", SqlDbType.VarChar, 25);
                ocmd.Parameters["@nombres"].Value = pNewUser.Nombres;
                ocmd.Parameters.Add("@apellidos", SqlDbType.VarChar, 25);
                ocmd.Parameters["@apellidos"].Value = pNewUser.Apellidos;
                ocmd.Parameters.Add("@telefono", SqlDbType.VarChar, 25);
                ocmd.Parameters["@telefono"].Value = pNewUser.Telefono;
                ocmd.Parameters.Add("@fecha_nacimiento", SqlDbType.Date);
                ocmd.Parameters["@fecha_nacimiento"].Value = pNewUser.Fecha_nacimiento;
                ocmd.Parameters.Add("@usuario", SqlDbType.VarChar, 25);
                ocmd.Parameters["@usuario"].Value = pNewUser.Usuario;
                ocmd.Parameters.Add("@password", SqlDbType.VarChar, 25);
                ocmd.Parameters["@password"].Value = pNewUser.Password;
                ocmd.Parameters.Add("@estado", SqlDbType.Int);
                ocmd.Parameters["@estado"].Value = pNewUser.Estado;
                ocmd.Parameters.Add("@rol", SqlDbType.Int);
                ocmd.Parameters["@rol"].Value = pNewUser.Rol.Id;

                DataTable ds = sqlServer.ejecutarSQL_DT(ocmd);

                sqlServer.cerrarConexion(ocnn);

                return true;
            }
        }
    }
}
