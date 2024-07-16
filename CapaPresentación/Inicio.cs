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

namespace CapaPresentación
{
    public partial class Inicio : Form
    {
        private Usuario _Usuario;
        private static Form FormularioActivo = null;
        private Control MenuActivo = null;

        public Inicio(Usuario usuario)
        {
            InitializeComponent();
            _Usuario = usuario;
        }

        private void AbrirFormulario(Control menu, Form formulario)
        {
            // Restaurar color de fondo de menú activo anterior
            if (MenuActivo != null)
            {
                MenuActivo.BackColor = Color.White; // Suponiendo que MenuActivo es una variable miembro
            }

            // Cambiar color de fondo del menú seleccionado
            menu.BackColor = Color.Silver;
            MenuActivo = menu; // Asignar el menú activo actual

            // Cerrar formulario activo anterior
            if (FormularioActivo != null)
            {
                FormularioActivo.Close();
            }

            // Mostrar nuevo formulario
            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.SteelBlue;

            PanelContenedor.Controls.Add(formulario); // 'contenedor' es el control donde se mostrará el formulario
            formulario.Show();
        }



        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hund, int wmsg, int wparam, int Iparam);


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MenuVertical.Width == 62)
            {
                MenuVertical.Width = 167;
            }
            else
            {
                MenuVertical.Width = 62;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario actual (Inicio)
            this.Close();

            // Cerrar otros formularios abiertos, si los hay
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name != "Login") // O cualquier otro formulario que no deba cerrarse
                {
                    form.Close();
                }
            }

            // Finalmente, cerrar la aplicación
            Application.Exit();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnRestaurar.Visible = true;
            btnMaximizar.Visible = false;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState=FormWindowState.Minimized;
        }

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        private void btnReporte_Click_1(object sender, EventArgs e)
        {
            SubMenu.Visible = true;
        }

        private void btnRptVenta_Click(object sender, EventArgs e)
        {
            SubMenu.Visible = false;

            frmReporteVentas frmReporteVentasForm = new frmReporteVentas();

            // Establecer propiedades del formulario hijo
            frmReporteVentasForm.TopLevel = false;
            frmReporteVentasForm.FormBorderStyle = FormBorderStyle.None;
            frmReporteVentasForm.Dock = DockStyle.Fill;

            // Agregar el formulario hijo al PanelContenedor del formulario Inicio
            PanelContenedor.Controls.Add(frmReporteVentasForm);

            // Asegurarse de que el formulario hijo esté en la parte superior
            frmReporteVentasForm.BringToFront();

            // Mostrar el formulario hijo
            frmReporteVentasForm.Show();
        }

        private void btnRptCompra_Click(object sender, EventArgs e)
        {

            SubMenu.Visible = false;

            frmReporteCompras frmReporteComprasForm = new frmReporteCompras();

            // Establecer propiedades del formulario hijo
            frmReporteComprasForm.TopLevel = false;
            frmReporteComprasForm.FormBorderStyle = FormBorderStyle.None;
            frmReporteComprasForm.Dock = DockStyle.Fill;

            // Agregar el formulario hijo al PanelContenedor del formulario Inicio
            PanelContenedor.Controls.Add(frmReporteComprasForm);

            // Asegurarse de que el formulario hijo esté en la parte superior
            frmReporteComprasForm.BringToFront();

            // Mostrar el formulario hijo
            frmReporteComprasForm.Show();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Cerrar Sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Verificar la respuesta del usuario
            if (resultado == DialogResult.Yes)
            {
                // Limpiar campos usando el método público en Login
                Login loginForm = Login.Instancia;
                loginForm.LimpiarCampos();

                // Mostrar el formulario de Login y cerrar el formulario actual (Inicio)
                loginForm.Show();
                this.Close(); // Cierra el formulario actual (Inicio)
            }
        }

        private void frm_closing(object sender, FormClosingEventArgs e)
        {
        }

        private void Cerrarsesion2_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Cerrar Sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Verificar la respuesta del usuario
            if (resultado == DialogResult.Yes)
            {
                // Limpiar campos usando el método público en Login
                Login loginForm = Login.Instancia;
                loginForm.LimpiarCampos();

                // Mostrar el formulario de Login y cerrar el formulario actual (Inicio)
                loginForm.Show();
                this.Close(); // Cierra el formulario actual (Inicio)
            }
        }

        private void guna2Button20_Click(object sender, EventArgs e)
        {
            Productos productosForm = new Productos();

            // Establecer propiedades del formulario hijo
            productosForm.TopLevel = false;
            productosForm.FormBorderStyle = FormBorderStyle.None;
            productosForm.Dock = DockStyle.Fill;

            // Agregar el formulario hijo al PanelContenedor del formulario Inicio
            PanelContenedor.Controls.Add(productosForm);

            // Asegurarse de que el formulario hijo esté en la parte superior
            productosForm.BringToFront();

            // Mostrar el formulario hijo
            productosForm.Show();
        }

        private void guna2Button18_Click(object sender, EventArgs e)
        {
            frmCategoria categoriaForm = new frmCategoria();

            // Establecer propiedades del formulario hijo
            categoriaForm.TopLevel = false;
            categoriaForm.FormBorderStyle = FormBorderStyle.None;
            categoriaForm.Dock = DockStyle.Fill;

            // Agregar el formulario hijo al PanelContenedor del formulario Inicio
            PanelContenedor.Controls.Add(categoriaForm);

            // Asegurarse de que el formulario hijo esté en la parte superior
            categoriaForm.BringToFront();

            // Mostrar el formulario hijo
            categoriaForm.Show();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmClientes clientesForm = new frmClientes();

            // Establecer propiedades del formulario hijo
            clientesForm.TopLevel = false;
            clientesForm.FormBorderStyle = FormBorderStyle.None;
            clientesForm.Dock = DockStyle.Fill;

            // Agregar el formulario hijo al PanelContenedor del formulario Inicio
            PanelContenedor.Controls.Add(clientesForm);

            // Asegurarse de que el formulario hijo esté en la parte superior
            clientesForm.BringToFront();

            // Mostrar el formulario hijo
            clientesForm.Show();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            frmProveedor proveedorForm = new frmProveedor();

            // Establecer propiedades del formulario hijo
            proveedorForm.TopLevel = false;
            proveedorForm.FormBorderStyle = FormBorderStyle.None;
            proveedorForm.Dock = DockStyle.Fill;

            // Agregar el formulario hijo al PanelContenedor del formulario Inicio
            PanelContenedor.Controls.Add(proveedorForm);

            // Asegurarse de que el formulario hijo esté en la parte superior
            proveedorForm.BringToFront();

            // Mostrar el formulario hijo
            proveedorForm.Show();
        }

        private void btnCompra_Click(object sender, EventArgs e)
        {
            frmCompra compraForm = new frmCompra();

            // Establecer propiedades del formulario hijo
            compraForm.TopLevel = false;
            compraForm.FormBorderStyle = FormBorderStyle.None;
            compraForm.Dock = DockStyle.Fill;

            // Agregar el formulario hijo al PanelContenedor del formulario Inicio
            PanelContenedor.Controls.Add(compraForm);

            // Asegurarse de que el formulario hijo esté en la parte superior
            compraForm.BringToFront();

            // Mostrar el formulario hijo
            compraForm.Show();
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            frmVentas ventaForm = new frmVentas();

            // Establecer propiedades del formulario hijo
            ventaForm.TopLevel = false;
            ventaForm.FormBorderStyle = FormBorderStyle.None;
            ventaForm.Dock = DockStyle.Fill;

            // Agregar el formulario hijo al PanelContenedor del formulario Inicio
            PanelContenedor.Controls.Add(ventaForm);

            // Asegurarse de que el formulario hijo esté en la parte superior
            ventaForm.BringToFront();

            // Mostrar el formulario hijo
            ventaForm.Show();
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            frmUsuario frmUsuarioForm = new frmUsuario();

            // Establecer propiedades del formulario hijo
            frmUsuarioForm.TopLevel = false;
            frmUsuarioForm.FormBorderStyle = FormBorderStyle.None;
            frmUsuarioForm.Dock = DockStyle.Fill;

            // Agregar el formulario hijo al PanelContenedor del formulario Inicio
            PanelContenedor.Controls.Add(frmUsuarioForm);

            // Asegurarse de que el formulario hijo esté en la parte superior
            frmUsuarioForm.BringToFront();

            // Mostrar el formulario hijo
            frmUsuarioForm.Show();
        }
    }
}
