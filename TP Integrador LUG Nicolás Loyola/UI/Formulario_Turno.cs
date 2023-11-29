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
    public partial class Formulario_Turno : Form
    {
        //Es lo que voy a instanciar para llenar con los datos de los campos del formulario
        BLL.BLL.Entidades.Turno_Ent newTurno;
        //Es lo que voy a instanciar para autocompletar el combobox con los usuarios disponibles
        List<BLL.BLL.Entidades.Usuario_Ent> listaUsuarios;
        //Es lo que voy a instanciar para llenar la prop de tipo usuario que tendrá newTurno
        BLL.BLL.Entidades.Usuario_Ent usuarioSeleccionado;
        public Formulario_Turno() //CONSTRUCTOR SOBRECARGADO, si no manda objeto es NUEVO
        {
            InitializeComponent();
            //Instancio el objeto que voy a llenar con los campos del form
            newTurno = new BLL.BLL.Entidades.Turno_Ent();
            //Instancio una variable que me permita listar los usuarios disponibles
            BLL.BLL.Clases.Usuario_BLL auxuser = new BLL.BLL.Clases.Usuario_BLL();

            //Instancio la lista de usuarios para completar el combobox
            listaUsuarios = new List<BLL.BLL.Entidades.Usuario_Ent>();
            //Lleno la lista llamando a la funcion que trae los usuarios
            listaUsuarios = auxuser.ListarTodo();
            //Recorro la lista de usuarios y lleno el combobox con los usuarios disponibles
            foreach (var user in listaUsuarios)
            {
                if (user.Rol.Id == 1)
                {
                    comboBox1.Items.Add($"{user.Id}-{user.Nombres}-{user.Apellidos}");
                }
            }
            //Configuro el combobox para que por defecto ya esté elegido el primero de los usuarios
            comboBox1.SelectedIndex = 0;

            //Pongo el ID en 0 para indicar que es un nuevo usuario
            textBox_id.Text = Convert.ToString('0');
        }
        public Formulario_Turno(BLL.BLL.Entidades.Turno_Ent pTurno) // CONSTRUCTOR SOBRECARGADO, si mando objeto es MODIFICAR
        {
            InitializeComponent();
            //Instancio el objeto que voy a llenar con los textbox
            newTurno = new BLL.BLL.Entidades.Turno_Ent();
            //Instancio una variable que me permita listar los usuarios disponibles
            BLL.BLL.Clases.Usuario_BLL auxuser = new BLL.BLL.Clases.Usuario_BLL();
            //Instancio la lista de usuarios para completar el combobox
            listaUsuarios = new List<BLL.BLL.Entidades.Usuario_Ent>();
            //Lleno la lista llamando a la funcion que trae los roles
            listaUsuarios = auxuser.ListarTodo();
            //Recorro la lista de usuarios y lleno el combobox con los usuarios disponibles
            foreach (var user in listaUsuarios)
            {
                if (user.Rol.Id == 1)
                {
                    comboBox1.Items.Add($"{user.Id}-{user.Nombres}-{user.Apellidos}");
                }
            }
            //Cargar campos del formulario con el objeto recibido por parámetro
            llenarCamposForm(pTurno);
        }
        private void llenarCamposForm(BLL.BLL.Entidades.Turno_Ent pTurno)
        {
            if (pTurno != null && pTurno.Cliente != null)
            {
                try
                {
                    //Si vengo de MODIFICAR lleno los campos con los datos del turno previamente seleccionado en la grilla
                    textBox_id.Text = Convert.ToString(pTurno.Id);
                    dateTimePicker_creacion.Value = pTurno.Fecha_creacion;
                    dateTimePicker_modificacion.Value = pTurno.Fecha_u_mod;
                    comboBox1.Text = $"{pTurno.Cliente.Id}-{pTurno.Cliente.Nombres}-{pTurno.Cliente.Apellidos}";
                    dateTimePicker_turno.Value = pTurno.Fecha_turno;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, revise haber seleccionado un turno de la grilla. Detalle del error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Error, revise haber seleccionado un turno de la grilla.");
                this.Close();
            } 

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Instancio una entidad usuario para poder llenar la prop de tipo usuario que tiene el newTurno
            usuarioSeleccionado = new BLL.BLL.Entidades.Usuario_Ent();
            //Intento llenar el objeto newTurno con los datos de los campos del formulario...
            try
            {
                newTurno.Id = Convert.ToInt32(textBox_id.Text);
                newTurno.Fecha_creacion = dateTimePicker_creacion.Value.Date;
                newTurno.Fecha_u_mod = dateTimePicker_modificacion.Value.Date;
                string auxcombobox = comboBox1.Text.Split('-')[0];
                usuarioSeleccionado = listaUsuarios.Find(user => user.Id == Convert.ToInt32(auxcombobox));
                newTurno.Cliente = new BLL.BLL.Entidades.Usuario_Ent();
                newTurno.Cliente.Id = usuarioSeleccionado.Id;
                newTurno.Fecha_turno = dateTimePicker_turno.Value.Date;
                
                //Para llamar al método Crear/Modificar instancio BLL de Turno
                BLL.BLL.Clases.Turno_BLL oTurno = new BLL.BLL.Clases.Turno_BLL();
                //Llamo al método Crear/Modificar pasándole como parámetro el objeto que llené con los campos del form.
                oTurno.Crear(newTurno);
                //Cierro este form y se va a ejecutar un evento para recargar la grilla.
                this.Close();
                Funciones.file file = new Funciones.file();
                file.Escribir($"Afectación correcta! en turno ID: {newTurno.Id}");
            }
            catch (Exception ex)
            {
                //Si hubo un error al tratar de llenar el objeto y guardarlo en la base me mostrará un MSGBOX
                MessageBox.Show("Se produjo un error. Detalle: " + ex.Message);
                Funciones.file file = new Funciones.file();
                file.Escribir($"Error en escritura de turno");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
