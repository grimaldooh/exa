using exa.Models;
using exa.Models.DB;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace exa.Controllers
{

   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

       // [HttpPost]
        public IActionResult Login()
        {
         
            return View();
        }
        
        public IActionResult Validate(User user)
        {
            

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ValidateUser([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Invalid user data");
            }

            int privatKey = privateKey();
            int publiKey = 10;

            var connectionOptions = new DbContextOptionsBuilder<LoginDbContext>()
                .UseSqlServer(Data.Helpers.Constants.ConnectionString)
                .Options;

            using (var ctx = new LoginDbContext(options: connectionOptions))
            {
                var usuario = ctx.Usuarios.FirstOrDefault(x => x.Name == user.Name);

                if (usuario != null)
                {
                    string pass = Desencriptar(usuario.Password, privatKey, publiKey);
                    if (pass == user.Password)
                    {
                        return Ok(1);
                    }
                    else
                    {
                        return Ok(0);
                    }
                }
            }

            return NotFound("User not found");
        }


        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] User user)
        {
            Console.WriteLine($"Name: {user.Name}, Password: {user.Password}");

            int privatKey = privateKey();
            int publiKey = 10;
            user.Password = Encriptar(user.Password, publiKey, privatKey);
           

            var connectionOptions = new DbContextOptionsBuilder<LoginDbContext>()
               .UseSqlServer(Data.Helpers.Constants.ConnectionString)
               .Options;
            using (var ctx = new LoginDbContext(options: connectionOptions))
            {
                ctx.Usuarios.Add(user);
                ctx.SaveChanges();
                return Ok(user.Id);
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        static int privateKey()
        {
            int privateKey = 3;
            return privateKey;
        }

        static int publicKey(int privateKey)
        {
            int publicKey = 1 + privateKey;
            return publicKey;
        }

        static string Encriptar(string password, int publicKey, int privatekey)
        {
            Console.WriteLine("Public Key: " + publicKey);
            Console.WriteLine("Private Key : " + privatekey);
            Dictionary<char, int> letrasNumerosCrip = new Dictionary<char, int>();
            string abecedario = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ \t\n\r\x0b\x0c";
            for (int i = 0; i < abecedario.Length; i++)
            {
                letrasNumerosCrip.Add(abecedario[i], i + 1);
            }

            List<int> numerosLetrasNombre = new List<int>();
            foreach (char letra in password)
            {
                if (letrasNumerosCrip.ContainsKey(letra))
                {
                    numerosLetrasNombre.Add(letrasNumerosCrip[letra]);
                }
            }

            Console.Write("Nombre convertido a número: ");
            foreach (int numero in numerosLetrasNombre)
            {
                Console.Write(numero + " ");
            }
            Console.WriteLine();

            for (int i = 0; i < numerosLetrasNombre.Count; i++)
            {
                numerosLetrasNombre[i] = numerosLetrasNombre[i] + publicKey + privatekey;
            }

            Console.Write("Mensaje encriptado: ");
            foreach (int numero in numerosLetrasNombre)
            {
                Console.Write(numero + " ");
            }
            Console.WriteLine();

            // Convertir la lista de enteros a una cadena
            string resultado = string.Join(",", numerosLetrasNombre);

            return resultado;
        }

        static string Desencriptar(string numerosStr, int privatKey, int publiKey)
        {
            // Convertir la cadena a una lista de enteros
            List<int> numeros = numerosStr.Split(',').Select(int.Parse).ToList();

            Dictionary<int, char> numerosLetras = new Dictionary<int, char>();
            string abecedario = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ \t\n\r\x0b\x0c";
            for (int i = 0; i < abecedario.Length; i++)
            {
                numerosLetras.Add(i + 1, abecedario[i]);
            }

            List<char> mensajeDesencriptado = new List<char>();
            foreach (int numero in numeros)
            {
                mensajeDesencriptado.Add(numerosLetras[numero - (privatKey + publiKey)]);
            }

            // Convertir la lista de caracteres a una cadena
            string resultado = new string(mensajeDesencriptado.ToArray());

            return resultado;
        }

    }
}