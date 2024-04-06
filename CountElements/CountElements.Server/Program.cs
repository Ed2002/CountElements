using CountElements.Server;

Console.WriteLine("Server Start Running");
Connection Connection = new("CE.Pipe");
await Connection.Start();
Console.ReadKey();