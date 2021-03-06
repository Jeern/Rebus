﻿using System;
using System.IO;
using Rebus.Tests.Contracts.Transports;
using Rebus.Tests.Contracts.Utilities;
using Rebus.Transport;
using Rebus.Transport.FileSystem;

namespace Rebus.Tests.Transport.FileSystem
{
    public class FileSystemTransportFactory : ITransportFactory
    {
        readonly string _baseDirectory;

        public FileSystemTransportFactory()
        {
#if NET45
            _baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "messages");
#elif NETSTANDARD1_6
            _baseDirectory = Path.Combine(AppContext.BaseDirectory, "messages");
#endif

            CleanUp();
        }

        public ITransport CreateOneWayClient()
        {
            return new FileSystemTransport(_baseDirectory, null);
        }

        public ITransport Create(string inputQueueAddress)
        {
            return new FileSystemTransport(_baseDirectory, inputQueueAddress);
        }

        public void CleanUp()
        {
            DeleteHelper.DeleteDirectory(_baseDirectory);
        }
    }
}