using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using Proyecto_DesarrolloSoftware.Componentes;
using static Mysqlx.Crud.Order.Types;

namespace Proyecto_DesarrolloSoftware
{
    namespace Proyecto_DesarrolloSoftware
    {
        internal class ClsConexion
        {
            private string connectionString;
            //Comentario Prueba

            public ClsConexion()
            {
                // Asigna la cadena de conexión a la variable de instancia
                connectionString = "Server=localhost;Database=solfarma_predefinido;Uid=root;Pwd=[tucontrasena]";
            }
            public string GetConnectionString()
            {
                return connectionString;
            }
            public string GetUserName(string username, string password)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {

                    MySqlCommand cmd = new MySqlCommand("ObtenerUsuario", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_username", username);
                    cmd.Parameters.AddWithValue("@p_password", password);

                    cmd.Parameters.Add(new MySqlParameter("@p_usuarioObtenido", MySqlDbType.VarChar, 100));
                    cmd.Parameters["@p_usuarioObtenido"].Direction = ParameterDirection.Output;

                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        string result = cmd.Parameters["@p_usuarioObtenido"].Value.ToString();
                        if (!string.IsNullOrEmpty(result))
                        {
                            return result;
                        }
                        else
                        {
                            Console.WriteLine("No se encontró ningún usuario con las credenciales proporcionadas.");
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de excepciones
                        Console.WriteLine("Error: " + ex.Message);
                        return null;
                    }
                }
            }
            public string GetUserRole(string username, string password)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {

                    MySqlCommand cmd = new MySqlCommand("ObtenerRolLogin", connection);
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_username", username);
                    cmd.Parameters.AddWithValue("@p_password", password);

                    cmd.Parameters.Add(new MySqlParameter("@p_userRole", MySqlDbType.VarChar, 100));
                    cmd.Parameters["@p_userRole"].Direction = ParameterDirection.Output;

                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        string result = cmd.Parameters["@p_userRole"].Value.ToString();
                        if (!string.IsNullOrEmpty(result))
                        {
                            return result;
                        }
                        else
                        {
                            Console.WriteLine("No se encontró ningún rol para el usuario con las credenciales proporcionadas.");
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de excepciones
                        Console.WriteLine("Error: " + ex.Message);
                        return null;
                    }
                }
            }

            // Método para verificar si un usuario ya existe en la base de datos
            public bool UsuarioExiste(string correo)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("CheckEmailExiste", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_Email", correo);

                    cmd.Parameters.Add(new MySqlParameter("@Total", MySqlDbType.Int32));
                    cmd.Parameters["@Total"].Direction = ParameterDirection.Output;

                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        int count = Convert.ToInt32(cmd.Parameters["@Total"].Value);
                        return count > 0; // Retorna true si existe algún usuario con ese correo
                    }
                    catch (Exception ex)
                    {
                        // Manejo de excepciones
                        Console.WriteLine("Error: " + ex.Message);
                        return false; // En caso de error, retornar false
                    }
                }
            }


            // Método para registrar un usuario en la base de datos
            public bool RegistrarUsuario(string correo, string contrasena)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        MySqlCommand cmd = new MySqlCommand("InsertEmpleadoUsuario", connection);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_Empleado_nombre", "Nombre Empleado");
                        cmd.Parameters.AddWithValue("@p_Empleado_apellido", "Apellido Empleado");
                        cmd.Parameters.AddWithValue("@p_Empleado_celular", "00000000");
                        cmd.Parameters.AddWithValue("@p_Empleado_email", correo);
                        cmd.Parameters.AddWithValue("@p_Empleado_direccion", "Dirección Empleado");
                        cmd.Parameters.AddWithValue("@p_Rol_id", 1);
                        cmd.Parameters.AddWithValue("@p_Usuario_contraseña", contrasena);
                        cmd.Parameters.AddWithValue("@p_Correo_Usuario", correo);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                    catch (Exception ex)
                    {
                        // Manejo de excepciones
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
            }
            // Método para actualizar la contraseña de un usuario
            public bool ActualizarContrasena(string correo, string nuevaContrasena)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("UpdateContrasena", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@nuevaContrasena", nuevaContrasena);

                    try
                    {
                        connection.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                    catch (Exception ex)
                    {
                        // Manejo de excepciones
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
            }



            // Método para obtener la ultima ID de X tabla 
            public int ObtenerUltimaID(string nombreTabla, string nombreColumnaID)
            {
                int ultimaID = 0;

                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("ObtenerUltimaID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombreTabla", nombreTabla);
                    cmd.Parameters.AddWithValue("@nombreColumnaID", nombreColumnaID);
                    MySqlParameter outputParameter = new MySqlParameter("@ultimaID", MySqlDbType.Int32);
                    outputParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParameter);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();

                        var resultado = cmd.Parameters["@ultimaID"].Value;
                        if (resultado != DBNull.Value && resultado != null)
                        {
                            ultimaID = Convert.ToInt32(resultado);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al obtener ID " + ex);
                    }
                }
                return ultimaID;
            }

            // Método para regresar tabla de datos para datagridview
            public DataTable FillDataGrid(string procedimientoAlm, Dictionary<string, object> parameters = null)
            {
                DataTable dt = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(procedimientoAlm, conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                cmd.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }

                        MySqlDataAdapter MyDa = new MySqlDataAdapter(cmd);
                        MyDa.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al obtener los datos: " + ex.Message);
                    }
                }
                return dt;
            }


            // Método obtener ventas últimos 3 meses
            public DataTable ObtenerVentasMeses(int year, int mes1, int mes2, int mes3)
            {
                DataTable dt = new DataTable();
                try
                {
                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("ObtenerVentasMeses", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@pYear", year);
                            cmd.Parameters.AddWithValue("@pMes1", mes1);
                            cmd.Parameters.AddWithValue("@pMes2", mes2);
                            cmd.Parameters.AddWithValue("@pMes3", mes3);

                            MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                            myDA.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener los datos: " + ex.Message);
                }

                return dt;
            }


            // Insertar producto
            public void InsertarProducto(int proveedorID, int categoriaID, string nombreProducto, decimal precioProducto, int existencias, string imagen, bool productoEstado)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("AddProducto", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros del procedimiento almacenado
                        cmd.Parameters.AddWithValue("@ProveedorID", proveedorID);
                        cmd.Parameters.AddWithValue("@CategoriaID", categoriaID);
                        cmd.Parameters.AddWithValue("@NombreProducto", nombreProducto);
                        cmd.Parameters.AddWithValue("@PrecioProducto", precioProducto);
                        cmd.Parameters.AddWithValue("@Existencias", existencias);
                        cmd.Parameters.AddWithValue("@Imagen", imagen);
                        cmd.Parameters.AddWithValue("@ProductoEstado", productoEstado);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al añadir el producto: " + ex.Message);
                }
            }

            // Obtener campos de producto específicos
            public string RecibirDatoProducto(int idProducto, string nombreCampo)
            {
                string dato = "";
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("SelectProductoCampoPorID", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@idProducto", idProducto);
                        cmd.Parameters.AddWithValue("@nombreCampo", nombreCampo);

                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                if (!rdr.IsDBNull(rdr.GetOrdinal("resultado")))
                                {
                                    dato = rdr["resultado"].ToString();
                                }

                                else
                                {
                                    dato = ""; 
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el dato del producto: " + ex.Message);
                }

                return dato;
            }

            public string ObtenerPathImagenProducto(int idProducto)
            {
                string pathImagen = "";
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("SelectImagenProductoPorID", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@idProducto", idProducto);

                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                if (rdr["img"] != DBNull.Value)
                                {
                                    pathImagen = rdr["img"].ToString();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el path de la imagen del producto: " + ex.Message);
                }

                return pathImagen;
            }


            // Método actualizar producto
            public void UpdateProducto(int productoID, int proveedorID, int categoriaID, string nombreProducto, decimal precioProducto, int existencias, string imagen, bool productoEstado)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("UpdateProducto", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parameters for the stored procedure
                        cmd.Parameters.AddWithValue("@ProductoID", productoID);
                        cmd.Parameters.AddWithValue("@ProveedorID", proveedorID);
                        cmd.Parameters.AddWithValue("@CategoriaID", categoriaID);
                        cmd.Parameters.AddWithValue("@NombreProducto", nombreProducto);
                        cmd.Parameters.AddWithValue("@PrecioProducto", precioProducto);
                        cmd.Parameters.AddWithValue("@Existencias", existencias);
                        cmd.Parameters.AddWithValue("@Imagen", imagen);
                        cmd.Parameters.AddWithValue("@ProductoEstado", productoEstado);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating product: " + ex.Message);
                }
            }

            // Método borrar producto
            public void EliminarProducto(int productoID)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("DeleteProducto", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ProductoID", productoID);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el producto: " + ex.Message);
                }
            }

            // Insertar EmpleadoUsuario
            public void InsertarEmpleadoUsuario(string nombre, string apellido, string telefono, string email, string direccion, int rolID, string contrasena)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("InsertEmpleadoUsuario", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_Empleado_nombre", nombre);
                        cmd.Parameters.AddWithValue("@p_Empleado_apellido", apellido);
                        cmd.Parameters.AddWithValue("@p_Empleado_celular", telefono);
                        cmd.Parameters.AddWithValue("@p_Empleado_email", email);
                        cmd.Parameters.AddWithValue("@p_Empleado_direccion", direccion);
                        cmd.Parameters.AddWithValue("@p_Rol_id", rolID);
                        cmd.Parameters.AddWithValue("@p_Usuario_contraseña", contrasena);
                        cmd.Parameters.AddWithValue("@p_Correo_Usuario", email);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al añadir el empleado: " + ex.Message);
                }
            }


            // Obtener campo específico Empleado/Usuario
            public string RecibirDatoUsuario(int idEmpleado, string nombreCampo)
            {
                string dato = "";
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("SelectEmpleadoCampoPorID", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                        cmd.Parameters.AddWithValue("@nombreCampo", nombreCampo);

                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                if (!rdr.IsDBNull(rdr.GetOrdinal("resultado")))
                                {
                                    dato = rdr["resultado"].ToString();
                                }

                                else
                                {
                                    dato = "";
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el dato del producto: " + ex.Message);
                }

                return dato;
            }

            public int ObtenerRolPorEmail(string email)
            {
                int retornoID = 0;
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("RolPorEmail", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@correo", email);

                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                if (!rdr.IsDBNull(rdr.GetOrdinal("Rol_id")))
                                {
                                    retornoID = rdr.GetInt32(rdr.GetOrdinal("Rol_id"));
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el rol: " + ex.Message);
                }

                return retornoID;
            }

            // Actualizar empleado usuario
            public void ActualizarEmpleadoUsuario(int empleadoID, string nombre, string apellido, string telefono, string direccion, int rolID, string correoUsuario)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("UpdateEmpleadoUsuario", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_Empleado_id", empleadoID);
                        cmd.Parameters.AddWithValue("@p_Empleado_nombre", nombre);
                        cmd.Parameters.AddWithValue("@p_Empleado_apellido", apellido);
                        cmd.Parameters.AddWithValue("@p_Empleado_celular", telefono);
                        cmd.Parameters.AddWithValue("@p_Empleado_direccion", direccion);
                        cmd.Parameters.AddWithValue("@p_Rol_id", rolID);
                        cmd.Parameters.AddWithValue("@p_Usuario_contraseña", "");
                        cmd.Parameters.AddWithValue("@p_Correo_Usuario", correoUsuario);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el empleado: " + ex.Message);
                }
            }

            // Eliminar Empleado Usuario
            public void EliminarEmpleadoUsuario(int empleadoID, string email)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("DeleteEmpleadoUsuario", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_Empleado_id", empleadoID);
                        cmd.Parameters.AddWithValue("@p_Correo_Usuario", email);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el producto: " + ex.Message);
                }
            }

            public DataTable ObtenerTopProducto(int mes, int año)
            {
                DataTable dt = new DataTable();

                try
                {
                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("ObtenerTopProductosVendidos", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@mes", mes);
                            cmd.Parameters.AddWithValue("@anio", año);

                            MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                            myDA.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener los datos: " + ex.Message);
                }

                return dt;
            }

            public DataTable ObtenerTopEmpleados(int mes, int año)
            {
                DataTable dt = new DataTable();

                try
                {
                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("ObtenerTopEmpleadosFacturas", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@mes", mes);
                            cmd.Parameters.AddWithValue("@anio", año);

                            MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                            myDA.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener los datos: " + ex.Message);
                }

                return dt;
            }

            // Obtener ids empleados 
            public List<int> ObtenerIDsEmpleados()
            {
                List<int> idsEmpleados = new List<int>();
                try
                {
                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();
                        using (MySqlCommand cmd = new MySqlCommand("GetEmpleados", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    idsEmpleados.Add(reader.GetInt32("ID"));
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener los datos: " + ex.Message);
                }

                return idsEmpleados;
            }

            // LLenar productos en flowpanel 
            public void ObtenerProductosFLP(FlowLayoutPanel Contenedor)
            {
                int id_producto;
                string nombreProducto;
                decimal precio;
                string imagen;
                try
                {
                    Contenedor.Controls.Clear();
                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();
                        using (MySqlCommand cmd = new MySqlCommand("ProductosUserControl", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            MySqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                id_producto = Convert.ToInt32(reader[0]);
                                nombreProducto = reader[3].ToString();
                                precio = Convert.ToDecimal(reader[4]);
                                imagen = reader[6].ToString();

                                UserControl1 producto = new UserControl1();
                                producto.Id = id_producto;
                                producto.Nombre = nombreProducto;
                                producto.precio = precio.ToString();

                                try
                                {
                                    // Manejar la imagen según el formato almacenado
                                    if (File.Exists(imagen))
                                    {
                                        // Si la imagen es una ruta de archivo
                                        producto.imagen = System.Drawing.Image.FromFile(imagen);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error al procesar la imagen: " + ex.Message);
                                }

                                Contenedor.Controls.Add(producto);
                            }
                            reader.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }


            // Llenar formulario específico por categoría 
            public void ObtenerProductosPorCategoriaFLP(FlowLayoutPanel Contenedor, int categoriaID)
            {
                int id_producto;
                string nombreProducto;
                decimal precio;
                string imagen;
                try
                {
                    Contenedor.Controls.Clear();
                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();
                        using (MySqlCommand cmd = new MySqlCommand("ProductosPorCategoriaUserControl", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@catID", categoriaID);
                            MySqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                id_producto = Convert.ToInt32(reader[0]);
                                nombreProducto = reader[3].ToString();
                                precio = Convert.ToDecimal(reader[4]);
                                imagen = reader[6].ToString();

                                UserControl1 producto = new UserControl1();
                                producto.Id = id_producto;
                                producto.Nombre = nombreProducto;
                                producto.precio = precio.ToString();

                                try
                                {
                                    // Manejar la imagen según el formato almacenado
                                    if (File.Exists(imagen))
                                    {
                                        // Si la imagen es una ruta de archivo
                                        producto.imagen = System.Drawing.Image.FromFile(imagen);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error al procesar la imagen: " + ex.Message);
                                }

                                Contenedor.Controls.Add(producto);
                            }
                            reader.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            // Método insertar cliente
            public int InsertarCliente(string nombreCliente, string apellidoCliente, string direccionCliente, string telefonoCliente, string identidadCliente)
            {
                int idCliente = 0;
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("InsertarCliente", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@NombreCliente", nombreCliente);
                        cmd.Parameters.AddWithValue("@ApellidoCliente", apellidoCliente);
                        cmd.Parameters.AddWithValue("@DireccionCliente", direccionCliente);
                        cmd.Parameters.AddWithValue("@telefonoCliente", telefonoCliente);
                        cmd.Parameters.AddWithValue("@IdentidadCliente", identidadCliente);



                        MySqlParameter outputParameter = new MySqlParameter("@ClienteId", MySqlDbType.Int32);
                        outputParameter.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParameter);

                        cmd.ExecuteNonQuery();
                        idCliente = (int)outputParameter.Value;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al añadir el cliente: " + ex.Message);
                }
                return idCliente;
            }

            // Insertar factura 
            public int InsertarFactura(int idCliente, int idEmpleado, decimal totalFactura, string facturaFecha)
            {
                int facturaID = 0;
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("InsertarFactura", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ClienteID", idCliente);
                        cmd.Parameters.AddWithValue("@EmpleadoID", idEmpleado);
                        cmd.Parameters.AddWithValue("@FacturaTotal", totalFactura);
                        cmd.Parameters.AddWithValue("@FacturaFecha", facturaFecha);

                        MySqlParameter outputParameter = new MySqlParameter("@FacturaID", MySqlDbType.Int32);
                        outputParameter.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParameter);

                        cmd.ExecuteNonQuery();
                        facturaID = (int)outputParameter.Value;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al añadir el cliente: " + ex.Message);
                }
                return facturaID;
            }

            // Insertar factura detalle
            public void InsertarFacturaDetalle(int facturaId, int productosId, int cantidadProductos)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("InsertarFacturaDetalle", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_Factura_id", facturaId);
                        cmd.Parameters.AddWithValue("@p_Productos_id", productosId);
                        cmd.Parameters.AddWithValue("@p_Cantidad_productos", cantidadProductos);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al añadir el detalle de la factura: " + ex.Message);
                }
            }

            // Llenar reporte
            public DataTable DetalleUltimaFactura()
            {
                DataTable dt = new DataTable();
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("ObtenerUltimaFactura", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (MySqlDataAdapter da = new MySqlDataAdapter("ObtenerUltimaFactura", conn))
                        {
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el detalle de la última factura" + ex.Message);
                }
                return dt;
            }

            // Recibir dato por correo
            public string RecibirDatoPorCorreo(string correoEmpleado, string nombreCampo)
            {
                string dato = "";
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("SelectEmpleadoCampoPorCorreo", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@correoEmpleado", correoEmpleado);
                        cmd.Parameters.AddWithValue("@nombreCampo", nombreCampo);

                        object resultado = cmd.ExecuteScalar();
                        if (resultado != DBNull.Value && resultado != null)
                        {
                            dato = resultado.ToString();
                        }
                        else
                        {
                            dato = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el empleado: " + ex.Message);
                }

                return dato;
            }

        }
    }
}