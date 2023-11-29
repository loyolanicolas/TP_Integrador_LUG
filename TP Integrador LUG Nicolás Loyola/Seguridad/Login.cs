using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad
{
    public class Login
    {
        public BLL.BLL.Entidades.Usuario_Ent validarUsuario(string p_usuario, string p_clave)
        {
            BLL.BLL.Clases.Usuario_BLL user = new BLL.BLL.Clases.Usuario_BLL();
            BLL.BLL.Entidades.Usuario_Ent usuario = user.TraerUnoPorUser(p_usuario);
            if (p_usuario == usuario.Usuario && p_clave == usuario.Password)
            {
                return usuario;
            }
            return null;
        }
    }
}
