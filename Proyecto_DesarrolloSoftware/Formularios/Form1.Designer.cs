namespace Proyecto_DesarrolloSoftware
{
    partial class Form1
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnSalir = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.btnRegistroLogin = new Guna.UI2.WinForms.Guna2TileButton();
            this.label3 = new System.Windows.Forms.Label();
            this.guna2PictureBox2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnVerContraseñaLogin = new Guna.UI2.WinForms.Guna2ImageCheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRecuperar = new Guna.UI2.WinForms.Guna2TileButton();
            this.btnIniciar = new Guna.UI2.WinForms.Guna2TileButton();
            this.txtContraseña = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtCorreo = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.BtnFullScreen = new Guna.UI2.WinForms.Guna2ImageButton();
            this.BtnExitFullScreen = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(187)))), ((int)(((byte)(33)))));
            this.guna2Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.guna2Panel1.BorderRadius = 10;
            this.guna2Panel1.Controls.Add(this.BtnExitFullScreen);
            this.guna2Panel1.Controls.Add(this.BtnFullScreen);
            this.guna2Panel1.Controls.Add(this.btnSalir);
            this.guna2Panel1.Controls.Add(this.guna2PictureBox1);
            this.guna2Panel1.Controls.Add(this.guna2ShadowPanel1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1083, 574);
            this.guna2Panel1.TabIndex = 0;
            // 
            // btnSalir
            // 
            this.btnSalir.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnSalir.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image4")));
            this.btnSalir.HoverState.ImageSize = new System.Drawing.Size(28, 28);
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnSalir.ImageRotate = 0F;
            this.btnSalir.ImageSize = new System.Drawing.Size(25, 25);
            this.btnSalir.Location = new System.Drawing.Point(1015, 2);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.PressedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image5")));
            this.btnSalir.PressedState.ImageSize = new System.Drawing.Size(23, 23);
            this.btnSalir.Size = new System.Drawing.Size(64, 54);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.Image")));
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(571, 73);
            this.guna2PictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(637, 501);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 1;
            this.guna2PictureBox1.TabStop = false;
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.btnRegistroLogin);
            this.guna2ShadowPanel1.Controls.Add(this.label3);
            this.guna2ShadowPanel1.Controls.Add(this.guna2PictureBox2);
            this.guna2ShadowPanel1.Controls.Add(this.btnVerContraseñaLogin);
            this.guna2ShadowPanel1.Controls.Add(this.label2);
            this.guna2ShadowPanel1.Controls.Add(this.label1);
            this.guna2ShadowPanel1.Controls.Add(this.btnRecuperar);
            this.guna2ShadowPanel1.Controls.Add(this.btnIniciar);
            this.guna2ShadowPanel1.Controls.Add(this.txtContraseña);
            this.guna2ShadowPanel1.Controls.Add(this.txtCorreo);
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(133)))), ((int)(((byte)(2)))));
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(-5, -7);
            this.guna2ShadowPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(459, 588);
            this.guna2ShadowPanel1.TabIndex = 0;
           // this.guna2ShadowPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2ShadowPanel1_Paint);
            // 
            // btnRegistroLogin
            // 
            this.btnRegistroLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnRegistroLogin.BorderRadius = 10;
            this.btnRegistroLogin.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRegistroLogin.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRegistroLogin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRegistroLogin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRegistroLogin.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(187)))), ((int)(((byte)(33)))));
            this.btnRegistroLogin.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistroLogin.ForeColor = System.Drawing.Color.White;
            this.btnRegistroLogin.Location = new System.Drawing.Point(63, 524);
            this.btnRegistroLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRegistroLogin.Name = "btnRegistroLogin";
            this.btnRegistroLogin.ShadowDecoration.BorderRadius = 20;
            this.btnRegistroLogin.ShadowDecoration.Depth = 20;
            this.btnRegistroLogin.ShadowDecoration.Enabled = true;
            this.btnRegistroLogin.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(1, 0, 1, 8);
            this.btnRegistroLogin.Size = new System.Drawing.Size(333, 46);
            this.btnRegistroLogin.TabIndex = 10;
            this.btnRegistroLogin.Text = "Registro";
            this.btnRegistroLogin.Click += new System.EventHandler(this.btnRegistroLogin_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(179, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 38);
            this.label3.TabIndex = 8;
            this.label3.Text = "Sol Farma";
            // 
            // guna2PictureBox2
            // 
            this.guna2PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox2.Image")));
            this.guna2PictureBox2.ImageRotate = 0F;
            this.guna2PictureBox2.Location = new System.Drawing.Point(112, 50);
            this.guna2PictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2PictureBox2.Name = "guna2PictureBox2";
            this.guna2PictureBox2.Size = new System.Drawing.Size(79, 58);
            this.guna2PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox2.TabIndex = 9;
            this.guna2PictureBox2.TabStop = false;
            // 
            // btnVerContraseñaLogin
            // 
            this.btnVerContraseñaLogin.CheckedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image6")));
            this.btnVerContraseñaLogin.CheckedState.ImageSize = new System.Drawing.Size(28, 28);
            this.btnVerContraseñaLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerContraseñaLogin.HoverState.ImageSize = new System.Drawing.Size(28, 28);
            this.btnVerContraseñaLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnVerContraseñaLogin.Image")));
            this.btnVerContraseñaLogin.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnVerContraseñaLogin.ImageRotate = 0F;
            this.btnVerContraseñaLogin.ImageSize = new System.Drawing.Size(28, 28);
            this.btnVerContraseñaLogin.Location = new System.Drawing.Point(340, 316);
            this.btnVerContraseñaLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnVerContraseñaLogin.Name = "btnVerContraseñaLogin";
            this.btnVerContraseñaLogin.Size = new System.Drawing.Size(47, 34);
            this.btnVerContraseñaLogin.TabIndex = 3;
            this.btnVerContraseñaLogin.UseTransparentBackground = true;
            this.btnVerContraseñaLogin.CheckedChanged += new System.EventHandler(this.btnVerContraseñaLogin_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(165, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 29);
            this.label2.TabIndex = 7;
            this.label2.Text = "Contraseña";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(184, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 29);
            this.label1.TabIndex = 6;
            this.label1.Text = "Correo";
            // 
            // btnRecuperar
            // 
            this.btnRecuperar.BackColor = System.Drawing.Color.Transparent;
            this.btnRecuperar.BorderRadius = 10;
            this.btnRecuperar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRecuperar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRecuperar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRecuperar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRecuperar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(187)))), ((int)(((byte)(33)))));
            this.btnRecuperar.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecuperar.ForeColor = System.Drawing.Color.White;
            this.btnRecuperar.Location = new System.Drawing.Point(63, 462);
            this.btnRecuperar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRecuperar.Name = "btnRecuperar";
            this.btnRecuperar.ShadowDecoration.BorderRadius = 20;
            this.btnRecuperar.ShadowDecoration.Depth = 20;
            this.btnRecuperar.ShadowDecoration.Enabled = true;
            this.btnRecuperar.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(1, 0, 1, 8);
            this.btnRecuperar.Size = new System.Drawing.Size(333, 46);
            this.btnRecuperar.TabIndex = 5;
            this.btnRecuperar.Text = "Olvide mi contraseña";
            this.btnRecuperar.Click += new System.EventHandler(this.btnRecuperar_Click);
            // 
            // btnIniciar
            // 
            this.btnIniciar.BackColor = System.Drawing.Color.Transparent;
            this.btnIniciar.BorderRadius = 10;
            this.btnIniciar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnIniciar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnIniciar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnIniciar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnIniciar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(187)))), ((int)(((byte)(33)))));
            this.btnIniciar.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIniciar.ForeColor = System.Drawing.Color.White;
            this.btnIniciar.Location = new System.Drawing.Point(63, 377);
            this.btnIniciar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.ShadowDecoration.BorderRadius = 20;
            this.btnIniciar.ShadowDecoration.Depth = 20;
            this.btnIniciar.ShadowDecoration.Enabled = true;
            this.btnIniciar.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(1, 0, 1, 8);
            this.btnIniciar.Size = new System.Drawing.Size(333, 46);
            this.btnIniciar.TabIndex = 4;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // txtContraseña
            // 
            this.txtContraseña.BorderRadius = 10;
            this.txtContraseña.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtContraseña.DefaultText = "";
            this.txtContraseña.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtContraseña.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtContraseña.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtContraseña.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtContraseña.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtContraseña.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtContraseña.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtContraseña.Location = new System.Drawing.Point(63, 309);
            this.txtContraseña.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtContraseña.MaxLength = 50;
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.PasswordChar = '*';
            this.txtContraseña.PlaceholderText = "";
            this.txtContraseña.SelectedText = "";
            this.txtContraseña.Size = new System.Drawing.Size(333, 48);
            this.txtContraseña.TabIndex = 2;
            // 
            // txtCorreo
            // 
            this.txtCorreo.BorderRadius = 10;
            this.txtCorreo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCorreo.DefaultText = "";
            this.txtCorreo.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCorreo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCorreo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCorreo.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCorreo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCorreo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCorreo.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCorreo.Location = new System.Drawing.Point(63, 187);
            this.txtCorreo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCorreo.MaxLength = 50;
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.PasswordChar = '\0';
            this.txtCorreo.PlaceholderText = "";
            this.txtCorreo.SelectedText = "";
            this.txtCorreo.Size = new System.Drawing.Size(333, 48);
            this.txtCorreo.TabIndex = 1;
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // BtnFullScreen
            // 
            this.BtnFullScreen.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.BtnFullScreen.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image2")));
            this.BtnFullScreen.HoverState.ImageSize = new System.Drawing.Size(28, 28);
            this.BtnFullScreen.Image = ((System.Drawing.Image)(resources.GetObject("BtnFullScreen.Image")));
            this.BtnFullScreen.ImageOffset = new System.Drawing.Point(0, 0);
            this.BtnFullScreen.ImageRotate = 0F;
            this.BtnFullScreen.ImageSize = new System.Drawing.Size(25, 25);
            this.BtnFullScreen.Location = new System.Drawing.Point(945, 2);
            this.BtnFullScreen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnFullScreen.Name = "BtnFullScreen";
            this.BtnFullScreen.PressedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image3")));
            this.BtnFullScreen.PressedState.ImageSize = new System.Drawing.Size(23, 23);
            this.BtnFullScreen.Size = new System.Drawing.Size(64, 54);
            this.BtnFullScreen.TabIndex = 4;
            // 
            // BtnExitFullScreen
            // 
            this.BtnExitFullScreen.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.BtnExitFullScreen.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.BtnExitFullScreen.HoverState.ImageSize = new System.Drawing.Size(28, 28);
            this.BtnExitFullScreen.Image = ((System.Drawing.Image)(resources.GetObject("BtnExitFullScreen.Image")));
            this.BtnExitFullScreen.ImageOffset = new System.Drawing.Point(0, 0);
            this.BtnExitFullScreen.ImageRotate = 0F;
            this.BtnExitFullScreen.ImageSize = new System.Drawing.Size(25, 25);
            this.BtnExitFullScreen.Location = new System.Drawing.Point(875, 2);
            this.BtnExitFullScreen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnExitFullScreen.Name = "BtnExitFullScreen";
            this.BtnExitFullScreen.PressedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.BtnExitFullScreen.PressedState.ImageSize = new System.Drawing.Size(23, 23);
            this.BtnExitFullScreen.Size = new System.Drawing.Size(64, 54);
            this.BtnExitFullScreen.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1083, 574);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.guna2ShadowPanel1.ResumeLayout(false);
            this.guna2ShadowPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private Guna.UI2.WinForms.Guna2TextBox txtContraseña;
        private Guna.UI2.WinForms.Guna2TextBox txtCorreo;
        private Guna.UI2.WinForms.Guna2TileButton btnIniciar;
        private Guna.UI2.WinForms.Guna2TileButton btnRecuperar;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2ImageCheckBox btnVerContraseñaLogin;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox2;
        private Guna.UI2.WinForms.Guna2ImageButton btnSalir;
        private Guna.UI2.WinForms.Guna2TileButton btnRegistroLogin;
        private Guna.UI2.WinForms.Guna2ImageButton BtnExitFullScreen;
        private Guna.UI2.WinForms.Guna2ImageButton BtnFullScreen;
    }
}

