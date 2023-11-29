using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class Usuarios : Form
    {
        BLL.BLL.Clases.Usuario_BLL oUsuario;
        BLL.BLL.Entidades.Usuario_Ent oUser;

        public Usuarios()
        {
            InitializeComponent();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            dataGridView1.CellClick += dataGridView1_CellClick;
            try
            {
                CargarGrilla();
                dataGridView1.ClearSelection();
            }
            catch (Exception)
            {

                MessageBox.Show("Se produjo un error al intentar cargar la grilla");
            }
        }

        private void CargarGrilla()
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            //this.dataGridView1.DataSource = oUsuario.ListarTodo();
            oUsuario = new BLL.BLL.Clases.Usuario_BLL();
            List<BLL.BLL.Entidades.Usuario_Ent> lista = oUsuario.ListarTodo();
            llenarGrilla(lista);
            //this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //this.dataGridView1.AutoResizeColumns();
            oUsuario = null;
        }

        private void llenarGrilla(List<BLL.BLL.Entidades.Usuario_Ent> l)
        {
            foreach (BLL.BLL.Entidades.Usuario_Ent user in l)
            {
                int indice = dataGridView1.Rows.Add();
                dataGridView1.Rows[indice].Cells["id"].Value = user.Id;
                dataGridView1.Rows[indice].Cells["fecha_creacion"].Value = user.Fecha_creacion;
                dataGridView1.Rows[indice].Cells["fecha_u_mod"].Value = user.Fecha_u_mod;
                dataGridView1.Rows[indice].Cells["dni"].Value = user.Dni;
                dataGridView1.Rows[indice].Cells["nombres"].Value = user.Nombres;
                dataGridView1.Rows[indice].Cells["apellidos"].Value = user.Apellidos;
                dataGridView1.Rows[indice].Cells["telefono"].Value = user.Telefono;
                dataGridView1.Rows[indice].Cells["fecha_nacimiento"].Value = user.Fecha_nacimiento;
                dataGridView1.Rows[indice].Cells["usuario"].Value = user.Usuario;
                dataGridView1.Rows[indice].Cells["password"].Value = user.Password;
                dataGridView1.Rows[indice].Cells["estado"].Value = user.Estado;
                dataGridView1.Rows[indice].Cells["rol"].Value = user.Rol.Id;
                dataGridView1.Rows[indice].Cells["detalle_rol"].Value = user.Rol.Detalle_Rol;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Formulario_Usuario ofrmNewUser = new Formulario_Usuario();
            ofrmNewUser.FormClosed += FormularioUsuarioClosed;
            ofrmNewUser.ShowDialog();
        }
        private void FormularioUsuarioClosed(object sender, FormClosedEventArgs e)
        {
            CargarGrilla();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                oUser = new BLL.BLL.Entidades.Usuario_Ent();
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                BLL.BLL.Entidades.Usuario_Ent userSelect = new BLL.BLL.Entidades.Usuario_Ent();
                userSelect.Id = Convert.ToInt32(row.Cells["id"].Value);
                userSelect.Fecha_creacion = Convert.ToDateTime(row.Cells["fecha_creacion"].Value);
                userSelect.Fecha_u_mod = Convert.ToDateTime(row.Cells["fecha_u_mod"].Value);
                userSelect.Dni = Convert.ToInt32(row.Cells["dni"].Value);
                userSelect.Nombres = Convert.ToString(row.Cells["nombres"].Value);
                userSelect.Apellidos = Convert.ToString(row.Cells["apellidos"].Value);
                userSelect.Telefono = Convert.ToString(row.Cells["telefono"].Value);
                userSelect.Fecha_nacimiento = Convert.ToDateTime(row.Cells["fecha_nacimiento"].Value);
                userSelect.Usuario = Convert.ToString(row.Cells["usuario"].Value);
                userSelect.Password = Convert.ToString(row.Cells["password"].Value);
                userSelect.Estado = Convert.ToInt32(row.Cells["estado"].Value);
                userSelect.Rol = new BLL.BLL.Entidades.Rol_Ent();
                userSelect.Rol.Id = Convert.ToInt32(row.Cells["rol"].Value);
                userSelect.Rol.Detalle_Rol = Convert.ToString(row.Cells["detalle_rol"].Value);
                oUser = userSelect;
                MessageBox.Show("El usuario seleccionado es el id: " + oUser.Id);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Formulario_Usuario ofrmModUser = new Formulario_Usuario(oUser);
                ofrmModUser.FormClosed += FormularioUsuarioClosed;
                ofrmModUser.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, revise haber seleccionado un usuario de la grilla. Detalle del error: " + ex.Message);
                Funciones.file file = new Funciones.file();
                file.Escribir($"Error al intentar modificar");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (oUser != null)
            {
                try
                {
                    DialogResult confirmacion = MessageBox.Show("¿Seguro quieres borrar el usuario?", "Confirmación", MessageBoxButtons.YesNo);
                    if (confirmacion == DialogResult.Yes)
                    {
                        //Para llamar al método Crear/Modificar instancio BLL de Ususario
                        BLL.BLL.Clases.Usuario_BLL oUsuario = new BLL.BLL.Clases.Usuario_BLL();
                        //Llamo al método Crear/Modificar pasándole como parámetro el objeto que llené con los campos del form.
                        //Para hacer la baja lógica lo único que cambio es la prop estado a 1
                        oUser.Estado = 1;
                        oUsuario.Crear(oUser);
                        CargarGrilla();
                        Funciones.file file = new Funciones.file();
                        file.Escribir($"Afectación correcta! en usuario: {oUser.Id} - {oUser.Nombres} - {oUser.Apellidos}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, revise haber seleccionado un usuario de la grilla. Detalle del error: " + ex.Message);
                    Funciones.file file = new Funciones.file();
                    file.Escribir($"Error al ejecutar baja");
                }
            }
            else
            {
                MessageBox.Show("Error, revise haber seleccionado un usuario de la grilla.");
                Funciones.file file = new Funciones.file();
                file.Escribir($"Error al ejecutar baja");
            }
            
            
        }
    }
}
