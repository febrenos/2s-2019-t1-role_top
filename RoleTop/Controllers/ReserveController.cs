using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoleTop.Models;

namespace RoleTop.Controllers
{
    public class ReserveController : Controller
    {
        ClienteRepository clienteRepository = new ClienteRepository ();
        ReserveRepository pedidoRepository = new ReserveRepository ();
        HamburguerRepository hamburguerRepository = new HamburguerRepository ();
        ShakeRepository shakesRepository = new ShakeRepository (); //Shake repositorio em branco tem que ser igual a 
        public IActionResult Index () // colocar o nome do arquivo que está na página 
        {
            PedidoViewModel pvm = new PedidoViewModel ();
            pvm.Hamburgueres = hamburguerRepository.ObterTodos ();

            pvm.Shakes = shakesRepository.ObterTodos (); // o shake repository que esta em verde

            var emailCliente =  ObterUsuarioSession();
            if(!string .IsNullOrEmpty(emailCliente))
            {
                pvm.Cliente = clienteRepository.ObterPor(emailCliente);
            }
            

            var nomeUsuario = ObterUsuarioNomeSession();
            if(!string.IsNullOrEmpty(nomeUsuario))
            {
                pvm.NomeCliente = nomeUsuario;
            }
            pvm.NomeView = "Pedido";
            pvm.UsuarioEmail = ObterUsuarioSession();
            pvm.UsuarioNome = ObterUsuarioNomeSession();
            return View (pvm);
        }

        public object Registrar (IFormCollection form) {
            Shake shake = new Shake ();
            Reserve pedido = new Reserve ();
            var nomeShake = form["shake"];
            shake = new Shake (nomeShake, shakesRepository.ObterPrecoDe (nomeShake));
            shake.Nome = form["shake"];
            shake.preco = shakesRepository.ObterPrecoDe (nomeShake);


            pedido.Shake = shake; //!
            var nomeHamburguer = form["hamburguer"];
            Hamburguer hamburguer = new Hamburguer (nomeHamburguer, hamburguerRepository.ObterPrecoDe (nomeHamburguer));
            hamburguer.Nome = form["hamburguer"];
            hamburguer.preco = 0.0;

            pedido.Hamburguer = hamburguer; //!

            Cliente cliente = new Cliente ();
            cliente.Nome = form["nome"];
            cliente.Endereco = form["endereco"];
            cliente.Telefone = form["telefone"];
            cliente.Email = form["email"];

            pedido.Cliente = cliente; //!

            pedido.DataDoPedido = DateTime.Now; //!Now pega a data e a hora

            pedido.PrecoTotal = hamburguer.preco + shake.preco; //!

            if (pedidoRepository.Inserir (pedido)) {
                return View ("Sucesso", new RespostaViewModel()
                {
                    NomeView = "Sucesso",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioNomeSession()

                });
            } else {
                return View ("Erro",new RespostaViewModel(){
                    NomeView = "Erro",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioNomeSession(),
                });
            }
        }

        public IActionResult Aprovar(ulong id)
        {
            var pedido = pedidoRepository.ObterPor(id);
            pedido.Status = (uint) StatusPedido.APROVADO;

            if(pedidoRepository.Atualizar(pedido))
            {
                return RedirectToAction("Dashboard", "Administrador");
            }
            else
            {
                return View ("Erro", new RespostaViewModel("Não foi possivel aprovar este pedido")
                {
                    NomeView = "Dashboard",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioSession()
                });
            }
        }
        public IActionResult Reprovar(ulong id)
        {
            var pedido = pedidoRepository.ObterPor(id);
            pedido.Status = (uint) StatusPedido.REPROVADO;

            if(pedidoRepository.Atualizar(pedido))
            {
                return RedirectToAction("Dashboard", "Administrador");
            }
            else
            {
                return View ("Erro", new RespostaViewModel("Não foi possivel aprovar este pedido")
                {
                    NomeView = "Dashboard",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioSession()
                });
            }
        }
    }

}
