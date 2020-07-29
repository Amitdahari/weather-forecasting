namespace WindowsFormsApp18
{
    partial class Form1
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
            this.choose_csv_button = new System.Windows.Forms.Button();
            this.process_button = new System.Windows.Forms.Button();
            this.map_reduce_button = new System.Windows.Forms.Button();
            this.view_File_path = new System.Windows.Forms.Label();
            this.get_result_button = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox_data = new System.Windows.Forms.GroupBox();
            this.inputGrid = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.outputGrid = new System.Windows.Forms.DataGridView();
            this.groupBox_data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // choose_csv_button
            // 
            this.choose_csv_button.Location = new System.Drawing.Point(25, 32);
            this.choose_csv_button.Name = "choose_csv_button";
            this.choose_csv_button.Size = new System.Drawing.Size(75, 23);
            this.choose_csv_button.TabIndex = 0;
            this.choose_csv_button.Text = "Browse";
            this.choose_csv_button.UseVisualStyleBackColor = true;
            this.choose_csv_button.Click += new System.EventHandler(this.choose_csv_button_Click);
            // 
            // process_button
            // 
            this.process_button.Location = new System.Drawing.Point(130, 681);
            this.process_button.Name = "process_button";
            this.process_button.Size = new System.Drawing.Size(75, 23);
            this.process_button.TabIndex = 1;
            this.process_button.Text = "Process";
            this.process_button.UseVisualStyleBackColor = true;
            // 
            // map_reduce_button
            // 
            this.map_reduce_button.Location = new System.Drawing.Point(361, 681);
            this.map_reduce_button.Name = "map_reduce_button";
            this.map_reduce_button.Size = new System.Drawing.Size(96, 23);
            this.map_reduce_button.TabIndex = 2;
            this.map_reduce_button.Text = "Map Reduce";
            this.map_reduce_button.UseVisualStyleBackColor = true;
            // 
            // view_File_path
            // 
            this.view_File_path.AutoSize = true;
            this.view_File_path.Location = new System.Drawing.Point(127, 37);
            this.view_File_path.Name = "view_File_path";
            this.view_File_path.Size = new System.Drawing.Size(122, 13);
            this.view_File_path.TabIndex = 4;
            this.view_File_path.Text = "Please choose a csv file";
            // 
            // get_result_button
            // 
            this.get_result_button.Location = new System.Drawing.Point(626, 681);
            this.get_result_button.Name = "get_result_button";
            this.get_result_button.Size = new System.Drawing.Size(75, 23);
            this.get_result_button.TabIndex = 5;
            this.get_result_button.Text = "Get Result File";
            this.get_result_button.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // groupBox_data
            // 
            this.groupBox_data.Controls.Add(this.inputGrid);
            this.groupBox_data.Location = new System.Drawing.Point(25, 61);
            this.groupBox_data.Name = "groupBox_data";
            this.groupBox_data.Size = new System.Drawing.Size(796, 438);
            this.groupBox_data.TabIndex = 7;
            this.groupBox_data.TabStop = false;
            this.groupBox_data.Text = "Data ";
            // 
            // inputGrid
            // 
            this.inputGrid.AllowUserToAddRows = false;
            this.inputGrid.AllowUserToDeleteRows = false;
            this.inputGrid.AllowUserToResizeColumns = false;
            this.inputGrid.AllowUserToResizeRows = false;
            this.inputGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inputGrid.Location = new System.Drawing.Point(73, 20);
            this.inputGrid.Name = "inputGrid";
            this.inputGrid.ReadOnly = true;
            this.inputGrid.Size = new System.Drawing.Size(642, 412);
            this.inputGrid.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.outputGrid);
            this.groupBox1.Location = new System.Drawing.Point(25, 505);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(796, 170);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Results";
            // 
            // outputGrid
            // 
            this.outputGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.outputGrid.Location = new System.Drawing.Point(73, 9);
            this.outputGrid.Name = "outputGrid";
            this.outputGrid.Size = new System.Drawing.Size(642, 150);
            this.outputGrid.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 739);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox_data);
            this.Controls.Add(this.get_result_button);
            this.Controls.Add(this.view_File_path);
            this.Controls.Add(this.map_reduce_button);
            this.Controls.Add(this.process_button);
            this.Controls.Add(this.choose_csv_button);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox_data.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inputGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outputGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button choose_csv_button;
        private System.Windows.Forms.Button process_button;
        private System.Windows.Forms.Button map_reduce_button;
        private System.Windows.Forms.Label view_File_path;
        private System.Windows.Forms.Button get_result_button;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox_data;
        private System.Windows.Forms.DataGridView inputGrid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView outputGrid;
    }
}

