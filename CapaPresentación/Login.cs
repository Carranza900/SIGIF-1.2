using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CapaEntidades;
using CapaNegocios;
using System.Data.SqlClient;
using CapaDatos;

namespace CapaPresentación
{
    public partial class Login : Form
    {

        private static Login instancia;

        // Propiedad estática para acceder a la instancia única
        public static Login Instancia
        {
            get
            {
                if (instancia == null || instancia.IsDisposed)
                    instancia = new Login();
                return instancia;
            }
        }
        public Login()
        {
            InitializeComponent();

            // Configurar los textos predeterminados y colores iniciales de los TextBox
            txtuser.Text = "Usuario";
            txtuser.ForeColor = Color.DimGray;

            txtpass.Text = "Contraseña";
            txtpass.ForeColor = Color.DimGray;
            txtpass.UseSystemPasswordChar = false; // Mostrar el texto plano inicialmente
        }

        public void LimpiarCampos()
        {
            txtuser.Text = "Usuario";
            txtuser.ForeColor = Color.DimGray;

            txtpass.Text = "Contraseña";
            txtpass.ForeColor = Color.DimGray;
            txtpass.UseSystemPasswordChar = false; // Mostrar el texto plano inicialmente
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hund, int wmsg, int wparam, int Iparam);

        private void txtuser_Enter(object sender, EventArgs e)
        {
            if (txtuser.Text == "Usuario")
            {
                txtuser.Text = "";
                txtuser.ForeColor = Color.DimGray;
            }
        }

        private void txtuser_Leave(object sender, EventArgs e)
        {
            if (txtuser.Text == "")
            {
                txtuser.Text = "Usuario";
                txtuser.ForeColor = Color.DimGray;
            }
        }

        private void txtpass_Enter(object sender, EventArgs e)
        {
            if (txtpass.Text == "Contraseña")
            {
                txtpass.Text = "";
                txtuser.ForeColor = Color.DimGray;
                txtpass.UseSystemPasswordChar = true;
            }
        }

        private void txtpass_Leave(object sender, EventArgs e)
        {
            if (txtpass.Text == "")
            {
                txtpass.Text = "Contraseña";
                txtpass.ForeColor = Color.DimGray;
                txtpass.UseSystemPasswordChar = false;
            }
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string username = txtuser.Text.Trim();
            string password = txtpass.Text.Trim();
            string mensaje = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("iniciosesion", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    cmd.Parameters.AddWithValue("@Nombre", username);
                    cmd.Parameters.AddWithValue("@Clave", password);

                    // Parámetro de salida
                    SqlParameter outputParameter = new SqlParameter();
                    outputParameter.ParameterName = "@mensaje";
                    outputParameter.SqlDbType = SqlDbType.VarChar;
                    outputParameter.Size = 30;
                    outputParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParameter);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    // Recuperar el mensaje de salida
                    mensaje = cmd.Parameters["@mensaje"].Value.ToString();
                }

                if (mensaje.StartsWith("Bienvenido"))
                {
                    // Usuario autenticado correctamente
                    Console.WriteLine($"Usuario autenticado: '{username}'");
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Aquí debes pasar el objeto Usuario correctamente
                    Usuario usuarioAutenticado = new Usuario { Nombre = username }; // Necesitas llenar todos los campos necesarios del objeto Usuario
                    Inicio form = new Inicio(usuarioAutenticado);
                    form.Show();
                    this.Hide();

                    // Maneja el evento de cierre de la ventana Inicio para mostrar nuevamente Login
                    form.FormClosing += frm_closing;
                }
                else
                {
                    // Usuario no autenticado
                    Console.WriteLine($"Autenticación fallida para: '{username}'");
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar iniciar sesión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            Login loginForm = Login.Instancia;
            loginForm.LimpiarCampos();
            loginForm.Show();
        }
    }
}
