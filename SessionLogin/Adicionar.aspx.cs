using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SessionLogin
{
    public partial class Adicionar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                var senhaEncriptada = BCrypt.Net.BCrypt.HashPassword(txtSenha.Text);

                cmd.Connection = Conexao.Connection;

                cmd.CommandText = @"insert into usuario
                                (nome, login, senha, nivel)
                                    values 
                                (@nome, @login, @senha, @nivel)";

                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@login", txtLogin.Text);
                cmd.Parameters.AddWithValue("@senha", senhaEncriptada);
                cmd.Parameters.AddWithValue("@nivel", ddNivel.Text);
                
                Conexao.Conectar();

                cmd.ExecuteNonQuery();
                Response.Redirect("listar.aspx");
            }
            catch(Exception ex)
            {
                lblResultado.CssClass = "text text-danger";
                lblResultado.Text = $"Error:{ex.Message}";
            }
            finally
            {
                Conexao.Desconectar();
            }

        }
    }
}