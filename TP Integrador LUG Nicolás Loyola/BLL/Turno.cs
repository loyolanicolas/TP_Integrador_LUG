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
        public class Turno_BLL
        {
            public List<BLL.Entidades.Turno_Ent> ListarTodo()
            {
                List<BLL.Entidades.Turno_Ent> lista = new List<BLL.Entidades.Turno_Ent>();

                BLL.Map.Turno_Map turno = new Map.Turno_Map();
                lista = turno.TraerTodo();
                return lista;
            }
            public bool Crear(BLL.Entidades.Turno_Ent pNewTurno)
            {
                BLL.Map.Turno_Map crea = new Map.Turno_Map();
                crea.Crear(pNewTurno);
                return true;
            }
            public bool Delete(BLL.Entidades.Turno_Ent pTurno)
            {
                BLL.Map.Turno_Map borra = new Map.Turno_Map();
                borra.Delete(pTurno);
                return true;
            }
        }
    }

    namespace BLL.Entidades
    {
        public class Turno_Ent
        {
            public int Id { get; set; }
            public DateTime Fecha_creacion { get; set; }
            public DateTime Fecha_u_mod { get; set; }
            public BLL.Entidades.Usuario_Ent Cliente { get; set; }
            public DateTime Fecha_turno { get; set; }
        }
    }

    namespace BLL.Map
    {
        class Turno_Map
        {
            public List<BLL.Entidades.Turno_Ent> TraerTodo()
            {
                DAL.sqlserver sqlServer = new DAL.sqlserver();
                SqlConnection ocnn = new SqlConnection();
                ocnn = sqlServer.abrirConexion();

                SqlCommand ocmd = new SqlCommand();
                ocmd.Connection = ocnn;
                ocmd.CommandText = "Turnos_TraerTodo";
                ocmd.CommandType = CommandType.StoredProcedure;

                DataTable ds = sqlServer.ejecutarSQL_DT(ocmd);
                List<BLL.Entidades.Turno_Ent> Lista = new List<BLL.Entidades.Turno_Ent>();
                Lista = castDStoEnt(ds);
                sqlServer.cerrarConexion(ocnn);

                return Lista;
            }
            private List<BLL.Entidades.Turno_Ent> castDStoEnt(DataTable dt)
            {
                List<BLL.Entidades.Turno_Ent> list_turnos = new List<Entidades.Turno_Ent>();
                foreach (DataRow fila in dt.Rows)
                {
                    BLL.Entidades.Turno_Ent turno = new BLL.Entidades.Turno_Ent();
                    turno.Id = int.Parse(fila["id_turno"].ToString());
                    turno.Fecha_creacion = DateTime.Parse(fila["fecha_creacion"].ToString()).Date;
                    turno.Fecha_u_mod = DateTime.Parse(fila["fecha_u_mod"].ToString()).Date;
                    turno.Cliente = new BLL.Entidades.Usuario_Ent();
                    turno.Cliente.Id = int.Parse(fila["id_cliente"].ToString());
                    turno.Cliente.Nombres = fila["nombres"].ToString();
                    turno.Cliente.Apellidos = fila["apellidos"].ToString();
                    turno.Fecha_turno = DateTime.Parse(fila["fecha_turno"].ToString()).Date;

                    list_turnos.Add(turno);
                }
                return list_turnos;
            }
            public bool Crear(BLL.Entidades.Turno_Ent pNewTurno)
            {
                DAL.sqlserver sqlServer = new DAL.sqlserver();
                SqlConnection ocnn = new SqlConnection();
                ocnn = sqlServer.abrirConexion();

                SqlCommand ocmd = new SqlCommand();
                ocmd.CommandText = "Crear_Turno";
                ocmd.CommandType = CommandType.StoredProcedure;

                ocmd.Connection = ocnn;

                ocmd.Parameters.Add("@id", SqlDbType.Int);
                ocmd.Parameters["@id"].Value = pNewTurno.Id;
                ocmd.Parameters.Add("@fecha_creacion", SqlDbType.Date);
                ocmd.Parameters["@fecha_creacion"].Value = pNewTurno.Fecha_creacion;
                ocmd.Parameters.Add("@fecha_u_mod", SqlDbType.Date);
                ocmd.Parameters["@fecha_u_mod"].Value = pNewTurno.Fecha_u_mod;
                ocmd.Parameters.Add("@cliente_id", SqlDbType.Int);
                ocmd.Parameters["@cliente_id"].Value = pNewTurno.Cliente.Id;
                ocmd.Parameters.Add("@fecha_turno", SqlDbType.Date);
                ocmd.Parameters["@fecha_turno"].Value = pNewTurno.Fecha_turno;
                

                DataTable ds = sqlServer.ejecutarSQL_DT(ocmd);
                
                sqlServer.cerrarConexion(ocnn);

                return true;
            }
            public bool Delete(BLL.Entidades.Turno_Ent pTurno)
            {
                DAL.sqlserver sqlServer = new DAL.sqlserver();
                SqlConnection ocnn = new SqlConnection();
                ocnn = sqlServer.abrirConexion();

                SqlCommand ocmd = new SqlCommand();
                ocmd.CommandText = "Borra_Turno";
                ocmd.CommandType = CommandType.StoredProcedure;

                ocmd.Connection = ocnn;

                ocmd.Parameters.Add("@id", SqlDbType.Int);
                ocmd.Parameters["@id"].Value = pTurno.Id;

                DataTable ds = sqlServer.ejecutarSQL_DT(ocmd);

                sqlServer.cerrarConexion(ocnn);

                return true;
            }
        }
        
    }
}

