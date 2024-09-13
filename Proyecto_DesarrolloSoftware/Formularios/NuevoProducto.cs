using Proyecto_DesarrolloSoftware.Componentes;
using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Guna.UI2.WinForms;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Proyecto_DesarrolloSoftware.Proyecto_DesarrolloSoftware;
using System.Windows.Forms.DataVisualization.Charting;
using Mysqlx.Resultset;
using System.Globalization;
using static TheArtOfDevHtmlRenderer.Adapters.RGraphicsPath;
using System.Drawing.Text;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using MySql.Data.MySqlClient;

namespace Proyecto_DesarrolloSoftware
{
    public partial class NuevoProducto : Form
    {
        private ClaseProductos claseProductos;
        private ClsConexion dbHelper;
        private ResponsiveHelper responsiveHelper;
        System.Drawing.Image ImagenActual;
        private Guna2Button botonSeleccionadoG; // trackea el botón seleccionado

        // Constructor que acepta el rol del usuario
        public NuevoProducto(string role)
        {
            InitializeComponent();
            EnviarEmail enviarEmail = new EnviarEmail();
            enviarEmail.CheckAndSendEmail();

            dtgProductos.CellContentClick += dtgProductos_CellContentClick;
            ImagenActual = imgProducto.Image;
            LlenarFlowLayoutPanel();

            // Inicia instancia de validacion
            var campos = new Dictionary<string, string>();
            Validacion validacion = new Validacion(campos);

            // Iniciar el movimiento del formulario
            FormMover.IniciarMovimiento(guna2Panel2);
            FormMover.IniciarMovimiento(PanelBotones);
            clickBoton();
            this.Shown += new System.EventHandler(this.NuevoProducto_Shown);

            // DB Helper
            dbHelper = new ClsConexion();

            // Inicializa la claseProductos con el flowLayoutPanel2
            claseProductos = new ClaseProductos(flowLayoutPanel2);

            // Manejadores de eventos 
            btnCapsula.Click += NuevoProducto_Shown;
            btnLiquido.Click += NuevoProducto_Shown;
            btnPolvo.Click += NuevoProducto_Shown;
            btnInyectable.Click += NuevoProducto_Shown;

            TBApellidoEmpleado.KeyPress += validacion.TBNombreEmpleado_KeyPress;
            TBNombreEmpleado.KeyPress += validacion.TBNombreEmpleado_KeyPress;
            TBEmail.KeyPress += validacion.TBEmail_KeyPress;
            TBTelefono.KeyPress += validacion.TBTelefono_KeyPress;
            TBDireccionEmpleado.KeyPress += validacion.TBDireccionEmpleado_KeyPress;
            TBBuscarEmpleado.KeyPress += validacion.TBBuscarEmpleado_KeyPress;

            TBApellidoEmpleado.TextChanged += validacion.TBNombreEmpleado_TextChanged;
            TBNombreEmpleado.TextChanged += validacion.TBNombreEmpleado_TextChanged;
            TBDireccionEmpleado.TextChanged += validacion.TBNombreEmpleado_TextChanged;
            TBBuscarEmpleado.TextChanged += validacion.TBNombreEmpleado_TextChanged;

            TBApellidoEmpleado.TextChanged += validacion.LimpiarEspaciosConsecutivos;
            TBNombreEmpleado.TextChanged += validacion.LimpiarEspaciosConsecutivos;
            TBDireccionEmpleado.TextChanged += validacion.LimpiarEspaciosConsecutivos;
            TBBuscarEmpleado.TextChanged += validacion.LimpiarEspaciosConsecutivos;

            txtNombreProducto.KeyPress += validacion.NombreProducto_KeyPress;
            txtNombreProducto.TextChanged += validacion.TBNombreEmpleado_TextChanged;
            txtNombreProducto.TextChanged += validacion.LimpiarEspaciosConsecutivos;
            txtPrecioProducto.KeyPress += validacion.PrecioProducto_KeyPress;
            txtProvedorProducto.KeyPress += validacion.IDEmpleado_Keypress;
            txtExistencias.KeyPress += validacion.IDEmpleado_Keypress;
            gunaTBStockAñadir.KeyPress += validacion.IDEmpleado_Keypress;

            // Handlers para mostrar la pantalla actual 
            btnCapsula.Click += IndicadorBotonSeleccionado;
            btnLiquido.Click += IndicadorBotonSeleccionado;
            btnPolvo.Click += IndicadorBotonSeleccionado;
            btnInyectable.Click += IndicadorBotonSeleccionado;
            btnNuevoProducto.Click += IndicadorBotonSeleccionado;
            gunaTButtonUsuario.Click += IndicadorBotonSeleccionado;
            gunaTButtonEstadisticas.Click += IndicadorBotonSeleccionado;
            gunaTButtonResumen.Click += IndicadorBotonSeleccionado;

            // Logica botones visibles segun rol
            SetButtonVisibility(role);

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
        
        private void LlenarFlowLayoutPanel()
        {
            dbHelper = new ClsConexion();
            dbHelper.ObtenerProductosFLP(flowLayoutPanel2);
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

        // Método para establecer la visibilidad de los botones basado en el rol para el usuario en bd debe ser 1 y 0 1 administrador y 0 empleado
        private void SetButtonVisibility(string role)
        {
            if (role == "administrador")
            {
                gunaTButtonUsuario.Visible = true;
                gunaTButtonEstadisticas.Visible = true;
                gunaTButtonResumen.Visible = true;
                btnNuevoProducto.Visible = true;
            }
            else
            {
                gunaTButtonUsuario.Visible = false;
                gunaTButtonEstadisticas.Visible = false;
                gunaTButtonResumen.Visible = false;
                btnNuevoProducto.Visible = false;
            }
        }

        private void NuevoProducto_Shown(object sender, EventArgs e)
        {
            // Suscribir al evento OnSelect de cada UserControl1 */
            foreach (Control item in flowLayoutPanel2.Controls)
            {
                if (item is UserControl1 wdg)
                {
                    wdg.OnSelect += UserControl1_OnSelect;
                }
            }
            SuscribirEventosUserControls();
        }

        private void SuscribirEventosUserControls()
        {
            foreach (Control item in flowLayoutPanel2.Controls)
            {
                if (item is UserControl1 wdg && !IsEventHandlerAttached(wdg, "OnSelect"))
                {
                    wdg.OnSelect += UserControl1_OnSelect;
                }
            }
        }

        private bool IsEventHandlerAttached(UserControl1 control, string eventName)
        {
            var eventField = typeof(UserControl1).GetField(eventName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return eventField?.GetValue(control) != null;
        }

        private void UserControl1_OnSelect(object sender, EventArgs e)
        {
            if (sender is UserControl1 userControl)
            {
                // Agregar los datos del UserControl seleccionado al guna2DataGridView2
                guna2DataGridView2.Rows.Add(userControl.Id,userControl.Title, 1, userControl.precio);
                CalcularTotal(); // Actualizar el total después de agregar
            }
        }
        private void Limpiarproductos(params Guna2DataGridView[] dataGrids)
        {
            foreach (var dataGrid in dataGrids)
            {
                dataGrid.Rows.Clear();
            }
            CalcularTotal(); // Actualizar el total después de limpiar
        }
        private void CalcularTotal()
        {
            decimal total = 0;
            int costColumnIndex = 3; // Asegúrate de que este índice es correcto
            foreach (DataGridViewRow row in guna2DataGridView2.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells[costColumnIndex].Value != null && decimal.TryParse(row.Cells[costColumnIndex].Value.ToString(), out decimal cellValue))
                {
                    total += cellValue;
                }
            }
            lblTotal.Text = total.ToString("C"); // Formato de moneda
        }

        private void guna2btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiarproductos(guna2DataGridView2);
        }

        private void clickBoton()
        {
            panelProductos.Show();
            panelProductos.BringToFront();

        }

        // Ocultar todos los páneles
        private void OcultarPaneles()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Guna2Panel && control != PanelBotones && control != guna2Panel2)
                {
                    control.Visible = false;
                }
            }
        }

        // Botones Usuario, Stats y Resumen. 
        // Mostrar panel correspondiente

        private void gunaTButtonUsuario_Click(object sender, EventArgs e)
        {
            ObtenerUltimaID(TBIDEmpleado, "empleado", "Empleado_id");
            OcultarPaneles();
            DataTable dt = dbHelper.FillDataGrid("GetEmpleados");
            gdgvUsuarios.DataSource = dt;
            gunaPanelUsuarioCRUD.Show();
            gunaPanelUsuarioCRUD.BringToFront();
        }

        private void gunaTButtonEstadisticas_Click(object sender, EventArgs e)
        {
            OcultarPaneles();
            FillChartEstadisticas();
            gunaPanelEstadisticas.Show();
            gunaPanelEstadisticas.BringToFront();
        }

        private void gunaTButtonResumen_Click(object sender, EventArgs e)
        {
            OcultarPaneles();
            FillChartResumen();
            gunaPanelResumen.Show();
            gunaPanelResumen.BringToFront();
        }

        // Botones productos, mostrar panel correspondiente
        private void btnCapsula_Click(object sender, EventArgs e)
        {
            OcultarPaneles();
            dbHelper.ObtenerProductosPorCategoriaFLP(flowLayoutPanel2, 1);
            clickBoton();
        }

        private void btnLiquido_Click(object sender, EventArgs e)
        {
            OcultarPaneles();
            dbHelper.ObtenerProductosPorCategoriaFLP(flowLayoutPanel2, 2);
            clickBoton();
        }

        private void btnPolvo_Click(object sender, EventArgs e)
        {
            OcultarPaneles();
            dbHelper.ObtenerProductosPorCategoriaFLP(flowLayoutPanel2, 3);
            clickBoton();
        }

        private void btnInyectable_Click(object sender, EventArgs e)
        {
            OcultarPaneles();
            dbHelper.ObtenerProductosPorCategoriaFLP(flowLayoutPanel2, 4);
            clickBoton();
        }

        private void txtBuscar_TextChanged_1(object sender, EventArgs e)
        {
            // Implementa la lógica de búsqueda cuando el texto cambia
            claseProductos.FiltrarPorTexto(txtBuscar.Text);
        }

        // Función para limpiar textboxes del panel del CRUD de usuarios
        private void LimpiarCamposUsuario(params Guna2TextBox[] textboxes)
        {
            foreach (Guna2TextBox txt in textboxes)
            {
                txt.Clear();
            }
            DataTable dt = dbHelper.FillDataGrid("GetEmpleados");
            gdgvUsuarios.DataSource = dt;
            ObtenerUltimaID(TBIDEmpleado, "empleado", "Empleado_id");
            btnAgregarUsuarios.Enabled = true;
            btnEditarUsuarios.Enabled = false;
            btnEliminarUsuarios.Enabled = false;
            TBEmail.Enabled = true;
        }

        // Botón limpiar campos en panel de CRUD 
        private void btnLimpiarUsuarios_Click(object sender, EventArgs e)
        {
            LimpiarCamposUsuario(TBNombreEmpleado, TBEmail, TBTelefono, TBApellidoEmpleado, TBDireccionEmpleado);
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            OcultarBotonesProducto();
            CargarDGVProductos();
            Limpiarproductos(guna2DataGridView2);
            PanelNuevosProductos.Show();
            PanelNuevosProductos.BringToFront();
            guna2Panel2.BringToFront();
            PanelBotones.BringToFront();
        }

        private void CargarDGVProductos()
        {
            
            OcultarBotonesProducto();
            gunaTBStockAñadir.Text = "";
            txtCodigoProducto.Text = "";
            txtNombreProducto.Text = "";
            txtPrecioProducto.Text = "";
            txtProvedorProducto.Text = "";
            txtExistencias.Text = "";
            gComboCategoriasProducto.SelectedIndex = 0;
            imgProducto.Image = ImagenActual;
            imagePath = "";
            ObtenerUltimaID(txtCodigoProducto, "productos", "Productos_id");
            DataTable dt = dbHelper.FillDataGrid("GetProductos");
            dtgProductos.DataSource = dt;
        }

        private void OcultarBotonesProducto()
        {
            btnAñadir.Enabled = true;
            btnModificar.Enabled = false;
            btnBorrar.Enabled = false;

            lblAñadirStock.Visible = false;
            txtExistencias.Enabled = true;
            gunaTBStockAñadir.Visible = false;
        }

        // Método obtener ultima ID
        private void ObtenerUltimaID(Guna2TextBox textBox, string tabla, string columnaID)
        {
            string nombreTabla = tabla;
            string nombreColumnaid = columnaID;

            int ultimaID = dbHelper.ObtenerUltimaID(nombreTabla, nombreColumnaid);
            int nuevaID = ultimaID + 1;

            if (ultimaID == 0)
            {
                nuevaID = 1;
            }

            textBox.Text = nuevaID.ToString();
        }

        private void dtgProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Asegúrate de que se ha hecho clic en una fila válida
            {
                // Habilita y deshabilita controles para la modificación de un producto
                lblAñadirStock.Visible = true;
                gunaTBStockAñadir.Visible = true;
                txtExistencias.Enabled = false;

                btnAñadir.Enabled = false;
                btnModificar.Enabled = true;
                btnBorrar.Enabled = true;
                DataGridViewRow selectedRow = dtgProductos.Rows[e.RowIndex];
                txtCodigoProducto.Text = selectedRow.Cells["ID"].Value.ToString();
                txtNombreProducto.Text = selectedRow.Cells["Nombre"].Value.ToString();
                txtPrecioProducto.Text = selectedRow.Cells["Precio"].Value.ToString();
                txtExistencias.Text = selectedRow.Cells["Existencias"].Value.ToString();

                // Obtiene ID categoría y lo asigna a combobox
                int categoria = Convert.ToInt32(dbHelper.RecibirDatoProducto(Convert.ToInt32(selectedRow.Cells["ID"].Value), "Categoria_id"));
                gComboCategoriasProducto.SelectedIndex = categoria - 1;

                // Obtiene ID proveedor y lo asigna a textbox
                txtProvedorProducto.Text = dbHelper.RecibirDatoProducto(Convert.ToInt32(selectedRow.Cells["ID"].Value), "Proveedor_id");

                string imagenDB = dbHelper.ObtenerPathImagenProducto(Convert.ToInt32(selectedRow.Cells["ID"].Value));

                if (!string.IsNullOrEmpty(imagenDB))
                {
                    try
                    {
                        imagePath = imagenDB;
                        System.Drawing.Image imagenOriginal = System.Drawing.Image.FromFile(imagenDB);
                        System.Drawing.Image imagenCircular = CrearImgCircular(imagenOriginal, imagenOriginal.Width, imagenOriginal.Height);
                        imgProducto.Image = imagenCircular;
                    }
                    catch
                    {
                        imagePath = "";
                        imgProducto.Image = ImagenActual;
                    }
                }
                else
                {
                    imagePath = "";
                    imgProducto.Image = ImagenActual;
                }
            }
        }

        private List<Producto> ObtenerProductosSeleccionados()
        {
            var productos = new List<Producto>();

            foreach (DataGridViewRow row in guna2DataGridView2.Rows)
            {
                if (row.IsNewRow) continue;

                string codigoProducto = row.Cells[0].Value.ToString();
                int cantidadProducto = Convert.ToInt32(row.Cells[2].Value);
                decimal precioUnitario = Convert.ToDecimal(row.Cells[3].Value);

                // Buscar si el producto ya está en la lista
                var productoExistente = productos.FirstOrDefault(p => p.Codigo == codigoProducto);

                if (productoExistente != null)
                {
                    // Si el producto ya existe, incrementar la cantidad
                    productoExistente.Cantidad += cantidadProducto;
                }
                else
                {
                    // Si el producto no existe, agregar un nuevo producto a la lista
                    var producto = new Producto
                    {
                        Codigo = codigoProducto,
                        Cantidad = cantidadProducto,
                        Descripcion = row.Cells[1].Value.ToString(), // Asegúrate de que esta columna contenga la descripción correcta
                        PrecioUnitario = precioUnitario
                    };
                    productos.Add(producto);
                }
            }

            return productos;
        }

        private void guna2TileButton2_Click(object sender, EventArgs e)
        {
            // Verificar si hay productos en el guna2DataGridView2
            bool hayProductos = guna2DataGridView2.Rows.Cast<DataGridViewRow>().Any(row => !row.IsNewRow && row.Cells.Cast<DataGridViewCell>().Any(cell => cell.Value != null && cell.Value.ToString() != string.Empty));

            if (!hayProductos)
            {
                // Mostrar mensaje de advertencia si no hay productos seleccionados
                MessageBox.Show("Seleccione algún producto antes de pasar a Facturación", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Obtener productos seleccionados y abrir el formulario de facturación
                var productosSeleccionados = ObtenerProductosSeleccionados();

                var facturacionForm = new facturacion(productosSeleccionados);
                facturacionForm.Show();
                this.Hide();
            }
        }

        private string imagePath = "";
        private void imgProducto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos soportados|*.jpg;*.jpeg;*.png";
                ofd.Title = "Selecciona una imagen";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    imagePath = ofd.FileName;
                    System.Drawing.Image imagenOriginal = System.Drawing.Image.FromFile(imagePath);
                    System.Drawing.Image imagenCircular = CrearImgCircular(imagenOriginal, imagenOriginal.Width, imagenOriginal.Height);
                    imgProducto.Image = imagenCircular;
                }
            }
        }

        // Función para aplicarle bordes curvos a la imagen
        private System.Drawing.Image CrearImgCircular(System.Drawing.Image img, int ancho, int alto)
        {
            Bitmap bm = new Bitmap(ancho, alto);
            using (Graphics g = Graphics.FromImage(bm))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Brush br = new TextureBrush(img))
                {
                    g.FillEllipse(br, 0, 0, ancho, alto);
                }
            }
            return bm;
        }

        // Función llenar chart de Resumen
        private void FillChartResumen()
        {
            // Definir variables 
            decimal promedioVentas = 0;
            decimal variacion = 0;
            decimal ventasMes1 = 0;
            decimal ventasMes2 = 0;
            decimal ventasMes3 = 0;

            // Selecciona año actual y mes desde el combobox
            int yearActual = DateTime.Now.Year;

            int mes1 = gunaComboMesResumen.SelectedIndex + 1;
            int mes2 = (mes1 - 1) <= 0 ? 12 : (mes1 - 1);
            int mes3 = (mes1 - 2) <= 0 ? (12 + (mes1 - 2)) : (mes1 - 2);

            // Crea datatable que obtiene datos desde la base de datos
            DataTable dt = dbHelper.ObtenerVentasMeses(yearActual, mes1, mes2, mes3);

            chartTopVentasMensual.Series.Clear();

            Series series1 = new Series("Ventas");
            series1.ChartType = SeriesChartType.Column;
            series1.XValueType = ChartValueType.String;
            series1.IsValueShownAsLabel = true;


            foreach (DataRow dr in dt.Rows)
            {
                string nombreMes = dr["NombreMes"].ToString();
                decimal totalVentas = Convert.ToDecimal(dr["TotalVentas"]);

                if (nombreMes.Equals(CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(mes1)))
                    ventasMes1 = totalVentas;
                else if (nombreMes.Equals(CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(mes2)))
                    ventasMes2 = totalVentas;
                else if (nombreMes.Equals(CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(mes3)))
                    ventasMes3 = totalVentas;

                promedioVentas += totalVentas;
                series1.Points.AddXY(nombreMes, totalVentas);
            }
            chartTopVentasMensual.Series.Add(series1);

            // Opciones de chart, eliminar grid y modificar dinámicamente el axis Y 
            chartTopVentasMensual.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartTopVentasMensual.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chartTopVentasMensual.ChartAreas[0].AxisY.Maximum = Double.NaN;
            chartTopVentasMensual.ChartAreas[0].RecalculateAxesScale();

            // Cálculo de promedio de ventas y variación
            promedioVentas = promedioVentas / 3;
            variacion = ventasMes1 - ventasMes2;

            // Asignación a labels
            lblPromedioVentas.Text = promedioVentas.ToString("F2");
            lblVariacion.Text = variacion.ToString("F2");

            if (variacion > 0) { lblVariacion.ForeColor = Color.Green; }
            else { lblVariacion.ForeColor = Color.Red; }
        }

        private void gunaComboMesResumen_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillChartResumen();
        }
        private void btnAñadir_Click(object sender, EventArgs e)
        {
            // Validar los campos de entrada
            var camposProducto = new Dictionary<string, string>
            {
                { "NombreProducto", txtNombreProducto.Text },
                { "PrecioProducto", txtPrecioProducto.Text },
                { "ProvedorProducto", txtProvedorProducto.Text },
                { "Existencias", txtExistencias.Text }
            };

            Validacion validar = new Validacion(camposProducto);
            string mensajeValidacion = "";

            if (!validar.Validar(out mensajeValidacion))
            {
                MessageBox.Show(mensajeValidacion);
                return;
            }

            // Convertir decimal a double
            double precio = (double)validar.PrecioDecimal;

            // Asigna un valor adecuado a Proveedor_id (1 en este caso, ya que no se usa)
            int proveedorId = 1;

            int categoriaID = Convert.ToInt32(gComboCategoriasProducto.SelectedIndex) + 1; // Ejemplo de cómo obtener el ID de la categoría desde un ComboBox
            string nombreProducto = txtNombreProducto.Text;
            bool estadoProducto = true;

            // Convertir la imagen a bytes
            System.Drawing.Image img = imgProducto.Image;

            dbHelper.InsertarProducto(proveedorId, categoriaID, nombreProducto, validar.PrecioDecimal, validar.Existencias, imagePath, estadoProducto);
            CargarDGVProductos();
        }

        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            if (dtgProductos.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dtgProductos.SelectedRows[0];

                // Validar los campos de entrada
                var camposProducto = new Dictionary<string, string>
            {
                { "NombreProducto", txtNombreProducto.Text },
                { "PrecioProducto", txtPrecioProducto.Text },
                { "ProvedorProducto", txtProvedorProducto.Text },
                { "Existencias", gunaTBStockAñadir.Text },
            };

                Validacion validar = new Validacion(camposProducto);
                string mensajeValidacion = "";

                if (!validar.Validar(out mensajeValidacion))
                {
                    MessageBox.Show(mensajeValidacion);
                    return;
                }

                int proveedorId = 1;
                int totalStock = Convert.ToInt32(txtExistencias.Text) + Convert.ToInt32(gunaTBStockAñadir.Text);

                int productoID = Convert.ToInt32(txtCodigoProducto.Text);
                int categoriaID = Convert.ToInt32(gComboCategoriasProducto.SelectedIndex) + 1;
                string nombreProducto = txtNombreProducto.Text;

                dbHelper.UpdateProducto(productoID, proveedorId, categoriaID, nombreProducto, validar.PrecioDecimal, totalStock, imagePath, true);
                MessageBox.Show("Producto modificado correctamente");

            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para modificar.");
            }
            CargarDGVProductos();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dtgProductos.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dtgProductos.SelectedRows[0];

                // Obtener el nombre del producto de la fila seleccionada
                int productoID = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                dbHelper.EliminarProducto(productoID);
                MessageBox.Show("Producto eliminado correctamente");

                // Recargar el DataGridView
                CargarDGVProductos();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para eliminar.");
            }
        }
        // Función buscar empleado DGV CRUD
        private void TBBuscarEmpleado_TextChanged(object sender, EventArgs e)
        {
            (gdgvUsuarios.DataSource as DataTable).DefaultView.RowFilter = string.Format("Nombre LIKE '%{0}%' OR Apellido LIKE '%{0}%'", TBBuscarEmpleado.Text);
        }

        // Agregar empleado y usuario
        private void btnAgregarUsuarios_Click(object sender, EventArgs e)
        {
            var campos = new Dictionary<string, string> // Diccionario con las textboxes a evaluar
            {
                { "NombreEmpleado", TBNombreEmpleado.Text },
                { "ApellidoEmpleado", TBApellidoEmpleado.Text },
                { "Email", TBEmail.Text },
                { "Telefono", TBTelefono.Text },
                { "DireccionEmpleado", TBDireccionEmpleado.Text } // Nombres de estos campos tienen que ir en los CASE de la clase validación
            };

            Validacion validar = new Validacion(campos);
            string mensajeValidacion = "";

            if (!validar.Validar(out mensajeValidacion))
            {
                MessageBox.Show(mensajeValidacion);
                return;
            }

            int idEmpleado = Convert.ToInt32(TBIDEmpleado.Text);
            string nombreEmpleado = TBNombreEmpleado.Text;
            string apellidoEmpleado = TBApellidoEmpleado.Text;
            string email = TBEmail.Text;
            string direccion = TBDireccionEmpleado.Text;
            string telefono = TBTelefono.Text;
            int rolId = (gRBAdmin.Checked) ? 0 : 1;

            dbHelper.InsertarEmpleadoUsuario(nombreEmpleado, apellidoEmpleado, telefono, email, direccion, rolId, "");
            MessageBox.Show("Empleado agregado correctamente");

            LimpiarCamposUsuario(TBNombreEmpleado, TBEmail, TBTelefono, TBApellidoEmpleado, TBDireccionEmpleado);
        }

        private void gdgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAgregarUsuarios.Enabled = false;
            btnEditarUsuarios.Enabled = true;
            btnEliminarUsuarios.Enabled = true;
            TBEmail.Enabled = false; // Deshabilita que se pueda editar el correo
            DataGridViewRow filaSeleccionada = gdgvUsuarios.Rows[e.RowIndex];
            TBIDEmpleado.Text = filaSeleccionada.Cells["ID"].Value.ToString();
            TBNombreEmpleado.Text = filaSeleccionada.Cells["Nombre"].Value.ToString();
            TBApellidoEmpleado.Text = filaSeleccionada.Cells["Apellido"].Value.ToString();

            string direccion = dbHelper.RecibirDatoUsuario(Convert.ToInt32(filaSeleccionada.Cells["ID"].Value), "Empleado_direccion");
            TBDireccionEmpleado.Text = direccion;
            string correo = dbHelper.RecibirDatoUsuario(Convert.ToInt32(filaSeleccionada.Cells["ID"].Value), "Empleado_email");
            TBEmail.Text = correo;
            string telefono = dbHelper.RecibirDatoUsuario(Convert.ToInt32(filaSeleccionada.Cells["ID"].Value), "Empleado_celular");
            TBTelefono.Text = telefono;

            if (dbHelper.ObtenerRolPorEmail(correo) == 0)
            {
                gRBAdmin.Checked = true;
            }
            else {
                gRBEmpleado.Checked = true;
            }
        }

        private void btnEditarUsuarios_Click(object sender, EventArgs e)
        {
            if (gdgvUsuarios.SelectedRows.Count > 0)
            {
                var campos = new Dictionary<string, string> // Diccionario con las textboxes a evaluar
            {
                { "NombreEmpleado", TBNombreEmpleado.Text },
                { "ApellidoEmpleado", TBApellidoEmpleado.Text },
                { "Telefono", TBTelefono.Text },
                { "DireccionEmpleado", TBDireccionEmpleado.Text } // Nombres de estos campos tienen que ir en los CASE de la clase validación
            };

                Validacion validar = new Validacion(campos);
                string mensajeValidacion = "";

                if (!validar.Validar(out mensajeValidacion))
                {
                    MessageBox.Show(mensajeValidacion);
                    return;
                }

                int idEmpleado = Convert.ToInt32(TBIDEmpleado.Text);
                string nombreEmpleado = TBNombreEmpleado.Text;
                string apellidoEmpleado = TBApellidoEmpleado.Text;
                string email = TBEmail.Text;
                string direccion = TBDireccionEmpleado.Text;
                string telefono = TBTelefono.Text;
                int rolId = (gRBAdmin.Checked) ? 0 : 1;

                dbHelper.ActualizarEmpleadoUsuario(idEmpleado, nombreEmpleado, apellidoEmpleado, telefono, direccion, rolId, email);
                MessageBox.Show("Empleado editado correctamente");
                LimpiarCamposUsuario(TBNombreEmpleado, TBEmail, TBTelefono, TBApellidoEmpleado, TBDireccionEmpleado);
            }
        }

        // Eliminar Empleado Usuario
        private void btnEliminarUsuarios_Click(object sender, EventArgs e)
        {
            int idEmpleado = Convert.ToInt32(TBIDEmpleado.Text);
            string email = TBEmail.Text;
            dbHelper.EliminarEmpleadoUsuario(idEmpleado, email);
            MessageBox.Show("Empleado eliminado correctamente");
            LimpiarCamposUsuario(TBNombreEmpleado, TBEmail, TBTelefono, TBApellidoEmpleado, TBDireccionEmpleado);
        }

        // Función llenar charts panel estadísticas
        private void FillChartEstadisticas()
        {

            // Chart producto más vendido
            DataTable dt = new DataTable();

            int yearActual = DateTime.Now.Year;
            int indexMes = gunaComboMesEstadistica.SelectedIndex + 1;
            dt = dbHelper.ObtenerTopProducto(indexMes, yearActual);

            chartProductosVendidos.Series.Clear();
            Series series1 = new Series("Productos");
            series1.ChartType = SeriesChartType.Column;
            series1.XValueType = ChartValueType.String;
            series1.IsValueShownAsLabel = true;

            int cantidadMasVendida = 0;
            string productoMasVendido = "";
            foreach (DataRow dr in dt.Rows)
            {
                string nombreProducto = dr["NombreProducto"].ToString();
                int cantidadVendidas = Convert.ToInt32(dr["TotalVendido"]);

                series1.Points.AddXY(nombreProducto, cantidadVendidas);

                if (cantidadVendidas > cantidadMasVendida)
                {
                    cantidadMasVendida = cantidadVendidas;
                    productoMasVendido = nombreProducto;
                }
            }

            chartProductosVendidos.Series.Add(series1);
            chartProductosVendidos.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartProductosVendidos.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chartProductosVendidos.ChartAreas[0].AxisY.Maximum = Double.NaN;
            chartProductosVendidos.ChartAreas[0].RecalculateAxesScale();
            lblTopVendido.Text = productoMasVendido;
            lblUnidadesVendidas.Text = cantidadMasVendida.ToString();

            // Chart Empleado con más ventas
            DataTable dt2 = new DataTable();
            dt2 = dbHelper.ObtenerTopEmpleados(indexMes, yearActual);

            chartEmpleadosVentas.Series.Clear();
            Series series2 = new Series("Empleados");
            series2.ChartType = SeriesChartType.Column;
            series2.XValueType = ChartValueType.String;
            series2.IsValueShownAsLabel = true;

            int masFacturasVendidas = 0;
            string empleadoMasVentas = "";
            foreach (DataRow dr in dt2.Rows)
            {
                string nombreEmpleado = dr["NombreCompleto"].ToString();
                int facturasVendidas = Convert.ToInt32(dr["TotalFacturas"]);

                series2.Points.AddXY(nombreEmpleado, facturasVendidas);

                if (facturasVendidas > masFacturasVendidas)
                {
                    masFacturasVendidas = facturasVendidas;
                    empleadoMasVentas = nombreEmpleado;
                }
            }

            chartEmpleadosVentas.Series.Add(series2);
            chartEmpleadosVentas.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartEmpleadosVentas.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chartEmpleadosVentas.ChartAreas[0].AxisY.Maximum = Double.NaN;
            lblEmpleadoVentas.Text = empleadoMasVentas;
            lblCantidadVentas.Text = masFacturasVendidas.ToString();
        }

        private void gunaComboMesEstadistica_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillChartEstadisticas();
        }

        private void IndicadorBotonSeleccionado(object sender, EventArgs e)
        {
            var botonClicado = sender as Guna2Button;

            Color backColorActual = botonClicado.BackColor;
            Color foreColorActual = botonClicado.ForeColor;
            if (botonSeleccionadoG != null)
            {
                botonSeleccionadoG.FillColor = backColorActual;
                botonSeleccionadoG.ForeColor = foreColorActual;
            }

            botonClicado.FillColor = ColorTranslator.FromHtml("#d89c1c");

            botonSeleccionadoG = botonClicado;
            
        }
    }
}