namespace CycleProcessControl
{
	partial class CreatePatternDialog
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
			this.SaveButton = new System.Windows.Forms.Button();
			this.NameBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// SaveButton
			// 
			this.SaveButton.Location = new System.Drawing.Point(263, 12);
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Size = new System.Drawing.Size(75, 23);
			this.SaveButton.TabIndex = 0;
			this.SaveButton.Text = "Създай";
			this.SaveButton.UseVisualStyleBackColor = true;
			this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
			// 
			// NameBox
			// 
			this.NameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.NameBox.Location = new System.Drawing.Point(12, 14);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(245, 20);
			this.NameBox.TabIndex = 1;
			// 
			// CreatePatternDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(350, 48);
			this.Controls.Add(this.NameBox);
			this.Controls.Add(this.SaveButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "CreatePatternDialog";
			this.Text = "Създаване на цикъл";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button SaveButton;
		private System.Windows.Forms.TextBox NameBox;
	}
}