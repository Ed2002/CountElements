using System.IO.Pipes;

namespace CountElements.Client
{
    public class Connection
    {
        private string NamePipeline = string.Empty;
        public Connection(string _namePipeline = ".myPipe") => NamePipeline = _namePipeline;

        public async Task Start()
        {
            while (true)
            {
                using (NamedPipeClientStream PipeClient = new(".", NamePipeline, PipeDirection.InOut))
                {
                    await PipeClient.ConnectAsync();

                    using (StreamReader reader = new(PipeClient))
                    using (StreamWriter writer = new(PipeClient))
                    {
                        Console.Write("Set e mensagem to send to server: ");
                        string? message = Console.ReadLine();

                        await writer.WriteLineAsync(message);
                        await writer.FlushAsync();

                        string? response = await reader.ReadLineAsync();
                        Console.WriteLine($"[SERVER]: {response}");
                    }
                }
            }
        }
    }
}
