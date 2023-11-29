using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; //Ado no conectado
using System.Data.SqlClient; //Ado conectado
using System.IO;

namespace DAL
{
    public class sqlserver
    {
        private SqlConnection cnn;

        public sqlserver()
        {
            this.cnn = new SqlConnection();
        }

        private string armarSqlConnection()
        {
            //String de conexión a la BD
            return "Data Source=DESKTOP-8SR0SLN\\SQLEXPRESS;Initial Catalog=TP_LUG;User ID=nicolas;Password=nicolas";
        }
        public SqlConnection abrirConexion()
        {
             this.cnn.ConnectionString = armarSqlConnection();

             try
             {
                while (this.cnn.State == ConnectionState.Closed)
                {
                    this.cnn.Open();
                }
                }
                catch (Exception ex)
                {
                    reportarErrores(ex.Message);
                }
            return this.cnn;
        }

        public void cerrarConexion(SqlConnection param_cnn)
        {
            if (this.cnn.State == ConnectionState.Open)
            {
                param_cnn.Close();
            }
        }

        public DataTable ejecutarSQL_DT(SqlCommand param_cmd)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = param_cmd;
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Funciones.file file = new Funciones.file();
                file.Escribir(ex.Message);
            }
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        
        private void reportarErrores(string detalle_error)
        {
            StreamWriter archivo = new StreamWriter("Bitacora.txt", true);
            archivo.WriteLine(DateTime.Now.ToString() + ";" + detalle_error);
            archivo.Close();
        }
    }
}
