using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

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
                //connectionString = "Server=localhost;Database=solfarma_predefinido;Uid=root;Pwd=042002;";
                connectionString = "Server=localhost;Database=solfarma_predefinido;Uid=root;Pwd=test;";
            }

            public string GetUserName(string username, string password)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT Correo_Usuario FROM solfarma_predefinido.usuario WHERE Correo_Usuario = @username AND Usuario_contraseña = @password";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    try
                    {
                        connection.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            return result.ToString();
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
                    string query = "SELECT r.Rol_nombre FROM solfarma_predefinido.usuario u INNER JOIN solfarma_predefinido.roles r ON u.Rol_id = r.Rol_id WHERE u.Correo_Usuario = @username AND u.Usuario_contraseña = @password";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    try
                    {
                        connection.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            return result.ToString();
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
                    string query = "SELECT COUNT(*) FROM solfarma_predefinido.usuario WHERE Correo_Usuario = @correo";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@correo", correo);

                    try
                    {
                        connection.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        // Manejo de excepciones
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
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

                        // Obtener el último ID de usuario y sumar 1
                        string queryUltimoId = "SELECT MAX(Usuario_id) FROM solfarma_predefinido.usuario";
                        MySqlCommand cmdUltimoId = new MySqlCommand(queryUltimoId, connection);
                        int ultimoId = Convert.ToInt32(cmdUltimoId.ExecuteScalar());
                        int nuevoId = ultimoId + 1;

                        // Insertar el nuevo usuario
                        string query = "INSERT INTO solfarma_predefinido.usuario (Usuario_id, Rol_id, Correo_Usuario, Usuario_contraseña) VALUES (@usuarioId, 0, @correo, @contrasena)";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@usuarioId", nuevoId);
                        cmd.Parameters.AddWithValue("@correo", correo);
                        cmd.Parameters.AddWithValue("@contrasena", contrasena);

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
                    string query = "UPDATE solfarma_predefinido.usuario SET Usuario_contraseña = @nuevaContrasena WHERE Correo_Usuario = @correo";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
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

                //Aun no implementado, se reemplaza método para insertar el producto completo
                public void GuardarImagen(string data)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string queryInsertarImagen = "INSERT INTO productos (Imagen) VALUES (@Image)";
                        using (MySqlCommand cmd = new MySqlCommand(queryInsertarImagen, connection))
                        {
                            cmd.Parameters.Add("@Image", MySqlDbType.VarBinary).Value = data;
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Imagen agregada exitosamente"); // Eliminar este mensaje después

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error insertando imagen: " + ex.Message);
                    }
                }
            }

            // Método para obtener la ultima ID de X tabla 
            public int ObtenerUltimaID(string nombreTabla, string nombreColumnaID)
            {
                int ultimaID = 0;

                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    string query = $"SELECT MAX({nombreColumnaID}) FROM {nombreTabla}";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    try
                    {
                        con.Open();
                        var resultado = cmd.ExecuteScalar();
                        if (resultado != DBNull.Value && resultado != null)
                        {
                            ultimaID = Convert.ToInt32(resultado);
                        }
                    }
                    catch (Exception ex){
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


            //****************** NO TOCAR ***************
            //****************** NO TOCAR ***************
            //****************** NO TOCAR ***************
            //****************** NO TOCAR ***************
            //****************** NO TOCAR ***************

            // Metodo para insertar un producto en la base de datos (Implementado a medias ) ****************** NO TOCAR ***************
            public bool GuardarProducto(Producto producto)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                    try
                    {
                        connection.Open();
                        string query = "INSERT INTO Productos (Proveedor_id, Nombre_producto, Productos_precios, Productos_exitencias, Imagen, Productos_estado) VALUES (@Proveedor_id, @Nombre_producto, @Productos_precios, @Productos_exitencias, @Imagen, @Productos_estado)";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@Proveedor_id", producto.Proveedor_id); // Ajustar según tu modelo de datos
                        cmd.Parameters.AddWithValue("@Nombre_producto", producto.Nombre);
                        cmd.Parameters.AddWithValue("@Productos_precios", producto.Precio);
                        cmd.Parameters.AddWithValue("@Productos_exitencias", producto.Existencias);
                        cmd.Parameters.AddWithValue("@Imagen", producto.Imagen);
                        cmd.Parameters.AddWithValue("@Productos_estado", true);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Manejar la excepción según sea necesario
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                    }
            }
        }
    }
}