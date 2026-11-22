using GetLibraries;

var httpClient = new HttpClient();
var service = new LibraryServicecs(httpClient);
var libraries = await service.GetLibraries();
Console.WriteLine("Retrieved libraries.");
