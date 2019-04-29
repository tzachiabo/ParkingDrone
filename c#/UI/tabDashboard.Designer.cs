namespace SOCIAL_APP_BUNIFU_FRAMEWORK_DEMO
{
    partial class tabDashboard
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
            this.chartDelay = new System.Windows.Forms.Timer(this.components);
            this.progressBarUpdate = new System.Windows.Forms.Timer(this.components);
            this.map_mission_map = new GMap.NET.WindowsForms.GMapControl();
            this.SuspendLayout();
            // 
            // chartDelay
            // 
            this.chartDelay.Enabled = true;
            this.chartDelay.Tick += new System.EventHandler(this.chartDelay_Tick);
            // 
            // progressBarUpdate
            // 
            this.progressBarUpdate.Enabled = true;
            this.progressBarUpdate.Interval = 2000;
            this.progressBarUpdate.Tick += new System.EventHandler(this.progressBarUpdate_Tick);
            // 
            // map_mission_map
            // 
            this.map_mission_map.Bearing = 0F;
            this.map_mission_map.CanDragMap = true;
            this.map_mission_map.EmptyTileColor = System.Drawing.Color.Navy;
            this.map_mission_map.GrayScaleMode = false;
            this.map_mission_map.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.map_mission_map.LevelsKeepInMemmory = 5;
            this.map_mission_map.Location = new System.Drawing.Point(3, 103);
            this.map_mission_map.MarkersEnabled = true;
            this.map_mission_map.MaxZoom = 2;
            this.map_mission_map.MinZoom = 2;
            this.map_mission_map.MouseWheelZoomEnabled = true;
            this.map_mission_map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.map_mission_map.Name = "map_mission_map";
            this.map_mission_map.NegativeMode = false;
            this.map_mission_map.PolygonsEnabled = true;
            this.map_mission_map.RetryLoadTile = 0;
            this.map_mission_map.RoutesEnabled = true;
            this.map_mission_map.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.map_mission_map.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.map_mission_map.ShowTileGridLines = false;
            this.map_mission_map.Size = new System.Drawing.Size(934, 541);
            this.map_mission_map.TabIndex = 0;
            this.map_mission_map.Zoom = 0D;
            // 
            // tabDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(49)))));
            this.Controls.Add(this.map_mission_map);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "tabDashboard";
            this.Size = new System.Drawing.Size(940, 647);
            this.Load += new System.EventHandler(this.tabDashboard_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer chartDelay;
        private System.Windows.Forms.Timer progressBarUpdate;
        private GMap.NET.WindowsForms.GMapControl map_mission_map;
    }
}
