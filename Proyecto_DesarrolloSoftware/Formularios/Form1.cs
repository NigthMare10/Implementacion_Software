using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Proyecto_DesarrolloSoftware.Proyecto_DesarrolloSoftware;
using static TheArtOfDevHtmlRenderer.Adapters.RGraphicsPath;

namespace Proyecto_DesarrolloSoftware
{
    public partial class Form1 : Form
    {
        public static class GlobalData
        {
            public static string UserRole { get; set; }
            public static string UserName { get; set; }
        }
        //dbHelper es una instancia de la clase ClsConexion

        private ClsConexion dbHelper;

        private ResponsiveHelper responsiveHelper;

        public Form1()
        {
            InitializeComponent();
            dbHelper = new ClsConexion();

            // Iniciar el movimiento del formulario
            FormMover.IniciarMovimiento(guna2ShadowPanel1);
            FormMover.IniciarMovimiento(guna2Panel1);

            responsiveHelper = new ResponsiveHelper(this);

            // Guardar los tamaños y ubicaciones originales de los controles
            SaveOriginalSizesAndLocations(this.Controls);

            // Asignar eventos a los botones
            BtnFullScreen.Click += BtnFullScreen_Click;
            BtnExitFullScreen.Click += BtnExitFullScreen_Click;
        }

        private void SaveOriginalSizesAndLocations(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                control.SetOriginalSizeAndLocation();
                if (control.HasChildren)
                {
                    SaveOriginalSizesAndLocations(control.Controls);
                }
            }
        }

        private void BtnFullScreen_Click(object sender, EventArgs e)
        {
            // Entrar en modo pantalla completa y despues se desactiva el boton de pantalla completa para evitar errores

            responsiveHelper.EnterFullScreen();
            BtnFullScreen.Enabled = false;
        }

        private void BtnExitFullScreen_Click(object sender, EventArgs e)
        {
            // Salir del modo pantalla completa y despues se activa el boton de pantalla completa para evitar errores
            responsiveHelper.ExitFullScreen();
            BtnFullScreen.Enabled = true;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            string username = txtCorreo.Text.Trim();
            string password = txtContraseña.Text.Trim();

            var camposProducto = new Dictionary<string, string>
            {
                { "Recuperacion", username },
            };

            Validacion validar = new Validacion(camposProducto);
            string mensajeValidacion = "";

            // Validar todos los campos
            if (!validar.Validar(out mensajeValidacion))
            {
                MessageBox.Show(mensajeValidacion);
                return;
            }

            string name = dbHelper.GetUserName(username, password);
            string role = dbHelper.GetUserRole(username, password);

            if (name != null && role != null)
            {
                // Guardamos el rol del usuario
                GlobalData.UserRole = role;
                // Guardamos el email unico del usuario
                GlobalData.UserName = name;

                NuevoProducto nuevoProducto = new NuevoProducto(GlobalData.UserRole);
                nuevoProducto.Show();
                this.Hide(); // Oculta el formulario actual (Form1)
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }
        private void btnRecuperar_Click(object sender, EventArgs e)
        {
            Recuperacion recuperacion = new Recuperacion();
            recuperacion.Show();
            this.Hide();
        }

        private void btnRegistroLogin_Click(object sender, EventArgs e)
        {
            Registro registro = new Registro();
            registro.Show();
            this.Hide();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnVerContraseñaLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (btnVerContraseñaLogin.Checked)
            {
                txtContraseña.PasswordChar = '\0'; // Muestra la contraseña
            }
            else
            {
                txtContraseña.PasswordChar = '*'; // Oculta la contraseña
            }
        }
    }
}
