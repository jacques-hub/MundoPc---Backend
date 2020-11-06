namespace Project.ApiRest
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseUrls("http://*:5000");
                });
    }
}
/*
 * Reemplace RestUrl con una URL que incluya la dirección IP de su máquina 
 * (no localhost o 127.0.0.1, ya que esta dirección se usa desde el emulador 
 * de dispositivo, no desde su máquina). Incluya también el número de puerto (5000). 
 * Para probar que sus servicios funcionan con un dispositivo, asegúrese de no 
 * tener un firewall activo que bloquee el acceso a este puerto.
 */
