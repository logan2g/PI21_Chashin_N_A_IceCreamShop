
namespace IceCreamShopView
{
    partial class FormImplementers
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
            this.ButtonUpd = new System.Windows.Forms.Button();
            this.ButtonDel = new System.Windows.Forms.Button();
            this.ButtonRef = new System.Windows.Forms.Button();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonUpd
            // 
            this.ButtonUpd.Location = new System.Drawing.Point(432, 98);
            this.ButtonUpd.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonUpd.Name = "ButtonUpd";
            this.ButtonUpd.Size = new System.Drawing.Size(81, 24);
            this.ButtonUpd.TabIndex = 14;
            this.ButtonUpd.Text = "Обновить";
            this.ButtonUpd.UseVisualStyleBackColor = true;
            this.ButtonUpd.Click += new System.EventHandler(this.ButtonRef_Click);
            // 
            // ButtonDel
            // 
            this.ButtonDel.Location = new System.Drawing.Point(432, 68);
            this.ButtonDel.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonDel.Name = "ButtonDel";
            this.ButtonDel.Size = new System.Drawing.Size(81, 26);
            this.ButtonDel.TabIndex = 13;
            this.ButtonDel.Text = "Удалить";
            this.ButtonDel.UseVisualStyleBackColor = true;
            this.ButtonDel.Click += new System.EventHandler(this.ButtonDel_Click);
            // 
            // ButtonRef
            // 
            this.ButtonRef.Location = new System.Drawing.Point(432, 40);
            this.ButtonRef.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonRef.Name = "ButtonRef";
            this.ButtonRef.Size = new System.Drawing.Size(81, 24);
            this.ButtonRef.TabIndex = 12;
            this.ButtonRef.Text = "Изменить";
            this.ButtonRef.UseVisualStyleBackColor = true;
            this.ButtonRef.Click += new System.EventHandler(this.ButtonUpd_Click);
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.Location = new System.Drawing.Point(432, 11);
            this.ButtonAdd.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(81, 25);
            this.ButtonAdd.TabIndex = 11;
            this.ButtonAdd.Text = "Добавить";
            this.ButtonAdd.UseVisualStyleBackColor = true;
            this.ButtonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 62;
            this.dataGridView.RowTemplate.Height = 28;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(428, 291);
            this.dataGridView.TabIndex = 10;
            // 
            // FormImplementers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 292);
            this.Controls.Add(this.ButtonUpd);
            this.Controls.Add(this.ButtonDel);
            this.Controls.Add(this.ButtonRef);
            this.Controls.Add(this.ButtonAdd);
            this.Controls.Add(this.dataGridView);
            this.MaximizeBox = false;
            this.Name = "FormImplementers";
            this.Text = "Исполнители";
            this.Load += new System.EventHandler(this.FormImplementers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonUpd;
        private System.Windows.Forms.Button ButtonDel;
        private System.Windows.Forms.Button ButtonRef;
        private System.Windows.Forms.Button ButtonAdd;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}