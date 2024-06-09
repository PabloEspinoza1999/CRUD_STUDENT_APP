using System;
using Cassandra;

namespace INV_CASSANDRA.Connection
{
    public class ApplicationConnection
    {
        private const string Keyspace = "bd_keyspace"; // Varia de la necesidad
        private const string ContactPoint = "127.0.0.1"; 
        private const int Port = 9043; 

        private Cluster? _cluster;
        private Cassandra.ISession? _session;

      
        public Cassandra.ISession Connect()
        {
            try
            {
                _cluster = Cluster.Builder()
                                  .AddContactPoint(ContactPoint)
                                  .WithPort(Port) 
                                  .Build();

                _session = _cluster.Connect(Keyspace);
                return _session;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to Cassandra: {ex.Message}");
                return null;
            }
        }

    
        public void Close()
        {
            _cluster?.Dispose();
        }
    }
}
