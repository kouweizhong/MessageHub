﻿namespace MessageHub.Wcf
{
    using System.ServiceModel.Channels;
    using Interfaces;

    public class WcfMessageHubConfiguration : IHubConfiguration
    {
        public bool UseEncryption { get; set; }

        public string RemoteEndpoint { get; set; }

        public Binding Binding { get; set; }
    }
}
