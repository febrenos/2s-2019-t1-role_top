using McBonaldsMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace McBonaldsMVC.Controllers
{
    public class PedidosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registrar(IFormCollection form)
        {
            Pedido pedido = new Pedido();

            Shake shake = new Shake();
            shake.Nome = form["shake"];
            shake.Preco = 0.0;

            Hamburguer shake = new Shake();
            hamburguer.Nome = form["hamburguer"];
            hamburguer.Preco = 0.0;
            Hamburguer hamburguer = new Hamburguer(form["hamburguer"],0)

            //Cliente cliente = new Cliente()
            //{ 
            //    Nome = form["nome"],
            //    cliente.Endereco = form["endereco"],
            //    cliente.Telefone = form["telefone"],
            //    cliente.Email = form["email"]
            //};
            //pedido.Shake = shake;
        }
    }
}
