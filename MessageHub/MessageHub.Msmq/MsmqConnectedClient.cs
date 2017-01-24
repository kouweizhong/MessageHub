﻿namespace MessageHub.Msmq
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Messaging;
    using System.Text;
    using System.Threading.Tasks;

    public class MsmqConnectedClient : IConnectedClient, IMessageHubServiceReceiver, IDisposable
    {
        private MessageQueue _queue;

        public Guid Id { get; set; }

        public string QueuePath { get; set; }

        public void Receive(Guid hubId, MessageHub.Message message)
        {
            if (_queue == null)
            {
                if (!MessageQueue.Exists(QueuePath))
                {
                    MessageQueue.Create(QueuePath);
                }

                _queue = new MessageQueue(QueuePath);
            }

            MessageEnvelope env = new MessageEnvelope
            {
                SenderId = hubId,
                Contents = message
            };

            Message msg = new Message(env);
            _queue.Send(msg);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_queue != null)
                    {
                        _queue.Close();
                        _queue.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}