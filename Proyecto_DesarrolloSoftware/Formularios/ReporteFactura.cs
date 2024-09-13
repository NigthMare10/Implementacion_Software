using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using MySql;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Reporting.WinForms;
using Proyecto_DesarrolloSoftware.Proyecto_DesarrolloSoftware;
using static Proyecto_DesarrolloSoftware.Form1;




namespace Proyecto_DesarrolloSoftware
{
    public partial class ReporteFactura : Form
    {
        public ReporteFactura()
        {
            InitializeComponent();
            ClsConexion dbHelper = new ClsConexion();
            DataTable dt = dbHelper.DetalleUltimaFactura();

            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rp = new ReportDataSource("DataSet1", dt);
            reportViewer1.LocalReport.DataSources.Add(rp);
            reportViewer1.RefreshReport();
            string userRole = GlobalData.UserRole;
        }

        private void ReporteFactura_Load(object sender, EventArgs e)
        {

           
        }

        private void btncargarreporte_Click(object sender, EventArgs e)
        {

           

        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
            NuevoProducto nuevoProducto = new NuevoProducto(GlobalData.UserRole);
            nuevoProducto.Show();
        }
    }
}
