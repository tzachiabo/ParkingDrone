namespace SOCIAL_APP_BUNIFU_FRAMEWORK_DEMO
{
    partial class tabMessages
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tabMessages));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            BunifuAnimatorNS.Animation animation1 = new BunifuAnimatorNS.Animation();
            this.btnEditPhoto = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bunifuTransition1 = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.SuspendLayout();
            // 
            // btnEditPhoto
            // 
            this.btnEditPhoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditPhoto.BackColor = System.Drawing.Color.Transparent;
            this.btnEditPhoto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEditPhoto.BackgroundImage")));
            this.btnEditPhoto.ButtonText = "   Start";
            this.btnEditPhoto.ButtonTextMarginLeft = 0;
            this.bunifuTransition1.SetDecoration(this.btnEditPhoto, BunifuAnimatorNS.DecorationType.None);
            this.btnEditPhoto.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.btnEditPhoto.DisabledFillColor = System.Drawing.Color.Gray;
            this.btnEditPhoto.DisabledForecolor = System.Drawing.Color.White;
            this.btnEditPhoto.ForeColor = System.Drawing.Color.White;
            this.btnEditPhoto.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btnEditPhoto.IconPadding = 5;
            this.btnEditPhoto.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btnEditPhoto.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(58)))), ((int)(((byte)(108)))));
            this.btnEditPhoto.IdleBorderRadius = 25;
            this.btnEditPhoto.IdleBorderThickness = 2;
            this.btnEditPhoto.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(48)))), ((int)(((byte)(69)))));
            this.btnEditPhoto.IdleIconLeftImage = ((System.Drawing.Image)(resources.GetObject("btnEditPhoto.IdleIconLeftImage")));
            this.btnEditPhoto.IdleIconRightImage = null;
            this.btnEditPhoto.Location = new System.Drawing.Point(698, 25);
            this.btnEditPhoto.Margin = new System.Windows.Forms.Padding(4);
            this.btnEditPhoto.Name = "btnEditPhoto";
            stateProperties1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(58)))), ((int)(((byte)(108)))));
            stateProperties1.BorderRadius = 25;
            stateProperties1.BorderThickness = 1;
            stateProperties1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(58)))), ((int)(((byte)(108)))));
            stateProperties1.IconLeftImage = null;
            stateProperties1.IconRightImage = null;
            this.btnEditPhoto.onHoverState = stateProperties1;
            this.btnEditPhoto.Size = new System.Drawing.Size(196, 43);
            this.btnEditPhoto.TabIndex = 10;
            this.btnEditPhoto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditPhoto.Click += new System.EventHandler(this.btnEditPhoto_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.bunifuTransition1.SetDecoration(this.panel1, BunifuAnimatorNS.DecorationType.None);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(280, 533);
            this.panel1.TabIndex = 12;
            // 
            // bunifuTransition1
            // 
            this.bunifuTransition1.AnimationType = BunifuAnimatorNS.AnimationType.ScaleAndHorizSlide;
            this.bunifuTransition1.Cursor = null;
            animation1.AnimateOnlyDifferences = true;
            animation1.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.BlindCoeff")));
            animation1.LeafCoeff = 0F;
            animation1.MaxTime = 1F;
            animation1.MinTime = 0F;
            animation1.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicCoeff")));
            animation1.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicShift")));
            animation1.MosaicSize = 0;
            animation1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            animation1.RotateCoeff = 0F;
            animation1.RotateLimit = 0F;
            animation1.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.ScaleCoeff")));
            animation1.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.SlideCoeff")));
            animation1.TimeCoeff = 0F;
            animation1.TransparencyCoeff = 0F;
            this.bunifuTransition1.DefaultAnimation = animation1;
            // 
            // tabMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(49)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnEditPhoto);
            this.bunifuTransition1.SetDecoration(this, BunifuAnimatorNS.DecorationType.None);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "tabMessages";
            this.Size = new System.Drawing.Size(940, 647);
            this.Load += new System.EventHandler(this.tabMessages_Load);
            this.ResumeLayout(false);

        }

        private void generate_prakings()
        {

        }

        #endregion
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnEditPhoto;
        private System.Windows.Forms.Panel panel1;
        private BunifuAnimatorNS.BunifuTransition bunifuTransition1;
    }
}
