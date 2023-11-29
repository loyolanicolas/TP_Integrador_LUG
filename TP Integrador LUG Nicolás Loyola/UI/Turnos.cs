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
    public partial class Turnos : Form
    {
        BLL.BLL.Clases.Turno_BLL oTurno;
        BLL.BLL.Entidades.Turno_Ent oTurnoEnt;
        public Turnos()
        {
            InitializeComponent();
        }

        private void Turnos_Load(object sender, EventArgs e)
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
            //this.dataGridView1.DataSource = oTurno.ListarTodo();
            oTurno = new BLL.BLL.Clases.Turno_BLL();
            List<BLL.BLL.Entidades.Turno_Ent> lista = oTurno.ListarTodo();
            llenarGrilla(lista);
            //this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            oTurno = null;
        }
        private void llenarGrilla(List<BLL.BLL.Entidades.Turno_Ent> l)
        {
            foreach (BLL.BLL.Entidades.Turno_Ent turno in l)
            {
                int indice = dataGridView1.Rows.Add();
                dataGridView1.Rows[indice].Cells["id"].Value = turno.Id;
                dataGridView1.Rows[indice].Cells["fecha_creacion"].Value = turno.Fecha_creacion;
                dataGridView1.Rows[indice].Cells["fecha_u_mod"].Value = turno.Fecha_u_mod;
                dataGridView1.Rows[indice].Cells["id_cliente"].Value = turno.Cliente.Id;
                dataGridView1.Rows[indice].Cells["nombres"].Value = turno.Cliente.Nombres;
                dataGridView1.Rows[indice].Cells["apellidos"].Value = turno.Cliente.Apellidos;
                dataGridView1.Rows[indice].Cells["fecha_turno"].Value = turno.Fecha_turno;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Formulario_Turno ofrmNewTurno = new Formulario_Turno();
            ofrmNewTurno.FormClosed += FormularioTurnoClosed;
            ofrmNewTurno.ShowDialog();
        }
        private void FormularioTurnoClosed(object sender, FormClosedEventArgs e)
        {
            CargarGrilla();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                oTurnoEnt = new BLL.BLL.Entidades.Turno_Ent();
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                BLL.BLL.Entidades.Turno_Ent turnoSelect = new BLL.BLL.Entidades.Turno_Ent();
                turnoSelect.Id = Convert.ToInt32(row.Cells["id"].Value);
                turnoSelect.Fecha_creacion = Convert.ToDateTime(row.Cells["fecha_creacion"].Value);
                turnoSelect.Fecha_u_mod = Convert.ToDateTime(row.Cells["fecha_u_mod"].Value);
                turnoSelect.Cliente = new BLL.BLL.Entidades.Usuario_Ent();
                turnoSelect.Cliente.Id = Convert.ToInt32(row.Cells["Id_cliente"].Value);
                turnoSelect.Cliente.Nombres = Convert.ToString(row.Cells["nombres"].Value);
                turnoSelect.Cliente.Apellidos = Convert.ToString(row.Cells["apellidos"].Value);
                turnoSelect.Fecha_turno = Convert.ToDateTime(row.Cells["fecha_turno"].Value);
                oTurnoEnt = turnoSelect;
                MessageBox.Show("El turno seleccionado es el id: " + oTurnoEnt.Id);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Formulario_Turno ofrmModTurno = new Formulario_Turno(oTurnoEnt);
                ofrmModTurno.FormClosed += FormularioTurnoClosed;
                ofrmModTurno.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, revise haber seleccionado un turno de la grilla. Detalle del error: " + ex.Message);
                Funciones.file file = new Funciones.file();
                file.Escribir($"Error al intentar modificar");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (oTurnoEnt != null)
            {
                DialogResult confirmacion = MessageBox.Show("¿Seguro quieres borrar el turno?", "Confirmación", MessageBoxButtons.YesNo);
                if (confirmacion == DialogResult.Yes)
                {
                    //Para llamar al método Crear/Modificar instancio BLL de Ususario
                    BLL.BLL.Clases.Turno_BLL oTurno = new BLL.BLL.Clases.Turno_BLL();
                    oTurno.Delete(oTurnoEnt);
                    CargarGrilla();
                    Funciones.file file = new Funciones.file();
                    file.Escribir($"Afectación correcta! en turno: {oTurnoEnt.Id} - {oTurnoEnt.Fecha_turno}");
                }
            }
            else
            {
                MessageBox.Show("Error, revise haber seleccionado un turno de la grilla.");
                Funciones.file file = new Funciones.file();
                file.Escribir($"Error al intentar cancelar");
            }
        }
    }
}
