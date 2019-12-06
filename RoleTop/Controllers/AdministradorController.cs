using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoleTop.Models;

namespace RoleTop.Controllers
{
    public class AdministradorController : Controller
    {
        ReserveRepository ReserveRepository = new ReserveRepository ();
        public IActionResult Dashboard () 
        {

            var ninguemLogado = string.IsNullOrEmpty(ObterUsuarioTipoSession());

            if (!ninguemLogado && (uint) TipoUsuario.ADMINISTRADOR == uint.Parse(ObterUsuarioTipoSession())) // isso ! inverte !nimguem = alguem //salva em uma variavel se der certo
            { // && = e ou || = ou

            var pedidos = pedidoRepository.ObterTodos ();
            DashboardViewModel dashboardViewModel = new DashboardViewModel ();

                foreach (var pedido in pedidos) {
                    switch (pedido.Status) {
                        case (uint) StatusPedido.APROVADO:
                            dashboardViewModel.PedidosAprovados++;
                            break;
                        case (uint) StatusPedido.REPROVADO:
                            dashboardViewModel.PedidosReprovados++;
                            break;

                        default:
                            dashboardViewModel.PedidosPendentes++;
                            dashboardViewModel.Pedidos.Add (pedido);
                            break;

                    }
                }
                dashboardViewModel.NomeView = "Dashboard";
                dashboardViewModel.UsuarioEmail = ObterUsuarioSession();

                return View (dashboardViewModel);
            }
            else{
                return View ("Erro", new RespostaViewModel(){
                    NomeView ="Dashboard",
                    Mensagem = "Você não tem permissão para acessar o Dashboard"
                });
            }

        }
    }
}
