namespace MeshViewer
{
	partial class MeshView
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
			if (disposing && (components != null)) { components.Dispose(); }

			base.Dispose(disposing);
		}

#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panelDrawArea = new System.Windows.Forms.Panel();
			this.SuspendLayout();

			// 
			// panelDrawArea
			// 
			this.panelDrawArea.AutoScroll  =  true;
			this.panelDrawArea.BorderStyle =  System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelDrawArea.Location    =  new System.Drawing.Point(12, 12);
			this.panelDrawArea.Name        =  "panelDrawArea";
			this.panelDrawArea.Size        =  new System.Drawing.Size(248, 247);
			this.panelDrawArea.TabIndex    =  0;
			this.panelDrawArea.Paint       += new System.Windows.Forms.PaintEventHandler(this.panelDrawArea_Paint);

			// 
			// MeshView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll          = true;
			this.AutoSize            = true;
			this.ClientSize          = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.panelDrawArea);
			this.Name        =  "MeshView";
			this.Text        =  "Mesh Viewer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MeshView_FormClosing);
			this.ResumeLayout(false);
		}

		private System.Windows.Forms.Panel panelDrawArea;

#endregion
	}
}