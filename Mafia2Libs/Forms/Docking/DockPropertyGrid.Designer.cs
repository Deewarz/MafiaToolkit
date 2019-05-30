﻿namespace Forms.Docking
{
    partial class DockPropertyGrid
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.QuickEditTab = new System.Windows.Forms.TabPage();
            this.RotationZNumeric = new System.Windows.Forms.NumericUpDown();
            this.RotationYNumeric = new System.Windows.Forms.NumericUpDown();
            this.RotationXNumeric = new System.Windows.Forms.NumericUpDown();
            this.PositionZNumeric = new System.Windows.Forms.NumericUpDown();
            this.PositionYNumeric = new System.Windows.Forms.NumericUpDown();
            this.PositionXNumeric = new System.Windows.Forms.NumericUpDown();
            this.CurrentEntry = new System.Windows.Forms.Label();
            this.RotationZLabel = new System.Windows.Forms.Label();
            this.RotationYLabel = new System.Windows.Forms.Label();
            this.RotationXLabel = new System.Windows.Forms.Label();
            this.PositionZLabel = new System.Windows.Forms.Label();
            this.PositionYLabel = new System.Windows.Forms.Label();
            this.PositionXLabel = new System.Windows.Forms.Label();
            this.PropertyTab = new System.Windows.Forms.TabPage();
            this.PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.QuickEditTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RotationZNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RotationYNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RotationXNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionZNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionYNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionXNumeric)).BeginInit();
            this.PropertyTab.SuspendLayout();
            this.MainTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // QuickEditTab
            // 
            this.QuickEditTab.Controls.Add(this.RotationZNumeric);
            this.QuickEditTab.Controls.Add(this.RotationYNumeric);
            this.QuickEditTab.Controls.Add(this.RotationXNumeric);
            this.QuickEditTab.Controls.Add(this.PositionZNumeric);
            this.QuickEditTab.Controls.Add(this.PositionYNumeric);
            this.QuickEditTab.Controls.Add(this.PositionXNumeric);
            this.QuickEditTab.Controls.Add(this.CurrentEntry);
            this.QuickEditTab.Controls.Add(this.RotationZLabel);
            this.QuickEditTab.Controls.Add(this.RotationYLabel);
            this.QuickEditTab.Controls.Add(this.RotationXLabel);
            this.QuickEditTab.Controls.Add(this.PositionZLabel);
            this.QuickEditTab.Controls.Add(this.PositionYLabel);
            this.QuickEditTab.Controls.Add(this.PositionXLabel);
            this.QuickEditTab.Location = new System.Drawing.Point(4, 22);
            this.QuickEditTab.Name = "QuickEditTab";
            this.QuickEditTab.Padding = new System.Windows.Forms.Padding(3);
            this.QuickEditTab.Size = new System.Drawing.Size(259, 424);
            this.QuickEditTab.TabIndex = 1;
            this.QuickEditTab.Text = "Edit Transform";
            this.QuickEditTab.UseVisualStyleBackColor = true;
            // 
            // RotationZNumeric
            // 
            this.RotationZNumeric.DecimalPlaces = 5;
            this.RotationZNumeric.Location = new System.Drawing.Point(67, 162);
            this.RotationZNumeric.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.RotationZNumeric.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.RotationZNumeric.Name = "RotationZNumeric";
            this.RotationZNumeric.Size = new System.Drawing.Size(185, 20);
            this.RotationZNumeric.TabIndex = 23;
            // 
            // RotationYNumeric
            // 
            this.RotationYNumeric.DecimalPlaces = 5;
            this.RotationYNumeric.Location = new System.Drawing.Point(67, 136);
            this.RotationYNumeric.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.RotationYNumeric.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.RotationYNumeric.Name = "RotationYNumeric";
            this.RotationYNumeric.Size = new System.Drawing.Size(185, 20);
            this.RotationYNumeric.TabIndex = 22;
            // 
            // RotationXNumeric
            // 
            this.RotationXNumeric.DecimalPlaces = 5;
            this.RotationXNumeric.Location = new System.Drawing.Point(67, 110);
            this.RotationXNumeric.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.RotationXNumeric.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.RotationXNumeric.Name = "RotationXNumeric";
            this.RotationXNumeric.Size = new System.Drawing.Size(185, 20);
            this.RotationXNumeric.TabIndex = 21;
            // 
            // PositionZNumeric
            // 
            this.PositionZNumeric.DecimalPlaces = 5;
            this.PositionZNumeric.Location = new System.Drawing.Point(67, 84);
            this.PositionZNumeric.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.PositionZNumeric.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.PositionZNumeric.Name = "PositionZNumeric";
            this.PositionZNumeric.Size = new System.Drawing.Size(185, 20);
            this.PositionZNumeric.TabIndex = 20;
            // 
            // PositionYNumeric
            // 
            this.PositionYNumeric.DecimalPlaces = 5;
            this.PositionYNumeric.Location = new System.Drawing.Point(67, 58);
            this.PositionYNumeric.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.PositionYNumeric.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.PositionYNumeric.Name = "PositionYNumeric";
            this.PositionYNumeric.Size = new System.Drawing.Size(185, 20);
            this.PositionYNumeric.TabIndex = 19;
            // 
            // PositionXNumeric
            // 
            this.PositionXNumeric.DecimalPlaces = 5;
            this.PositionXNumeric.Location = new System.Drawing.Point(67, 32);
            this.PositionXNumeric.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.PositionXNumeric.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.PositionXNumeric.Name = "PositionXNumeric";
            this.PositionXNumeric.Size = new System.Drawing.Size(185, 20);
            this.PositionXNumeric.TabIndex = 18;
            // 
            // CurrentEntry
            // 
            this.CurrentEntry.AutoSize = true;
            this.CurrentEntry.Location = new System.Drawing.Point(10, 7);
            this.CurrentEntry.Name = "CurrentEntry";
            this.CurrentEntry.Size = new System.Drawing.Size(111, 13);
            this.CurrentEntry.TabIndex = 12;
            this.CurrentEntry.Text = "FRAME NAME HERE";
            // 
            // RotationZLabel
            // 
            this.RotationZLabel.AutoSize = true;
            this.RotationZLabel.Location = new System.Drawing.Point(7, 164);
            this.RotationZLabel.Name = "RotationZLabel";
            this.RotationZLabel.Size = new System.Drawing.Size(57, 13);
            this.RotationZLabel.TabIndex = 10;
            this.RotationZLabel.Text = "Rotation Z";
            // 
            // RotationYLabel
            // 
            this.RotationYLabel.AutoSize = true;
            this.RotationYLabel.Location = new System.Drawing.Point(7, 138);
            this.RotationYLabel.Name = "RotationYLabel";
            this.RotationYLabel.Size = new System.Drawing.Size(57, 13);
            this.RotationYLabel.TabIndex = 8;
            this.RotationYLabel.Text = "Rotation Y";
            // 
            // RotationXLabel
            // 
            this.RotationXLabel.AutoSize = true;
            this.RotationXLabel.Location = new System.Drawing.Point(7, 112);
            this.RotationXLabel.Name = "RotationXLabel";
            this.RotationXLabel.Size = new System.Drawing.Size(57, 13);
            this.RotationXLabel.TabIndex = 6;
            this.RotationXLabel.Text = "Rotation X";
            // 
            // PositionZLabel
            // 
            this.PositionZLabel.AutoSize = true;
            this.PositionZLabel.Location = new System.Drawing.Point(7, 86);
            this.PositionZLabel.Name = "PositionZLabel";
            this.PositionZLabel.Size = new System.Drawing.Size(54, 13);
            this.PositionZLabel.TabIndex = 4;
            this.PositionZLabel.Text = "Position Z";
            // 
            // PositionYLabel
            // 
            this.PositionYLabel.AutoSize = true;
            this.PositionYLabel.Location = new System.Drawing.Point(7, 60);
            this.PositionYLabel.Name = "PositionYLabel";
            this.PositionYLabel.Size = new System.Drawing.Size(54, 13);
            this.PositionYLabel.TabIndex = 2;
            this.PositionYLabel.Text = "Position Y";
            // 
            // PositionXLabel
            // 
            this.PositionXLabel.AutoSize = true;
            this.PositionXLabel.Location = new System.Drawing.Point(7, 34);
            this.PositionXLabel.Name = "PositionXLabel";
            this.PositionXLabel.Size = new System.Drawing.Size(54, 13);
            this.PositionXLabel.TabIndex = 0;
            this.PositionXLabel.Text = "Position X";
            // 
            // PropertyTab
            // 
            this.PropertyTab.Controls.Add(this.PropertyGrid);
            this.PropertyTab.Location = new System.Drawing.Point(4, 22);
            this.PropertyTab.Name = "PropertyTab";
            this.PropertyTab.Padding = new System.Windows.Forms.Padding(3);
            this.PropertyTab.Size = new System.Drawing.Size(259, 424);
            this.PropertyTab.TabIndex = 0;
            this.PropertyTab.Text = "Property Grid";
            this.PropertyTab.UseVisualStyleBackColor = true;
            // 
            // PropertyGrid
            // 
            this.PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.PropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.PropertyGrid.Name = "PropertyGrid";
            this.PropertyGrid.Size = new System.Drawing.Size(253, 418);
            this.PropertyGrid.TabIndex = 2;
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.PropertyTab);
            this.MainTabControl.Controls.Add(this.QuickEditTab);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(267, 450);
            this.MainTabControl.TabIndex = 7;
            // 
            // DockPropertyGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 450);
            this.Controls.Add(this.MainTabControl);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.HideOnClose = true;
            this.Name = "DockPropertyGrid";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight;
            this.TabText = "Frame Properties";
            this.Text = "PropertyGrid";
            this.QuickEditTab.ResumeLayout(false);
            this.QuickEditTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RotationZNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RotationYNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RotationXNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionZNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionYNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionXNumeric)).EndInit();
            this.PropertyTab.ResumeLayout(false);
            this.MainTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage QuickEditTab;
        public System.Windows.Forms.NumericUpDown RotationZNumeric;
        public System.Windows.Forms.NumericUpDown RotationYNumeric;
        public System.Windows.Forms.NumericUpDown RotationXNumeric;
        public System.Windows.Forms.NumericUpDown PositionZNumeric;
        public System.Windows.Forms.NumericUpDown PositionYNumeric;
        public System.Windows.Forms.NumericUpDown PositionXNumeric;
        private System.Windows.Forms.Label CurrentEntry;
        private System.Windows.Forms.Label RotationZLabel;
        private System.Windows.Forms.Label RotationYLabel;
        private System.Windows.Forms.Label RotationXLabel;
        private System.Windows.Forms.Label PositionZLabel;
        private System.Windows.Forms.Label PositionYLabel;
        private System.Windows.Forms.Label PositionXLabel;
        private System.Windows.Forms.TabPage PropertyTab;
        public System.Windows.Forms.PropertyGrid PropertyGrid;
        private System.Windows.Forms.TabControl MainTabControl;
    }
}