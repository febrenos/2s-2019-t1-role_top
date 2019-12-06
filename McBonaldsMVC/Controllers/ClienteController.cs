using System;
using McBonaldsMVC.Enums;
using McBonaldsMVC.Repositories;
using McBonaldsMVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace McBonaldsMVC.Controllers {
    public class ClienteController : AbstractController {

        private ClienteRepository clienteRepository = new ClienteRepository ();
        PedidoRepository pedidoRepository = new PedidoRepository ();
        [HttpGet]
        public IActionResult Login () {
            {
                return View (new BaseViewModel () {
                    NomeView = "Login",
                        UsuarioEmail = ObterUsuarioSession (),
                        UsuarioNome = ObterUsuarioNomeSession ()
                });
            }
        }

        [HttpPost]
        public IActionResult Login (IFormCollection form) {
            ViewData["Action"] = "Login";
            try {
                System.Console.WriteLine ("==================");
                System.Console.WriteLine (form["email"]);
                System.Console.WriteLine (form["senha"]);
                System.Console.WriteLine ("==================");

                var usuario = form["email"];
                var senha = form["senha"];

                var cliente = clienteRepository.ObterPor (usuario);

                if (cliente != null) {
                    if (cliente.Senha.Equals (senha)) 
                    {
                        switch(cliente.TipoUsuario)
                        {
                            case (uint) TipoUsuario.CLIENTE:
                            HttpContext.Session.SetString (SESSION_EMAIL_CLIENTE, usuario);
                            HttpContext.Session.SetString (SESSION_NOME_CLIENTE, cliente.Nome);
                            HttpContext.Session.SetString (SESSION_TIPO_CLIENTE, cliente.TipoUsuario.ToString());

                            return RedirectToAction ("Historico", "Cliente");

                            default:
                            HttpContext.Session.SetString (SESSION_EMAIL_CLIENTE, usuario);
                            HttpContext.Session.SetString (SESSION_NOME_CLIENTE, cliente.Nome);
                            HttpContext.Session.SetString (SESSION_TIPO_CLIENTE, cliente.TipoUsuario.ToString());
                            
                            return RedirectToAction ("Historico", "Adiministrador");

                        }
                        
                        
                    } else {

                        return View ("Erro", new RespostaViewModel ("Senha incorreta"));
                    }

                } else {

                    return View ("Erro", new RespostaViewModel ($"Usuário {usuario} não encontrado") {
                        NomeView = "Erro",

                    });
                }

            } catch (Exception e) {
                System.Console.WriteLine (e.StackTrace);
                return View ("Erro");
            }
        }
        public IActionResult Historico () {
            var emailCliente = HttpContext.Session.GetString (SESSION_EMAIL_CLIENTE);
            var pedidosCliente = pedidoRepository.ObterTodosPorCliente (emailCliente);

            return View (new HistoricoViewModel () {
                Pedidos = pedidosCliente,
                NomeView = "Historico",
                UsuarioNome =  ObterUsuarioNomeSession(),
                UsuarioEmail = ObterUsuarioSession(),
            });
        
        }

        public IActionResult Logoff(){
            HttpContext.Session.Remove(SESSION_EMAIL_CLIENTE);
            HttpContext.Session.Remove(SESSION_NOME_CLIENTE);
            HttpContext.Session.Clear();
            return RedirectToAction ("Index", "Home");
        }
    }
}