using RolêTopMVC.Enums;
using RolêTopMVC.Models;
using RolêTopMVC.Repositories;
using RolêTopMVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace RolêTopMVC.Controllers
{
    public class PedidoController : AbstractController
    {
        ClienteRepository clienteRepository = new ClienteRepository();
        PedidoRepository pedidoRepository = new PedidoRepository();
        HamburguerRepository hamburguerRepository = new HamburguerRepository();
        ShakeRepository shakeRepository = new ShakeRepository();
        public IActionResult Index()
        {
            PedidoViewModel pvm = new PedidoViewModel();
            pvm.Hamburgueres = hamburguerRepository.ObterTodos();
            pvm.Shakes = shakeRepository.ObterTodos();

            var emailCliente = ObterUsuarioSession();
            if(!string.IsNullOrEmpty(emailCliente))
            {
                pvm.Cliente = clienteRepository.ObterPor(emailCliente);
            }

            var nomeUsuario = ObterUsuarioNomeSession();
            if(!string.IsNullOrEmpty(nomeUsuario))
            {
                pvm.NomeCliente = nomeUsuario;
            }
                pvm.NomeView = "Pedido";
                pvm.UsuarioEmail = emailCliente;
                pvm.UsuarioNome = nomeUsuario;

                return View(pvm);

        }

        public IActionResult Registrar(IFormCollection form){
            ViewData["Action"]= "Pedido";
            Pedido pedido = new Pedido();

            var nomeShake = form["shake"];
            Shake shake = new Shake (nomeShake, shakeRepository.ObterPrecoDe(nomeShake));
            pedido.Shake = shake;

            
            var nomeHamburguer = form["hamburguer"];
            Hamburguer hamburguer = new Hamburguer(nomeHamburguer, hamburguerRepository.ObterPrecoDe(nomeHamburguer));
            pedido.Hamburguer = hamburguer;
            

            Cliente cliente = new Cliente();
            cliente.Nome = form["nome"];
            cliente.Endereco = form["endereco"];
            cliente.Telefone = form["telefone"];
            cliente.Email = form["email"];

            pedido.Cliente = cliente;
            pedido.DataDoPedido = DateTime.Now;
            pedido.PrecoTotal = hamburguer.Preco + shake.Preco;

            if(pedidoRepository.Inserir(pedido))
            {
                return View ("Sucesso", new RespostaViewModel()
                {
                    NomeView = "Pedido",
                    Mensagem = $"{cliente.Nome} seu pedido foi concluído!"
                });
                
            } else {
                return View ("Erro", new RespostaViewModel()
                {
                    NomeView = "Pedido",
                    Mensagem = $"{cliente.Nome} seu pedido não foi concluído!"
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
            else {
                return View("Erro", new RespostaViewModel("Não foi possível aprovar este pedido")
                {
                    NomeView = "Dashboard",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioNomeSession()                   
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
            else {
                return View("Erro", new RespostaViewModel("Não foi possível reprovar este pedido")
                {
                    NomeView = "Dashboard",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioNomeSession()                   
                 });
                
            }

        }
    
    }
}