namespace AuditingExample
{
    partial class DemoForm
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
            this.GetAuditEntriesButton = new System.Windows.Forms.Button();
            this.grid = new System.Windows.Forms.DataGridView();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.WriteAuditEvent = new System.Windows.Forms.Button();
            this.QueryButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // GetAuditEntriesButton
            // 
            this.GetAuditEntriesButton.Location = new System.Drawing.Point(11, 12);
            this.GetAuditEntriesButton.Name = "GetAuditEntriesButton";
            this.GetAuditEntriesButton.Size = new System.Drawing.Size(178, 32);
            this.GetAuditEntriesButton.TabIndex = 0;
            this.GetAuditEntriesButton.Text = "Get Audit Entries";
            this.GetAuditEntriesButton.UseVisualStyleBackColor = true;
            this.GetAuditEntriesButton.Click += new System.EventHandler(this.GetAuditEntriesButton_Click);
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(12, 50);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(729, 390);
            this.grid.TabIndex = 1;
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(195, 12);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(178, 32);
            this.UpdateButton.TabIndex = 2;
            this.UpdateButton.Text = "Update Settings";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // WriteAuditEvent
            // 
            this.WriteAuditEvent.Location = new System.Drawing.Point(379, 12);
            this.WriteAuditEvent.Name = "WriteAuditEvent";
            this.WriteAuditEvent.Size = new System.Drawing.Size(178, 32);
            this.WriteAuditEvent.TabIndex = 2;
            this.WriteAuditEvent.Text = "Write Audit Event";
            this.WriteAuditEvent.UseVisualStyleBackColor = true;
            this.WriteAuditEvent.Click += new System.EventHandler(this.WriteAuditEvent_Click);
            // 
            // QueryButton
            // 
            this.QueryButton.Location = new System.Drawing.Point(563, 12);
            this.QueryButton.Name = "QueryButton";
            this.QueryButton.Size = new System.Drawing.Size(178, 32);
            this.QueryButton.TabIndex = 3;
            this.QueryButton.Text = "SPAuditQuery";
            this.QueryButton.UseVisualStyleBackColor = true;
            this.QueryButton.Click += new System.EventHandler(this.QueryButton_Click);
            // 
            // DemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 452);
            this.Controls.Add(this.QueryButton);
            this.Controls.Add(this.WriteAuditEvent);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.GetAuditEntriesButton);
            this.Name = "DemoForm";
            this.Text = "Demo Form";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button GetAuditEntriesButton;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button WriteAuditEvent;
        private System.Windows.Forms.Button QueryButton;
    }
}

