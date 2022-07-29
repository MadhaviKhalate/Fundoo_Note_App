using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class MSMQmodel
    {
        MessageQueue messageQueue = new MessageQueue();

        public void sendData2Queue(string token)
        {
            messageQueue.Path = @".\private$\token";
            if(!MessageQueue.Exists(messageQueue.Path))
            {
                //Exists
                MessageQueue.Create(messageQueue.Path);
            }
           
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
            messageQueue.Send(token);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }

        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = messageQueue.EndReceive(e.AsyncResult);
            string Token = msg.Body.ToString();
            string Subject = "Fundoonotes reset link";
            string Body = Token;
            var SMTP = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("madhavikhalate123@gmail.com", "ovhlontshvyrnmbd"),
                EnableSsl = true,
            };
            SMTP.Send("madhavikhalate123@gmail.com", "madhavikhalate123@gmail.com", Subject, Body);
            messageQueue.BeginReceive();
        }
    }
}
