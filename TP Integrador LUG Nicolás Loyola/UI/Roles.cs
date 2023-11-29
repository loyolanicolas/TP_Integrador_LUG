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
    public partial class Roles : Form
    {
        BLL.BLL.Clases.Rol_BLL oRol;
        public Roles()
        {
            InitializeComponent();
            oRol = new BLL.BLL.Clases.Rol_BLL();
        }

        private void Roles_Load(object sender, EventArgs e)
        {
            try
            {
                CargarGrilla();
            }
            catch (Exception)
            {

                MessageBox.Show("Se produjo un error al intentar cargar la grilla");
            }
        }
        private void CargarGrilla()
        {
            this.dataGridView1.DataSource = null;
            List<BLL.BLL.Entidades.Rol_Ent> lista = oRol.ListarTodo();
            llenarGrilla(lista);
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            oRol = null;
        }
        private void llenarGrilla(List<BLL.BLL.Entidades.Rol_Ent> l)
        {
            foreach (BLL.BLL.Entidades.Rol_Ent rol in l)
            {
                int indice = dataGridView1.Rows.Add();
                dataGridView1.Rows[indice].Cells["id"].Value = rol.Id;
                dataGridView1.Rows[indice].Cells["rol"].Value = rol.Detalle_Rol;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Este botón aún se encuentra en construcción, disculpe las molestias");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Este botón aún se encuentra en construcción, disculpe las molestias");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Este botón aún se encuentra en construcción, disculpe las molestias");
        }
    }
}
