using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Proyecto_DesarrolloSoftware.Componentes;
using System.Windows.Forms;

namespace Proyecto_DesarrolloSoftware
{
    
    public class ClaseProductos
    {
        private FlowLayoutPanel _flowLayoutPanel2;

        public ClaseProductos(FlowLayoutPanel flowLayoutPanel2)
        {
            _flowLayoutPanel2 = flowLayoutPanel2;
        }
        public void AddItem(string name, double cost, categories category, string imagePath)
        {
            // Forma la ruta completa de la imagen
            string fullImagePath = Path.Combine(Application.StartupPath, "Icon", imagePath);

            // Verifica si la imagen existe en la ruta especificada
            if (File.Exists(fullImagePath))
            {
                // Carga la imagen desde la ruta
                Image icon = Image.FromFile(fullImagePath);

                // Agrega el control personalizado al flowLayoutPanel
                _flowLayoutPanel2.Controls.Add(new UserControl1()
                {
                    Title = name,
                    Cost = cost, // Pasa el valor como double
                    Category = category,
                    Icon = icon,
                    Tag = category
                });
            }
            else
            {
                MessageBox.Show("No se pudo encontrar la imagen: " + imagePath);
            }
        }

        public void RemoveItem(UserControl1 userControl)
        {
            _flowLayoutPanel2.Controls.Remove(userControl);
        }

        public UserControl1 FindControlByTag(categories category, int index)
        {
            int currentIndex = 0;

            foreach (Control item in _flowLayoutPanel2.Controls)
            {
                if (item is UserControl1 wdg && wdg.Tag.Equals(category))
                {
                    if (currentIndex == index)
                    {
                        return wdg;
                    }
                    currentIndex++;
                }
            }
            return null;
        }


        public void FiltrarPorTexto(string texto)
        {
            foreach (Control item in _flowLayoutPanel2.Controls)
            {
                if (item is UserControl1 wdg)
                {
                    // Filtra por el texto
                    wdg.Visible = wdg.lblTitle.Text.ToLower().Contains(texto.Trim().ToLower());
                }
            }
        }
    }
    public class Producto
    {
        public string Codigo { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal => Cantidad * PrecioUnitario;


        //para otro usoooo

        public int Proveedor_id { get; set; } = 0; // Valor predeterminado si no se usa
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Existencias { get; set; }
        public byte[] Imagen { get; set; }
    }
}



