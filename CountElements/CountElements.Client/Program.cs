using CountElements.Client;

Console.WriteLine("Client Start Running");
Connection Connection = new("CE.Pipe");
await Connection.Start();
Console.ReadKey();