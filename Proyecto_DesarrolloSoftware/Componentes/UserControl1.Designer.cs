namespace Proyecto_DesarrolloSoftware.Componentes
{
    partial class UserControl1
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl1));
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.gunaImagen = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.labelPrecioProducto = new System.Windows.Forms.Label();
            this.labelNombreProducto = new System.Windows.Forms.Label();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gunaImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.Controls.Add(this.gunaImagen);
            this.guna2Panel1.Controls.Add(this.labelPrecioProducto);
            this.guna2Panel1.Controls.Add(this.labelNombreProducto);
            this.guna2Panel1.Location = new System.Drawing.Point(3, 2);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(172, 85);
            this.guna2Panel1.TabIndex = 0;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // gunaImagen
            // 
            this.gunaImagen.BackColor = System.Drawing.Color.Transparent;
            this.gunaImagen.Image = ((System.Drawing.Image)(resources.GetObject("gunaImagen.Image")));
            this.gunaImagen.ImageRotate = 0F;
            this.gunaImagen.Location = new System.Drawing.Point(94, 18);
            this.gunaImagen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gunaImagen.Name = "gunaImagen";
            this.gunaImagen.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.gunaImagen.Size = new System.Drawing.Size(76, 57);
            this.gunaImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.gunaImagen.TabIndex = 4;
            this.gunaImagen.TabStop = false;
            this.gunaImagen.UseTransparentBackground = true;
            this.gunaImagen.Click += new System.EventHandler(this.gunaImagen_Click);
            // 
            // labelPrecioProducto
            // 
            this.labelPrecioProducto.AutoSize = true;
            this.labelPrecioProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelPrecioProducto.Font = new System.Drawing.Font("Segoe UI Symbol", 10F);
            this.labelPrecioProducto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(133)))), ((int)(((byte)(2)))));
            this.labelPrecioProducto.Location = new System.Drawing.Point(3, 46);
            this.labelPrecioProducto.Name = "labelPrecioProducto";
            this.labelPrecioProducto.Size = new System.Drawing.Size(59, 23);
            this.labelPrecioProducto.TabIndex = 2;
            this.labelPrecioProducto.Text = " L 9.00";
            // 
            // labelNombreProducto
            // 
            this.labelNombreProducto.AutoSize = true;
            this.labelNombreProducto.Font = new System.Drawing.Font("Segoe UI Symbol", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNombreProducto.Location = new System.Drawing.Point(3, 7);
            this.labelNombreProducto.Name = "labelNombreProducto";
            this.labelNombreProducto.Size = new System.Drawing.Size(92, 19);
            this.labelNombreProducto.TabIndex = 1;
            this.labelNombreProducto.Text = "Panadol Ultra";
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.guna2Panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(179, 89);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gunaImagen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        public System.Windows.Forms.Label labelPrecioProducto;
        public System.Windows.Forms.Label labelNombreProducto;
        public Guna.UI2.WinForms.Guna2CirclePictureBox gunaImagen;
    }
}
