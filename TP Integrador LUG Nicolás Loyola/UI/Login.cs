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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button_Login_Click(object sender, EventArgs e)
        {
            //Capa seguridad también tiene una clase llamada login que valida la conexión del usuario
            Seguridad.Login login = new Seguridad.Login();
            try
            {
                BLL.BLL.Entidades.Usuario_Ent ok = login.validarUsuario(this.textBox_usuario.Text, this.textBox_password.Text);
                if (ok != null)
                {
                    try
                    {
                        //Desencadeno el evento
                        OnLoginCorrecto(new UsuarioEventArgs(ok));
                        Funciones.file file = new Funciones.file();
                        file.Escribir($"Login correcto, usuario: {ok.Id}-{ok.Nombres}-{ok.Apellidos}");
                        this.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Se produjo un error en el Login, contacte al administrador");
                    }
                }
                else
                {
                    MessageBox.Show("Login Incorrecto");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Usuario incorrecto");
            }
            
            
        }
        //Declaro el método para desencadenar el evento
        protected virtual void OnLoginCorrecto(UsuarioEventArgs e)
        {
            LoginCorrecto?.Invoke(this, e);
        }

        //Evento con el que voy a modificar el estado de los ToolStripMenuItem si el login es correcto
        public event EventHandler<UsuarioEventArgs> LoginCorrecto;

        public class UsuarioEventArgs : EventArgs
        {
            public BLL.BLL.Entidades.Usuario_Ent Usuario { get; set; }

            public UsuarioEventArgs(BLL.BLL.Entidades.Usuario_Ent usuario)
            {
                Usuario = usuario;
            }
        }
    }
}
