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
    public partial class Recuperacion : Form
    {
        private ResponsiveHelper responsiveHelper;

        public Recuperacion()
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

        private void btnRecuperar_Click(object sender, EventArgs e)
        {
            // Recoger los datos del formulario
            string correo = txtCorreoRecuperacion.Text.Trim();
            string nuevaContrasena = txtContraseña1.Text.Trim();
            string confirmarContrasena = txtContraseña2.Text.Trim();

            var camposProducto = new Dictionary<string, string>
            {
                { "Recuperacion", correo },
                { "Contraseña", nuevaContrasena },
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
            // Crear una instancia de la clase ClsConexion
            ClsConexion dbHelper = new ClsConexion();

            // Intentar actualizar la contraseña
            bool exito = dbHelper.ActualizarContrasena(correo, nuevaContrasena);

            if (exito)
            {
                MessageBox.Show("Contraseña actualizada con éxito. Ahora puede iniciar sesión.");

                // Redirigir a la pantalla de inicio de sesión
                Form1 PantallaInicio = new Form1();
                PantallaInicio.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Error al actualizar la contraseña. Por favor, intente nuevamente.");
            }
        }

        private void btnVerContraseñaRecu1_CheckedChanged(object sender, EventArgs e)
        {
            if (btnVerContraseñaRecu1.Checked)
            {
                txtContraseña1.PasswordChar = '\0'; // Muestra la contraseña
            }
            else
            {
                txtContraseña1.PasswordChar = '*'; // Oculta la contraseña
            }
        }

        private void btnVerContraseñaRecu2_CheckedChanged(object sender, EventArgs e)
        {
            if (btnVerContraseñaRecu2.Checked)
            {
                txtContraseña2.PasswordChar = '\0'; // Muestra la contraseña
            }
            else
            {
                txtContraseña2.PasswordChar = '*'; // Oculta la contraseña
            }
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
