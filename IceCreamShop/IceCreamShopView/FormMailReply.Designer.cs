
namespace IceCreamShopView
{
    partial class FormMailReply
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
            this.labelFrom = new System.Windows.Forms.Label();
            this.labelSubject = new System.Windows.Forms.Label();
            this.textBoxFrom = new System.Windows.Forms.TextBox();
            this.textBoxSubject = new System.Windows.Forms.TextBox();
            this.textBoxBody = new System.Windows.Forms.TextBox();
            this.labelBody = new System.Windows.Forms.Label();
            this.labelReplySubject = new System.Windows.Forms.Label();
            this.labelReplyText = new System.Windows.Forms.Label();
            this.textBoxReplySubject = new System.Windows.Forms.TextBox();
            this.textBoxReplyText = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(13, 13);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(46, 13);
            this.labelFrom.TabIndex = 0;
            this.labelFrom.Text = "От кого";
            // 
            // labelSubject
            // 
            this.labelSubject.AutoSize = true;
            this.labelSubject.Location = new System.Drawing.Point(13, 39);
            this.labelSubject.Name = "labelSubject";
            this.labelSubject.Size = new System.Drawing.Size(34, 13);
            this.labelSubject.TabIndex = 1;
            this.labelSubject.Text = "Тема";
            // 
            // textBoxFrom
            // 
            this.textBoxFrom.Location = new System.Drawing.Point(65, 10);
            this.textBoxFrom.Name = "textBoxFrom";
            this.textBoxFrom.ReadOnly = true;
            this.textBoxFrom.Size = new System.Drawing.Size(327, 20);
            this.textBoxFrom.TabIndex = 2;
            // 
            // textBoxSubject
            // 
            this.textBoxSubject.Location = new System.Drawing.Point(65, 36);
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.ReadOnly = true;
            this.textBoxSubject.Size = new System.Drawing.Size(327, 20);
            this.textBoxSubject.TabIndex = 3;
            // 
            // textBoxBody
            // 
            this.textBoxBody.Location = new System.Drawing.Point(65, 62);
            this.textBoxBody.Multiline = true;
            this.textBoxBody.Name = "textBoxBody";
            this.textBoxBody.ReadOnly = true;
            this.textBoxBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxBody.Size = new System.Drawing.Size(327, 318);
            this.textBoxBody.TabIndex = 4;
            // 
            // labelBody
            // 
            this.labelBody.AutoSize = true;
            this.labelBody.Location = new System.Drawing.Point(13, 65);
            this.labelBody.Name = "labelBody";
            this.labelBody.Size = new System.Drawing.Size(37, 13);
            this.labelBody.TabIndex = 5;
            this.labelBody.Text = "Текст";
            // 
            // labelReplySubject
            // 
            this.labelReplySubject.AutoSize = true;
            this.labelReplySubject.Location = new System.Drawing.Point(420, 39);
            this.labelReplySubject.Name = "labelReplySubject";
            this.labelReplySubject.Size = new System.Drawing.Size(71, 13);
            this.labelReplySubject.TabIndex = 6;
            this.labelReplySubject.Text = "Тема ответа";
            // 
            // labelReplyText
            // 
            this.labelReplyText.AutoSize = true;
            this.labelReplyText.Location = new System.Drawing.Point(420, 65);
            this.labelReplyText.Name = "labelReplyText";
            this.labelReplyText.Size = new System.Drawing.Size(74, 13);
            this.labelReplyText.TabIndex = 7;
            this.labelReplyText.Text = "Текст ответа";
            // 
            // textBoxReplySubject
            // 
            this.textBoxReplySubject.Location = new System.Drawing.Point(500, 36);
            this.textBoxReplySubject.Name = "textBoxReplySubject";
            this.textBoxReplySubject.Size = new System.Drawing.Size(322, 20);
            this.textBoxReplySubject.TabIndex = 8;
            // 
            // textBoxReplyText
            // 
            this.textBoxReplyText.Location = new System.Drawing.Point(500, 62);
            this.textBoxReplyText.Multiline = true;
            this.textBoxReplyText.Name = "textBoxReplyText";
            this.textBoxReplyText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxReplyText.Size = new System.Drawing.Size(322, 318);
            this.textBoxReplyText.TabIndex = 9;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(747, 7);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 10;
            this.buttonSend.Text = "Отправить";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // FormMailReply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 392);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxReplyText);
            this.Controls.Add(this.textBoxReplySubject);
            this.Controls.Add(this.labelReplyText);
            this.Controls.Add(this.labelReplySubject);
            this.Controls.Add(this.labelBody);
            this.Controls.Add(this.textBoxBody);
            this.Controls.Add(this.textBoxSubject);
            this.Controls.Add(this.textBoxFrom);
            this.Controls.Add(this.labelSubject);
            this.Controls.Add(this.labelFrom);
            this.MaximizeBox = false;
            this.Name = "FormMailReply";
            this.Text = "Ответить";
            this.Load += new System.EventHandler(this.FormMailReply_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.Label labelSubject;
        private System.Windows.Forms.TextBox textBoxFrom;
        private System.Windows.Forms.TextBox textBoxSubject;
        private System.Windows.Forms.TextBox textBoxBody;
        private System.Windows.Forms.Label labelBody;
        private System.Windows.Forms.Label labelReplySubject;
        private System.Windows.Forms.Label labelReplyText;
        private System.Windows.Forms.TextBox textBoxReplySubject;
        private System.Windows.Forms.TextBox textBoxReplyText;
        private System.Windows.Forms.Button buttonSend;
    }
}