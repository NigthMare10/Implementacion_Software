using System;
using System.Data;
using MySql.Data.MySqlClient; // Asegúrate de tener la referencia a MySql.Data
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Proyecto_DesarrolloSoftware.Proyecto_DesarrolloSoftware;

public class EnviarEmail
{
    private string myEmail = "solfarma29@gmail.com";
    private string myPassword = "pngf rdqk svrb zsay"; // No cambiar la contraseña Porque es cifrada
    private string myAlias = "SolFarma";
    private string toEmail = "solfarma29@gmail.com";
    private string smtpHost = "smtp.gmail.com";
    private int smtpPort = 587;

    private ClsConexion conexion;

    public EnviarEmail()
    {
        conexion = new ClsConexion();
    }

    public void CheckAndSendEmail()
    {
        string connectionString = conexion.GetConnectionString();
        string query = "CALL GetLowStockProducts1()";

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            MySqlCommand cmd = new MySqlCommand(query, conn);

            try
            {
                conn.Open();
                Console.WriteLine("Conexión a la base de datos exitosa.");

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        string messageBody = "Los siguientes productos tienen existencias bajas:\n\n";

                        while (reader.Read())
                        {
                            string productName = reader["Nombre_producto"].ToString();
                            int stock = Convert.ToInt32(reader["Productos_exitencias"]);
                            messageBody += $"El producto {productName} está con una cantidad muy baja en el almacén con una cantidad de {stock} unidades.\n";
                        }

                        SendEmail(messageBody);
                    }
                    else
                    {
                        Console.WriteLine("No hay productos con existencias bajas.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se ha producido un error al conectar a la base de datos: " + ex.Message);
            }
        }
    }

    private void SendEmail(string messageBody)
    {
        try
        {
            MailMessage mail = new MailMessage
            {
                From = new MailAddress(myEmail, myAlias, System.Text.Encoding.UTF8),
                Subject = "Alerta de existencias bajas",
                Body = messageBody,
                IsBodyHtml = true,
                Priority = MailPriority.High
            };
            mail.To.Add(toEmail);

            SmtpClient smtp = new SmtpClient
            {
                UseDefaultCredentials = false,
                Port = smtpPort,
                Host = smtpHost,
                Credentials = new NetworkCredential(myEmail, myPassword),
                EnableSsl = true
            };
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };

            Console.WriteLine("Enviando correo...");
            smtp.Send(mail);
            Console.WriteLine("Correo enviado.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al enviar correo: " + e.Message);
        }
    }
}
