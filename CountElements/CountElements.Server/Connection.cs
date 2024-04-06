using CountElements.Server.Domain;
using System.IO.Pipes;

namespace CountElements.Server
{
    public class Connection(string NamePipeline = ".myPipe")
    {
        public async Task Start()
        {
            try
            {
                CountFoods PipeCountFoods = new();
                CountData PipeCountData = new();

                while (PipeCountData.CountFoods)
                {
                    Thread CountThread = new(() => PipeCountFoods.CountElements(new CountData()));
                    CountThread.Start();

                    using (NamedPipeServerStream PipeServer = new(NamePipeline))
                    {
                        await PipeServer.WaitForConnectionAsync();
                        
                        using (StreamWriter writer = new(PipeServer))
                        {
                            string t = PipeCountFoods.GetCountDataAsString(PipeCountData);
                            await writer.WriteLineAsync(t);
                            await writer.FlushAsync();
                        }

                        Thread.Sleep(2000);
                    }

                    CountThread.Join();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"End Connection {ex.Message}");
            }
        }

    }
}
