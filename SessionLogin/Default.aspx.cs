using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SessionLogin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                string usuario = txtUser.Text;
                string senha = txtPsw.Text;

                // recuperar senha do banco

                cmd.Connection = Conexao.Connection;

                cmd.CommandText = @"select senha from usuario where login = @usuario";

                cmd.Parameters.AddWithValue("@login", usuario);

                Conexao.Conectar();

                string senhaEncriptada = Convert.ToString(cmd.ExecuteScalar());// recebe um único dado do banco e passa para a variável

                if (string.IsNullOrEmpty(senhaEncriptada)) {
                    throw new Exception("Usuário ou Senha Inválido");
                }
                if (BCrypt.Net.BCrypt.Verify(senha, senhaEncriptada)) {
                    
                    cmd.CommandText = @"select nivel from usuario where login = @login";
                    
                    cmd.Parameters.AddWithValue("@login", usuario);

                    string nivel = Convert.ToString(cmd.ExecuteScalar());

                    FormsAuthentication.RedirectFromLoginPage(nivel, false);

                    Session["Perfil"] = nivel;
                }
                else
                {
                    throw new Exception("Usuário ou Senha Inválido");
                }



            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
    }
}