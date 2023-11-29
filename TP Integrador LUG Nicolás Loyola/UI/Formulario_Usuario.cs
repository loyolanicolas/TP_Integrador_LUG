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
    public partial class Formulario_Usuario : Form
    {
        //Es lo que voy a instanciar para llenar con los datos de los campos del formulario
        BLL.BLL.Entidades.Usuario_Ent newUser;
        //Es lo que voy a instanciar para autocompletar el combobox con los roles disponibles
        List<BLL.BLL.Entidades.Rol_Ent> listaRoles;
        //Es lo que voy a instanciar para llenar la prop de tipo rol que tendrá newUser
        BLL.BLL.Entidades.Rol_Ent rolSeleccionado;
        public Formulario_Usuario() //CONSTRUCTOR SOBRECARGADO, si no manda objeto es NUEVO
        {
            InitializeComponent();
            //Instancio el objeto que voy a llenar con los textbox
            newUser = new BLL.BLL.Entidades.Usuario_Ent();
            //Instancio una variable que me permita listar los roles disponibles
            BLL.BLL.Clases.Rol_BLL auxrol = new BLL.BLL.Clases.Rol_BLL();

            //Instancio la lista de roles para completar el combobox
            listaRoles = new List<BLL.BLL.Entidades.Rol_Ent>();
            //Lleno la lista llamando a la funcion que trae los roles
            listaRoles = auxrol.ListarTodo();
            //Recorro la lista de roles y lleno el combobox con los roles disponibles
            foreach (var rol in listaRoles)
            {
                comboBox_rol.Items.Add(rol.Detalle_Rol);
            }
            //Configuro el combobox para que por defecto ya esté elegido el primero de los roles
            comboBox_rol.SelectedIndex = 0;
            //Pongo el ID en 0 para indicar que es un nuevo usuario
            textBox_id.Text = Convert.ToString('0');
        }
        public Formulario_Usuario(BLL.BLL.Entidades.Usuario_Ent pUser) // CONSTRUCTOR SOBRECARGADO, si mando objeto es MODIFICAR
        {
            InitializeComponent();
            //Instancio el objeto que voy a llenar con los textbox
            newUser = new BLL.BLL.Entidades.Usuario_Ent();
            //Instancio una variable que me permita listar los roles disponibles
            BLL.BLL.Clases.Rol_BLL auxrol = new BLL.BLL.Clases.Rol_BLL();
            //Instancio la lista de roles para completar el combobox
            listaRoles = new List<BLL.BLL.Entidades.Rol_Ent>();
            //Lleno la lista llamando a la funcion que trae los roles
            listaRoles = auxrol.ListarTodo();
            //Recorro la lista de roles y lleno el combobox con los roles disponibles
            foreach (var rol in listaRoles)
            {
                comboBox_rol.Items.Add(rol.Detalle_Rol);
            }
            //Cargar campos del formulario con el objeto recibido por parámetro
            llenarCamposForm(pUser);
        }

        private void llenarCamposForm(BLL.BLL.Entidades.Usuario_Ent pUser)
        {
            if (pUser != null && pUser.Rol != null)
            {
                try
                {
                    //Si vengo de MODIFICAR lleno los campos con los datos del usuario previamente seleccionado en la grilla
                    textBox_id.Text = Convert.ToString(pUser.Id);
                    dateTimePicker_creacion.Value = pUser.Fecha_creacion;
                    dateTimePicker_modificacion.Value = pUser.Fecha_u_mod;
                    textBox_dni.Text = Convert.ToString(pUser.Dni);
                    textBox_nombres.Text = pUser.Nombres;
                    textBox_apellidos.Text = pUser.Apellidos;
                    textBox_telefonos.Text = pUser.Telefono;
                    dateTimePicker_nacimiento.Value = pUser.Fecha_nacimiento;
                    textBox_username.Text = pUser.Usuario;
                    textBox_password.Text = pUser.Password;
                    comboBox_rol.Text = Convert.ToString(pUser.Rol.Detalle_Rol);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, revise haber seleccionado un usuario de la grilla. Detalle del error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Error, revise haber seleccionado un usuario de la grilla.");
                this.Close();
            }
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Instancio una entidad rol para poder llenar la prop de tipo rol que tiene el newUser
            rolSeleccionado = new BLL.BLL.Entidades.Rol_Ent();
            //Intento llenar el objeto newUser con los datos de los campos del formulario...
            try
            {
                newUser.Id = Convert.ToInt32(textBox_id.Text);
                newUser.Fecha_creacion = dateTimePicker_creacion.Value.Date;
                newUser.Fecha_u_mod = dateTimePicker_modificacion.Value.Date;
                newUser.Dni = Convert.ToInt32(textBox_dni.Text);
                newUser.Nombres = Convert.ToString(textBox_nombres.Text);
                newUser.Apellidos = Convert.ToString(textBox_apellidos.Text);
                newUser.Telefono = Convert.ToString(textBox_telefonos.Text);
                newUser.Fecha_nacimiento = dateTimePicker_nacimiento.Value.Date;
                newUser.Usuario = Convert.ToString(textBox_username.Text);
                newUser.Password = Convert.ToString(textBox_password.Text);
                newUser.Estado = 0;
                rolSeleccionado = listaRoles.Find(rol => rol.Detalle_Rol == comboBox_rol.Text);
                newUser.Rol = new BLL.BLL.Entidades.Rol_Ent();
                newUser.Rol.Id = rolSeleccionado.Id;
                newUser.Rol.Detalle_Rol = rolSeleccionado.Detalle_Rol;
                //Para llamar al método Crear/Modificar instancio BLL de Ususario
                BLL.BLL.Clases.Usuario_BLL oUsuario = new BLL.BLL.Clases.Usuario_BLL();
                //Llamo al método Crear/Modificar pasándole como parámetro el objeto que llené con los campos del form.
                oUsuario.Crear(newUser);
                //Cierro este form y se va a ejecutar un evento para recargar la grilla.
                this.Close();
                Funciones.file file = new Funciones.file();
                file.Escribir($"ABM Correcto! en usuario: {newUser.Nombres} - {newUser.Apellidos}");
            }
            catch (Exception ex)
            {
                //Si hubo un error al tratar de llenar el objeto y guardarlo en la base me mostrará un MSGBOX
                MessageBox.Show("Se produjo un error. Detalle: " + ex.Message);
                Funciones.file file = new Funciones.file();
                file.Escribir($"Error en ABM");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
