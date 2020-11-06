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
 * Reemplace RestUrl con una URL que incluya la direcci�n IP de su m�quina 
 * (no localhost o 127.0.0.1, ya que esta direcci�n se usa desde el emulador 
 * de dispositivo, no desde su m�quina). Incluya tambi�n el n�mero de puerto (5000). 
 * Para probar que sus servicios funcionan con un dispositivo, aseg�rese de no 
 * tener un firewall activo que bloquee el acceso a este puerto.
 */
