using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;

namespace Proyecto_DesarrolloSoftware.Componentes
{
    public enum categories { Capsula, Liquido, Polvo, Inyectable }
    public partial class UserControl1 : UserControl
    {

        private categories _category;
        private double _cost;
        public event EventHandler OnSelect;
        private int id = 0;

        public UserControl1()
        {
            InitializeComponent();
            lblTitle = labelNombreProducto;
            this.Click += new EventHandler(gunaImagen_Click); // Agregar manejador de clic para el UserControl
        }

        // Propiedades y métodos adicionales aquí...

        public Label lblTitle { get; set; }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            // Código para el evento Paint del panel, si es necesario
        }

        private void gunaImagen_Click(object sender, EventArgs e)
        {
            OnSelect?.Invoke(this, EventArgs.Empty);
        }

        // Método protegido para invocar el evento
        protected virtual void OnSelectClicked(EventArgs e)
        {
            OnSelect?.Invoke(this, e);
        }

        // Propiedades adicionales aquí...

        public categories Category { get => _category; set => _category = value; }
        public string Title { get => labelNombreProducto.Text; set => labelNombreProducto.Text = value; }
        public double Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                labelPrecioProducto.Text = precio.ToString(); // Formato de moneda
            }
        }
        public Image Icon { get => gunaImagen.Image; set => gunaImagen.Image = value; }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        public string Nombre
        {
            get { return labelNombreProducto.Text; }
            set { labelNombreProducto.Text = value; }
        }

        public string descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public Image imagen
        {
            get { return gunaImagen.Image; }
            set { gunaImagen.Image = value; }
        }

        public string precio
        {
            get { return labelPrecioProducto.Text; }
            set
            {
                labelPrecioProducto.Text = value;
            }
        }

    }
}
