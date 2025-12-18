using Zoologico.ApiConsumer;
using Zoologico.Modelos;

namespace Zoologico.ApiTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Crud<Raza>.UrlBase = "http://10.147.19.119/api/Razas";
            Crud<Especie>.UrlBase = "http://10.147.19.119/api/Especies";
            // insertar una especie
            var nuevaEspecie = new Especie
            {
                Codigo = 0,
                NombreComun = "León"
            };
            var apiResult = Crud<Especie>.Create(nuevaEspecie);
            var especies = Crud<Especie>.ReadAll();

            nuevaEspecie = apiResult.Data;
            nuevaEspecie.NombreComun = "León Modificado";
            Crud<Especie>.Update( nuevaEspecie.Codigo.ToString(), nuevaEspecie);

            var unaEspecie = Crud<Especie>.ReadBy("Codigo", "12");
            Crud<Especie>.Delete("12");

            Console.WriteLine(apiResult);
            Console.ReadLine();
        }
    }
}
