using System.IO.Pipes;

namespace CountElements.Server
{
    public class Connection
    {
        private string NamePipeline = string.Empty;

        public Connection(string _namePipeline = ".myPipe") => NamePipeline = _namePipeline;

        public async Task Start()
        {
            try
            {
                while (true)
                {
                    using (NamedPipeServerStream PipeServer = new(NamePipeline))
                    {
                        await PipeServer.WaitForConnectionAsync();

                        using (StreamReader reader = new(PipeServer))
                        using (StreamWriter writer = new(PipeServer))
                        {
                            string? receivedMessage = await reader.ReadLineAsync();
                            Console.WriteLine($"[Client]: {receivedMessage}");

                            if(!string.IsNullOrEmpty(receivedMessage) && receivedMessage.ToLower().Equals("ping")) 
                                await writer.WriteLineAsync("pong");
                            
                            await writer.WriteLineAsync("Mensagem Received");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"End Connection {ex.Message}");
            }
        }

    }
}
