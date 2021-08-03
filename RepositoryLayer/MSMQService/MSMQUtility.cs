using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Text;


namespace RepositoryLayer.MSMQService
{
    public class MSMQUtility
    {
        public void SendMessage()
        {
            var url = "Link to reset your password : https://localhost:44359/api/users/resetpassword";

            MessageQueue messageQueue = new MessageQueue();
            if (MessageQueue.Exists(@".\Private$\MyQueue"))
            {
                messageQueue = new MessageQueue(@".\Private$\MyQueue");
            }
            else
            {
                messageQueue = MessageQueue.Create(@".\Private$\MyQueue");
            }
            Message message = new Message();
            message.Formatter = new BinaryMessageFormatter();
            message.Body = url;
            messageQueue.Label = "url link";
            messageQueue.Send(message);
        }

        public string ReceiveMessage()
        {
            MessageQueue reciever = new MessageQueue(@".\Private$\MyQueue");
            var recieving = reciever.Receive();
            recieving.Formatter = new BinaryMessageFormatter();
            string linkToBeSend = recieving.Body.ToString();
            return linkToBeSend;
        }
    }
}
