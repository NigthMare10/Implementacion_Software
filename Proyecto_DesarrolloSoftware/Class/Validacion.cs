using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Proyecto_DesarrolloSoftware.Proyecto_DesarrolloSoftware;
using Guna.UI2.WinForms;
using static TheArtOfDevHtmlRenderer.Adapters.RGraphicsPath;
using System.Net.Mail;

namespace Proyecto_DesarrolloSoftware
{
    internal class Validacion
    {
        private Dictionary<string, string> _campos;
        private ClsConexion dbHelper = new ClsConexion();

        public Validacion(Dictionary<string, string> campos)
        {
            _campos = campos;
            PrecioDecimal = 0;
            Existencias = 0;
        }

        public bool Validar(out string mensaje)
        {
            foreach (var campo in _campos)
            {
                string llaveCampo = campo.Key;
                string valorCampo = campo.Value;

                if (string.IsNullOrEmpty(valorCampo))
                {
                    mensaje = "Por favor, no deje campos vacíos";
                    return false;
                }

                switch (llaveCampo)
                {
                    case "NombreEmpleado":
                    case "NombreCliente":
                    case "ApellidoEmpleado":
                    case "ApellidoCliente":
                        if (valorCampo.Length < 3 || valorCampo.Length > 50 || !EsSoloLetras(valorCampo))
                        {
                            mensaje = "El campo debe tener entre 3 y 50 caracteres y solo puede contener letras y espacios.";
                            return false;
                        }
                        break;

                    case "Email":
                        if (!EsEmailValido(valorCampo))
                        {
                            mensaje = "Por favor, ingrese un correo electrónico válido.";
                            return false;
                        }
                        if (dbHelper.UsuarioExiste(valorCampo))
                        {
                            mensaje = "Ya existe un usuario con este correo electrónico.";
                            return false;
                        }
                        break;

                    case "Telefono":
                        if (valorCampo.Length < 8)
                        {
                            mensaje = "El telefono debe de tener al menos 8 caracteres";
                            return false;
                        }

                        if (Regex.IsMatch(valorCampo, @"(\d)\1{4}"))
                        {
                            mensaje = "El teléfono no puede contener cinco números consecutivos iguales.";
                            return false;
                        }
                        break;

                    case "Identidad":
                        if (valorCampo.Length != 13 || !EsSoloNumeros(valorCampo))
                        {
                            mensaje = "El número de identidad debe tener exactamente 13 dígitos numéricos.";
                            return false;
                        }

                        string codigoDepto = valorCampo.Substring(0, 2);
                        string codigoMunicipio = valorCampo.Substring(2, 2);
                        string identidadAño = valorCampo.Substring(4, 4);
                        string ultimosDigitos = valorCampo.Substring(8, 5);
                        int año;
                        bool añoValido = int.TryParse(identidadAño, out año);
                        int añoActual = DateTime.Now.Year;
                        int añoMaximo = añoActual - 110;

                        if (!EsCodigoDepartamentoValido(codigoDepto))
                        {
                            mensaje = "El código del departamento no es válido.";
                            return false;
                        }

                        if (!EsCodigoMunicipioValido(codigoDepto, codigoMunicipio))
                        {
                            mensaje = "El código del municipio no es válido.";
                            return false;
                        }

                        if (!añoValido || año > añoActual || año < añoMaximo)
                        {
                            mensaje = $"El año debe estar entre {añoMaximo} y {añoActual}.";
                            return false;
                        }

                        if(ultimosDigitos == "00000")
                        {
                            mensaje = "Los últimos cinco dígitos no pueden ser 00000";
                            return false;
                        }

                        break;

                    case "Fecha":
                        if (!EsFechaValida(valorCampo))
                        {
                            mensaje = "Por favor, ingrese una fecha válida en el formato DD/MM/YYYY.";
                            return false;
                        }
                        break;

                    case "DireccionEmpleado":
                        if (valorCampo.Length < 10)
                        {
                            mensaje = "La dirección debe de tener al menos 10 caracteres";
                            return false;
                        }
                        break;

                    // Case IDVendedor valida que el ID proporcionado esté en la base de datos
                    case "IDVendedor":
                        List<int> idEmpleados = dbHelper.ObtenerIDsEmpleados();
                        if (!idEmpleados.Contains(int.Parse(valorCampo)))
                        {
                            mensaje = "El ID del vendedor no se encuentra en la base de datos";
                            return false;
                        }
                        break;

                    // Producto
                    case "NombreProducto":
                        if (valorCampo.Length < 3)
                        {
                            mensaje = "El nombre del producto debe de tener al menos 3 caracteres";
                            return false;
                        }
                        break;

                    case "PrecioProducto":
                        if (!decimal.TryParse(valorCampo, out decimal precioDecimal) || precioDecimal <= 0)
                        {
                            mensaje = "Por favor, ingrese un precio válido.";
                            return false;
                        }
                        PrecioDecimal = precioDecimal;
                        break;

                    case "Existencias":
                        if (!int.TryParse(valorCampo, out int existencias) || existencias < 0)
                        {
                            mensaje = "Por favor, ingrese una cantidad de existencias válida.";
                            return false;
                        }
                        Existencias = existencias;
                        break;
                    case "Contraseña":
                    case "Contraseña2":
                        if (valorCampo.Length < 8)
                        {
                            mensaje = "La contraseña debe tener al menos 8 caracteres.";
                            return false;
                        }
                        if (_campos["Contraseña"] != _campos["Contraseña2"])
                        {
                            mensaje = "Las contraseñas no coinciden.";
                            return false;
                        }
                        break;
                    case "Recuperacion":
                        if (!dbHelper.UsuarioExiste(valorCampo))
                        {
                            mensaje = "No existe un usuario con este correo electrónico.";
                            return false;
                        }
                        break;
                        // AÑADIR MÁS CASES CON EL NOMBRE DE SU VALIDACIÓN DONDE LA LLAMEN

                }
            }

            mensaje = "Validaciones realizadas exitosamente";
            return true;
        }


        private bool EsEmailValido(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private bool EsSoloLetras(string input)
        {
            return Regex.IsMatch(input, @"^[\p{L}\s]+$");
        }

        private bool EsSoloNumeros(string input)
        {
            return Regex.IsMatch(input, @"^\d+$");
        }

        private bool EsFechaValida(string fecha)
        {
            DateTime parsedDate;
            return DateTime.TryParseExact(fecha, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate);
        }
        
        // Función para verificar que el código del depto sea válido entre 0 y 18
        private bool EsCodigoDepartamentoValido(string codigoDepto)
        {
            int codigo;
            return int.TryParse(codigoDepto, out codigo) && codigo >= 1 && codigo <= 18;
        }

        private bool EsCodigoMunicipioValido(string codigoDepto, string codigoMunicipio)
        {
            // Define the maximum number of municipalities for each department
            var municipiosCodigos = new Dictionary<int, int>
            {
                { 1, 8 },  // Atlántida
                { 2, 10 }, // Colón
                { 3, 21 }, // Comayagua
                { 4, 23 }, // Copán
                { 5, 12 }, // Cortés
                { 6, 16 }, // Choluteca
                { 7, 19 }, // El Paraíso
                { 8, 28 }, // Francisco Morazán
                { 9, 6 },  // Gracias a Dios
                { 10, 17 }, // Intibucá
                { 11, 4 }, // Islas de la Bahía
                { 12, 19 }, // La Paz
                { 13, 28 }, // Lempira
                { 14, 16 }, // Ocotepeque
                { 15, 23 }, // Olancho
                { 16, 28 }, // Santa Bárbara
                { 17, 9 },  // Valle
                { 18, 11 }  // Yoro
            };

            int department, municipality;
            if (int.TryParse(codigoDepto, out department) && int.TryParse(codigoMunicipio, out municipality))
            {
                if (municipiosCodigos.ContainsKey(department))
                {
                    return municipality >= 1 && municipality <= municipiosCodigos[department];
                }
            }
            return false;
        }


        // Keypresses 
        //Keypress textbox Email, no acepta espacios y solo acepta ciertos caracteres especiales
        public void TBEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            Guna2TextBox textBox = sender as Guna2TextBox;

            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '@' && e.KeyChar != '.' &&
                 e.KeyChar != '_' && e.KeyChar != '-' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            // Previene múltiples arrobas
            if (e.KeyChar == '@')
            {
                if (textBox.Text.Contains('@') || textBox.Text.Length == 0)
                {
                    e.Handled = true;
                    return;
                }
            }

            // Límite de caracteres en caja, 60 caracteres
            if (textBox.Text.Length >= 60 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Previene simbolos especiales después del arroba
            if (textBox.Text.Contains('@'))
            {
                int arrobaIndex = textBox.Text.IndexOf('@');
                string despuesArroba = textBox.Text.Substring(arrobaIndex + 1);

                // No se puede iniciar con punto
                if (despuesArroba.Length == 0 && e.KeyChar == '.')
                {
                    e.Handled = true;
                    return;
                }

                // Previene simbolos consecutivos después del arroba
                if (despuesArroba.Length > 0 && (e.KeyChar == '.' || e.KeyChar == '_' || e.KeyChar == '-') && despuesArroba.EndsWith(e.KeyChar.ToString()))
                {
                    e.Handled = true;
                    return;
                }

                // Previene más de dos puntos después del arroba
                int contadorPuntos = despuesArroba.Count(c => c == '.');
                if (contadorPuntos >= 2 && e.KeyChar == '.')
                {
                    e.Handled = true;
                    return;
                }

                // Solo permite letras, puntos y numeros después del arroba
                if (e.KeyChar != '.' && !char.IsLetterOrDigit(e.KeyChar))
                {
                    e.Handled = true;
                    return;
                }
            }

            // Previene caracteres especiales seguidos de caracteres especiales
            if (textBox.Text.Length > 0 && (e.KeyChar == '.' || e.KeyChar == '_' || e.KeyChar == '-'))
            {
                if (textBox.Text.EndsWith(e.KeyChar.ToString()))
                {
                    e.Handled = true;
                    return;
                }

                // Chequea por caracteres consecutivos
                if (textBox.Text.Length > 1 && textBox.Text[textBox.Text.Length - 2] == e.KeyChar)
                {
                    e.Handled = true;
                    return;
                }
            }

            // No se puede iniciar email con caracteres especiales
            if (textBox.Text.Length == 0 && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            // después de un caracter especial no puede ir la arroba
            if (textBox.Text.Length > 0 && e.KeyChar == '@')
            {
                if (textBox.Text.EndsWith(".") || textBox.Text.EndsWith("_") || textBox.Text.EndsWith("-"))
                {
                    e.Handled = true;
                    return;
                }
            }

        }

        // Keypress textbox teléfono, solo acepta números
        public void TBTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Guna2TextBox textBox = sender as Guna2TextBox;


            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            if (textBox.Text.Length == 0 && !char.IsControl(e.KeyChar)) // Primer caracter solo permite 2, 3, 8 y 9
            {
                if (e.KeyChar != '2' && e.KeyChar != '3' && e.KeyChar != '8' && e.KeyChar != '9')
                {
                    e.Handled = true;
                }
            }

            // Límite de caracteres en caja, 8 caracteres
            if (textBox.Text.Length >= 8 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Keypress textbox Nombre Empleado, acepta letras y espacios únicamente
        public void TBNombreEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            Guna2TextBox textBox = sender as Guna2TextBox;

            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }

            // Límite de caracteres en caja, 50 caracteres
            if (textBox.Text.Length >= 50 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Este evento se llama cuando el TextBox pierde el foco
        public void TBNombreEmpleado_Leave(object sender, EventArgs e)
        {
            Guna2TextBox textBox = sender as Guna2TextBox;

            // Verificar si el contenido es solo espacios
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                MessageBox.Show("El campo no puede estar vacío o solo contener espacios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox.Focus();
            }
        }

        // Este evento se llama cada vez que el texto en el TextBox cambia
        public void TBNombreEmpleado_TextChanged(object sender, EventArgs e)
        {
            Guna2TextBox textBox = sender as Guna2TextBox;

            // Verificar si el contenido es solo espacios
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = string.Empty;
            }
        }

        // Evento que no deje escribir espacios consecutivos
        public void LimpiarEspaciosConsecutivos(object sender, EventArgs e)
        {
            Guna2TextBox textBox = sender as Guna2TextBox;

            int posicion = textBox.SelectionStart;

            string nuevoTexto = System.Text.RegularExpressions.Regex.Replace(textBox.Text, @"\s{2,}", " ");
            if (nuevoTexto != textBox.Text)
            {
                textBox.Text = nuevoTexto;
                textBox.SelectionStart = posicion > textBox.Text.Length ? textBox.Text.Length : posicion;
            }
        }

        // Keypress textbox búsqueda de empleado, acepta letras y espacios (Búsqueda por nombre)
        public void TBBuscarEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            Guna2TextBox textBox = sender as Guna2TextBox;

            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }

            // Límite de caracteres en caja, 60 caracteres
            if (textBox.Text.Length >= 60 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Keypress textbox dirección, acepta letras y números
        public void TBDireccionEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            Guna2TextBox textBox = sender as Guna2TextBox;

            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ' && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Límite de caracteres en caja, 100 caracteres
            if (textBox.Text.Length >= 100 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Keypress identidad
        public void Identidad_Keypress(object sender, KeyPressEventArgs e)
        {
            Guna2TextBox textBox = sender as Guna2TextBox;


            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            // Límite de caracteres en caja, 13 caracteres
            if (textBox.Text.Length >= 13 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Keypress IDEmpleado
        public void IDEmpleado_Keypress(object sender, KeyPressEventArgs e)
        {
            Guna2TextBox textBox = sender as Guna2TextBox;


            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            // Límite de caracteres en caja, 5 caracteres
            if (textBox.Text.Length >= 5 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Keypress nombre producto
        public void NombreProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Guna2TextBox textBox = sender as Guna2TextBox;

            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }

            // Límite de caracteres en caja, 60 caracteres
            if (textBox.Text.Length >= 60 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Keypress precio producto
        public void PrecioProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Guna2TextBox textBox = sender as Guna2TextBox;

            // Permite números y un punto decimal
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && textBox.Text.Contains("."))
            {
                e.Handled = true;
            }
            
            // Límite de 8 caracteres
            if (textBox.Text.Length >= 8 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public decimal PrecioDecimal { get; private set; }
        public int Existencias { get; private set; }


    }
}
