using MySqlX.XDevAPI.Common;
using Proyecto_DesarrolloSoftware.Proyecto_DesarrolloSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_DesarrolloSoftware
{
    public partial class Registro : Form
    {
        private ResponsiveHelper responsiveHelper;

        public Registro()
        {
            InitializeComponent();
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            // Recoger los datos del formulario
            string correo = txtUsuario.Text.Trim();
            string contrasena = txtContraseña1.Text.Trim();
            string confirmarContrasena = txtContraseña2.Text.Trim();

            var camposProducto = new Dictionary<string, string>
            {
                { "Email", correo },
                { "Contraseña", contrasena },
                { "Contraseña2", confirmarContrasena },
            };

            Validacion validar = new Validacion(camposProducto);
            string mensajeValidacion = "";

            // Validar todos los campos
            if (!validar.Validar(out mensajeValidacion))
            {
                MessageBox.Show(mensajeValidacion);
                return;
            }

            // Crear una instancia de la clase ClsConexion para registrar el usuario
            ClsConexion dbHelper = new ClsConexion();
            bool exito = dbHelper.RegistrarUsuario(correo, contrasena);

            if (exito)
            {
                MessageBox.Show("Usuario registrado con éxito. Ahora puede iniciar sesión.");

                // Redirigir a la pantalla de inicio de sesión
                Form1 PantallaInicio = new Form1();
                PantallaInicio.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Error al registrar el usuario. Por favor, intente nuevamente.");
            }
        }

        private void btnVerContraseñaRegistro1_CheckedChanged(object sender, EventArgs e)
        {
            if (btnVerContraseñaRegistro1.Checked)
            {
                txtContraseña1.PasswordChar = '\0'; // Muestra la contraseña
            }
            else
            {
                txtContraseña1.PasswordChar = '*'; // Oculta la contraseña
            }
        }

        private void btnVerContraseñaRegistro2_CheckedChanged(object sender, EventArgs e)
        {
            if (btnVerContraseñaRegistro2.Checked)
            {
                txtContraseña2.PasswordChar = '\0'; // Muestra la contraseña
            }
            else
            {
                txtContraseña2.PasswordChar = '*'; // Oculta la contraseña
            }
        }
    }
}
