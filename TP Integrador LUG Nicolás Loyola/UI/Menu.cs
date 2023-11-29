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
    public partial class Menu : Form
    {
        //Declaro una variable de tipo Login que luego necesito instanciar, para poder suscribirle un método y mostrar el form de login
        private Login o_Login;
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            //Instancio la variable que había declarado, y le suscribo el método
            o_Login = new Login();
            o_Login.LoginCorrecto += O_Login_LoginCorrecto;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Pregunto si está seguro que va a cerrar el programa
            DialogResult confirmar = MessageBox.Show("¿Seguro quieres salir?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmar == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Al hacer click sobre "Login" se abrirá el formulario de logueo
            o_Login.ShowDialog();
        }
        private void O_Login_LoginCorrecto(object sender, Login.UsuarioEventArgs e)
        {
            BLL.BLL.Entidades.Usuario_Ent user = e.Usuario;
            //Si el logueo fue correcto y se desencadena el evento, modifico el estado de los ToolStripMenuItem

            try
            {
                if (user.Rol.Id == 0)
                {
                    logoutToolStripMenuItem.Enabled = true;
                    loginToolStripMenuItem.Enabled = false;
                    menuToolStripMenuItem.Enabled = true;
                    sistemaToolStripMenuItem.Enabled = true;
                    MessageBox.Show("Login Correcto");
                }
                else if (user.Rol.Id == 1)
                {
                    logoutToolStripMenuItem.Enabled = true;
                    loginToolStripMenuItem.Enabled = false;
                    menuToolStripMenuItem.Enabled = true;
                    MessageBox.Show("Login Correcto");
                }
                else MessageBox.Show("Se produjo un error!");
            }
            catch (Exception)
            {

                MessageBox.Show("Error");
            }
            
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logoutToolStripMenuItem.Enabled = false;
            loginToolStripMenuItem.Enabled = true;
            menuToolStripMenuItem.Enabled = false;
            sistemaToolStripMenuItem.Enabled = false;
            MessageBox.Show("Hasta la próxima!");
            Funciones.file file = new Funciones.file();
            file.Escribir($"Logout correcto");
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nicolás Loyola 2023, User: nico Pass: nico");
        }

        private void turnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Turnos ofrmTurnos = new Turnos();
            ofrmTurnos.MdiParent = this;
            ofrmTurnos.Show();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuarios ofrmUsuarios = new Usuarios();
            ofrmUsuarios.MdiParent = this;
            ofrmUsuarios.Show();
        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Roles ofrmRoles = new Roles();
            ofrmRoles.MdiParent = this;
            ofrmRoles.Show();
        }

        private void auditoríaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Auditoria ofrmAuditoria = new Auditoria();
            ofrmAuditoria.MdiParent = this;
            ofrmAuditoria.Show();
        }
    }
}
