﻿namespace HeySteimkeWindows
{
    partial class HeySteimkeStart
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPlaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.execSkriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemsTreeView = new System.Windows.Forms.TreeView();
            this.itemContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.placeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addItemMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editPlaceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletePlaceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.itemContextMenu.SuspendLayout();
            this.placeContextMenu.SuspendLayout();
            this.userContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.internToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(756, 40);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateToolStripMenuItem,
            this.addPlaceToolStripMenuItem,
            this.loginToolStripMenuItem,
            this.serverToolStripMenuItem,
            this.beendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(91, 36);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(255, 44);
            this.updateToolStripMenuItem.Text = "Update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // addPlaceToolStripMenuItem
            // 
            this.addPlaceToolStripMenuItem.Name = "addPlaceToolStripMenuItem";
            this.addPlaceToolStripMenuItem.Size = new System.Drawing.Size(255, 44);
            this.addPlaceToolStripMenuItem.Text = "Add place";
            this.addPlaceToolStripMenuItem.Click += new System.EventHandler(this.addPlaceToolStripMenuItem_Click);
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(255, 44);
            this.loginToolStripMenuItem.Text = "Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(255, 44);
            this.serverToolStripMenuItem.Text = "Server";
            this.serverToolStripMenuItem.Click += new System.EventHandler(this.serverToolStripMenuItem_Click);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(255, 44);
            this.beendenToolStripMenuItem.Text = "Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // internToolStripMenuItem
            // 
            this.internToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.execSkriptToolStripMenuItem,
            this.userViewToolStripMenuItem,
            this.addUserToolStripMenuItem});
            this.internToolStripMenuItem.Name = "internToolStripMenuItem";
            this.internToolStripMenuItem.Size = new System.Drawing.Size(98, 36);
            this.internToolStripMenuItem.Text = "Intern";
            // 
            // execSkriptToolStripMenuItem
            // 
            this.execSkriptToolStripMenuItem.Name = "execSkriptToolStripMenuItem";
            this.execSkriptToolStripMenuItem.Size = new System.Drawing.Size(257, 44);
            this.execSkriptToolStripMenuItem.Text = "ExecSkript";
            this.execSkriptToolStripMenuItem.Click += new System.EventHandler(this.execSkriptToolStripMenuItem_Click);
            // 
            // userViewToolStripMenuItem
            // 
            this.userViewToolStripMenuItem.Name = "userViewToolStripMenuItem";
            this.userViewToolStripMenuItem.Size = new System.Drawing.Size(257, 44);
            this.userViewToolStripMenuItem.Text = "UserView";
            this.userViewToolStripMenuItem.Click += new System.EventHandler(this.userViewToolStripMenuItem_Click);
            // 
            // addUserToolStripMenuItem
            // 
            this.addUserToolStripMenuItem.Name = "addUserToolStripMenuItem";
            this.addUserToolStripMenuItem.Size = new System.Drawing.Size(257, 44);
            this.addUserToolStripMenuItem.Text = "Add User";
            this.addUserToolStripMenuItem.Click += new System.EventHandler(this.addUserToolStripMenuItem_Click);
            // 
            // ItemsTreeView
            // 
            this.ItemsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemsTreeView.Location = new System.Drawing.Point(0, 40);
            this.ItemsTreeView.Margin = new System.Windows.Forms.Padding(4);
            this.ItemsTreeView.Name = "ItemsTreeView";
            this.ItemsTreeView.Size = new System.Drawing.Size(756, 775);
            this.ItemsTreeView.TabIndex = 1;
            this.ItemsTreeView.DoubleClick += new System.EventHandler(this.ItemsTreeView_DoubleClick);
            this.ItemsTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ItemsTreeView_MouseDown);
            // 
            // itemContextMenu
            // 
            this.itemContextMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.itemContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.itemContextMenu.Name = "itemContextMenu";
            this.itemContextMenu.Size = new System.Drawing.Size(161, 80);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(160, 38);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(160, 38);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // placeContextMenu
            // 
            this.placeContextMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.placeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addItemMenuItem,
            this.editPlaceMenuItem,
            this.deletePlaceMenuItem});
            this.placeContextMenu.Name = "placeContextMenu";
            this.placeContextMenu.Size = new System.Drawing.Size(189, 118);
            // 
            // addItemMenuItem
            // 
            this.addItemMenuItem.Name = "addItemMenuItem";
            this.addItemMenuItem.Size = new System.Drawing.Size(188, 38);
            this.addItemMenuItem.Text = "Add item";
            this.addItemMenuItem.Click += new System.EventHandler(this.addItemMenuItem_Click);
            // 
            // editPlaceMenuItem
            // 
            this.editPlaceMenuItem.Name = "editPlaceMenuItem";
            this.editPlaceMenuItem.Size = new System.Drawing.Size(188, 38);
            this.editPlaceMenuItem.Text = "Edit";
            this.editPlaceMenuItem.Click += new System.EventHandler(this.editPlaceMenuItem_Click);
            // 
            // deletePlaceMenuItem
            // 
            this.deletePlaceMenuItem.Name = "deletePlaceMenuItem";
            this.deletePlaceMenuItem.Size = new System.Drawing.Size(188, 38);
            this.deletePlaceMenuItem.Text = "Delete";
            this.deletePlaceMenuItem.Click += new System.EventHandler(this.deletePlaceMenuItem_Click);
            // 
            // userContextMenuStrip
            // 
            this.userContextMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.userContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem1,
            this.deleteToolStripMenuItem1});
            this.userContextMenuStrip.Name = "userContextMenuStrip";
            this.userContextMenuStrip.Size = new System.Drawing.Size(301, 124);
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            this.editToolStripMenuItem1.Size = new System.Drawing.Size(300, 38);
            this.editToolStripMenuItem1.Text = "Edit";
            this.editToolStripMenuItem1.Click += new System.EventHandler(this.editToolStripMenuItem1_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(300, 38);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // HeySteimkeStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 815);
            this.Controls.Add(this.ItemsTreeView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "HeySteimkeStart";
            this.Text = "HeySteimke Client";
            this.Load += new System.EventHandler(this.HeySteimkeStart_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.itemContextMenu.ResumeLayout(false);
            this.placeContextMenu.ResumeLayout(false);
            this.userContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.TreeView ItemsTreeView;
        private System.Windows.Forms.ContextMenuStrip itemContextMenu;
        private System.Windows.Forms.ToolStripMenuItem internToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem execSkriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip placeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addItemMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editPlaceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePlaceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userViewToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip userContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPlaceToolStripMenuItem;
    }
}

