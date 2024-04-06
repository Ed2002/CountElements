using System.IO.Pipes;

namespace CountElements.Client
{
    public class Connection(string NamePipeline = ".myPipe")
    {
        public async Task Start()
        {
            while (true)
            {
                using (NamedPipeClientStream PipeClient = new(".", NamePipeline, PipeDirection.In))
                {
                    await PipeClient.ConnectAsync();

                    using (StreamReader reader = new(PipeClient))
                    {
                        string? response = await reader.ReadToEndAsync();
                        Console.WriteLine($"[SERVER]: {response}");
                    }
                }
            }
        }
    }
}
