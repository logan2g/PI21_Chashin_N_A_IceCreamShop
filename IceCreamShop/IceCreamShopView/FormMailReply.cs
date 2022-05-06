using System;
using IceCreamShopContracts.BusinessLogicsContracts;
using IceCreamShopBusinessLogic.MailWorker;
using IceCreamShopContracts.BindingModels;
using System.Windows.Forms;
using System.Linq;

namespace IceCreamShopView
{
    public partial class FormMailReply : Form
    {
        public string MessageId;
        private readonly IMessageInfoLogic _logic;
        private readonly AbstractMailWorker _mailWorker;

        public FormMailReply(IMessageInfoLogic logic, AbstractMailWorker mailWorker)
        {
            InitializeComponent();
            _logic = logic;
            _mailWorker = mailWorker;
        }

        private void LoadData()
        {
            var message = _logic.Read(null).FirstOrDefault(rec => rec.MessageId.Equals(MessageId));
            if (message == null)
            {
                throw new Exception("Письмо не найдено");
            }
            textBoxFrom.Text = message.SenderName;
            textBoxSubject.Text = message.Subject;
            textBoxBody.Text = message.Body;
            textBoxReplySubject.Text = "Re: " + message.Subject;
            if (!string.IsNullOrEmpty(message.Reply))
            {
                buttonSend.Enabled = false;
                textBoxReplyText.Text = message.Reply;
            }
        }

        private void FormMailReply_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            _mailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = textBoxFrom.Text,
                Subject = textBoxReplySubject.Text,
                Text = textBoxReplyText.Text
            });
            _logic.CreateOrUpdate(new MessageInfoBindingModel
            {
                MessageId = MessageId,
                Reply = textBoxReplyText.Text
            });
            MessageBox.Show("Отправлено!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
