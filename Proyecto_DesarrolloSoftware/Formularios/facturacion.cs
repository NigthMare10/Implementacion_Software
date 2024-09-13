using Guna.UI2.WinForms;
using MySqlX.XDevAPI.Common;
using Proyecto_DesarrolloSoftware.Proyecto_DesarrolloSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using static Proyecto_DesarrolloSoftware.ClaseProductos;
using static Proyecto_DesarrolloSoftware.Form1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Proyecto_DesarrolloSoftware
{

    public partial class facturacion : Form
    {

        private List<Producto> productosSeleccionados;
        private ResponsiveHelper responsiveHelper;
        public facturacion(List<Producto> productosSeleccionados)
        {
            InitializeComponent();
            
            AgregarProductosAlDtgFactura(productosSeleccionados);
            this.productosSeleccionados = productosSeleccionados ?? throw new ArgumentNullException(nameof(productosSeleccionados));
            imprimirTileButton2.Enabled = false;
            AgregarProductosAlDtgFactura(this.productosSeleccionados);
            CalcularTotalFactura();
           
            facturadescuentoRadioButton1.CheckedChanged += new EventHandler(facturadescuentoRadioButton1_CheckedChanged);
           
            responsiveHelper = new ResponsiveHelper(this);
           
            fechafacturaTextBox3.MaxLength = 10; // Establecer longitud máxima a 10 caracteres

            // Clase validación 
            var campos = new Dictionary<string, string>();
            Validacion validar = new Validacion(campos);


            // Guardar los tamaños y ubicaciones originales de los controles
            SaveOriginalSizesAndLocations(this.Controls);

            // Asignar eventos a los botones
            BtnFullScreen.Click += BtnFullScreen_Click;
            BtnExitFullScreen.Click += BtnExitFullScreen_Click;

            FormMover.IniciarMovimiento(guna2ShadowPanel22);
            FormMover.IniciarMovimiento(guna2ShadowPanel13);

            string userRole = GlobalData.UserRole;

            // Handlers de keypress
            identidadfacturaTextBox4.KeyPress += validar.Identidad_Keypress;
            txtdireccion.KeyPress += validar.TBDireccionEmpleado_KeyPress;
            telefonofacturaTextBox2.KeyPress += validar.TBTelefono_KeyPress;
            txtnombrecliente.KeyPress += validar.TBNombreEmpleado_KeyPress;
            txtapellido_cliente.KeyPress += validar.TBNombreEmpleado_KeyPress;
            txtnombrecliente.Leave += validar.TBNombreEmpleado_Leave;
            txtnombrecliente.TextChanged += validar.TBNombreEmpleado_TextChanged;
            txtapellido_cliente.Leave += validar.TBNombreEmpleado_Leave;
            txtapellido_cliente.TextChanged += validar.TBNombreEmpleado_TextChanged;
            txtdireccion.Leave += validar.TBNombreEmpleado_Leave;
            txtdireccion.TextChanged += validar.TBNombreEmpleado_TextChanged;
            txtnombrecliente.TextChanged += validar.LimpiarEspaciosConsecutivos;
            txtapellido_cliente.TextChanged += validar.LimpiarEspaciosConsecutivos;
            txtdireccion.TextChanged += validar.LimpiarEspaciosConsecutivos;



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

        string empleadoNombre;
        int empleadoIDGlobal;
        private void facturacion_Load(object sender, EventArgs e)
        {
            // Obtiene al empleado que está realizando la venta y lo asigna a variables y labels
            ClsConexion dbHelper = new ClsConexion();
            empleadoIDGlobal = Convert.ToInt32(dbHelper.RecibirDatoPorCorreo(GlobalData.UserName, "Empleado_id"));
            string nombretemp = dbHelper.RecibirDatoPorCorreo(GlobalData.UserName, "Empleado_nombre");
            string apellidotemp = dbHelper.RecibirDatoPorCorreo(GlobalData.UserName, "Empleado_apellido");
            empleadoNombre = nombretemp + " " + apellidotemp;

            lblIDEmpleadoConectado.Text = "#" + empleadoIDGlobal.ToString();
            lblNombreEmpleadoConectado.Text = empleadoNombre.ToString();

            // Asignar fecha actual al textbox fechafacturaTextBox3
            fechafacturaTextBox3.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnpagofinal_Click(object sender, EventArgs e)
        {
            ClsConexion dbHelper = new ClsConexion();
            var campos = new Dictionary<string, string>
            {
                { "NombreCliente", txtnombrecliente.Text },
                { "ApellidoCliente", txtapellido_cliente.Text },
                { "Telefono", telefonofacturaTextBox2.Text },
                { "FechaFactura", fechafacturaTextBox3.Text },
                { "Identidad", identidadfacturaTextBox4.Text },
                { "DireccionEmpleado", txtdireccion.Text }
            };

            var validador = new Validacion(campos);

            if (!validador.Validar(out string mensaje))
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtgfactura.Rows.Count == 0)
            {
                MessageBox.Show("No hay productos en la factura. Por favor agregue productos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verificar que existencias no sean menores a cantidad seleccionada
            int codigoProducto = 0;
            int cantidad = 0;
            int existenciasProducto = 0;

            foreach (DataGridViewRow dr in dtgfactura.Rows)
            {
                if (dr.IsNewRow) continue;

                codigoProducto = Convert.ToInt32(dr.Cells[0].Value);
                cantidad = Convert.ToInt32(dr.Cells[1].Value);

                // Si existencias son menores a la cantidad seleccionada no realiza el pago 
                existenciasProducto = Convert.ToInt32(dbHelper.RecibirDatoProducto(codigoProducto, "Productos_exitencias"));
                if (cantidad > existenciasProducto) {
                    MessageBox.Show("No hay las suficientes existencias para uno de los productos");
                    return;
                }

            }
            
            // Inserta al cliente y retorna la ID, si ya existe retorna la ID existente y si no, crea uno nuevo y retorna esa
            int idCliente = dbHelper.InsertarCliente(txtnombrecliente.Text, txtapellido_cliente.Text, txtdireccion.Text, telefonofacturaTextBox2.Text, identidadfacturaTextBox4.Text);
            int empleadoID = Convert.ToInt32(empleadoIDGlobal); // Obtiene ID vendedor como int
            decimal facturaTotal = total;
            string fechaString = "";

            // Obtener fecha y cambiarle el formato para hacerla compatible con MySQL
            DateTime facturaFecha;
            if (DateTime.TryParseExact(fechafacturaTextBox3.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out facturaFecha))
            {
                facturaFecha = facturaFecha.Add(DateTime.Now.TimeOfDay);

                fechaString = facturaFecha.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                MessageBox.Show("Fecha de factura inválida.");
                return;
            }
            // Inserta factura con los campos proporcionados
            int facturaID = dbHelper.InsertarFactura(idCliente, empleadoID, facturaTotal, fechaString);

            // Iteración de filas en DGV para añadirlas a factura detalle
            foreach (DataGridViewRow dr in dtgfactura.Rows)
            {
                if (dr.IsNewRow) continue;

                codigoProducto = Convert.ToInt32(dr.Cells[0].Value);
                cantidad = Convert.ToInt32(dr.Cells[1].Value);

                dbHelper.InsertarFacturaDetalle(facturaID, codigoProducto, cantidad); // Procedimiento almacena el detalle y resta la cantidad de productos en tabla productos
            }


            MessageBox.Show("Pago exitoso, factura #"+facturaID.ToString(), "Pago", MessageBoxButtons.OK);

            imprimirTileButton2.Enabled = true;

            btnpagofinal.Enabled = false;

        }

        private void facturadescuentoRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            CalcularTotalFactura();
        }

        private void imprimirTileButton2_Click(object sender, EventArgs e)
        {
            ReporteFactura  reportForm = new ReporteFactura();

            // Mostrar el formulario ReportForm
            reportForm.Show();
            this.Hide();

        }
        decimal total = 0;
        //funcion CalcularTotalFactura
        private void CalcularTotalFactura()
        {
            decimal subtotal = productosSeleccionados.Sum(p => p.PrecioTotal);
            label212.Text = $"L {subtotal:N2}";

            // Calcular el 15% de descuento
            decimal impuesto = subtotal * 0.15m;
            label8.Text = $"L {impuesto:N2}";

            // Calcular el total con descuento si el radio button está seleccionado
            total = subtotal + impuesto;
            if (facturadescuentoRadioButton1.Checked)
            {
                total -= total * 0.40m;
            }

            label10.Text = $"L {total:N2}";
        }

        private void AgregarProductosAlDtgFactura(List<Producto> productos)
        {
            dtgfactura.Rows.Clear(); // Asegúrate de limpiar las filas antes de agregar nuevas

            foreach (var producto in productos)
            {
                int rowIndex = dtgfactura.Rows.Add();
                DataGridViewRow row = dtgfactura.Rows[rowIndex];
                row.Cells[0].Value = producto.Codigo; // Asignar el número secuencial
                row.Cells[1].Value = producto.Cantidad;
                row.Cells[2].Value = producto.Descripcion;
                row.Cells[3].Value = producto.PrecioUnitario;
                row.Cells[4].Value = producto.PrecioTotal;
            }
        }
       
        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            NuevoProducto nuevoProducto = new NuevoProducto(GlobalData.UserRole);
            nuevoProducto.Show();
            
        }

    }
}
