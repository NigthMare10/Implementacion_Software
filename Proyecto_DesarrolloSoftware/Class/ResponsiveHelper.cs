using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto_DesarrolloSoftware
{
    public class ResponsiveHelper
    {
        private List<Control> allControls = new List<Control>();
        private Form form;
        private Size originalFormSize;

        public ResponsiveHelper(Form form)
        {
            this.form = form;
            originalFormSize = form.ClientSize;
            InitializeControls(form.Controls);
        }

        private void InitializeControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                allControls.Add(control);
                control.SetOriginalSizeAndLocation();
                if (control.HasChildren)
                {
                    InitializeControls(control.Controls);
                }
            }
        }

        public void AdjustControls()
        {
            foreach (Control control in allControls)
            {
                AdjustControl(control);
            }
        }

        private void AdjustControl(Control control)
        {
            // Ajustar el tamaño del control según el tamaño del formulario
            float formWidthFactor = (float)form.ClientSize.Width / originalFormSize.Width;
            float formHeightFactor = (float)form.ClientSize.Height / originalFormSize.Height;

            int newWidth = (int)(control.OriginalSize().Width * formWidthFactor);
            int newHeight = (int)(control.OriginalSize().Height * formHeightFactor);
            control.Size = new Size(newWidth, newHeight);

            // Ajustar la posición del control según el tamaño del formulario
            int newX = (int)(control.OriginalLocation().X * formWidthFactor);
            int newY = (int)(control.OriginalLocation().Y * formHeightFactor);
            control.Location = new Point(newX, newY);
        }

        public void EnterFullScreen()
        {
            originalFormSize = form.ClientSize;
            form.WindowState = FormWindowState.Maximized;
            AdjustControls();
        }

        public void ExitFullScreen()
        {
            form.WindowState = FormWindowState.Normal;
            form.ClientSize = originalFormSize;
            AdjustControls();
        }
    }

    public static class ControlExtensions
    {
        private static readonly Dictionary<Control, Size> originalSizes = new Dictionary<Control, Size>();
        private static readonly Dictionary<Control, Point> originalLocations = new Dictionary<Control, Point>();

        public static void SetOriginalSizeAndLocation(this Control control)
        {
            originalSizes[control] = control.Size;
            originalLocations[control] = control.Location;
        }

        public static Size OriginalSize(this Control control)
        {
            return originalSizes.ContainsKey(control) ? originalSizes[control] : control.Size;
        }

        public static Point OriginalLocation(this Control control)
        {
            return originalLocations.ContainsKey(control) ? originalLocations[control] : control.Location;
        }
    }
}
