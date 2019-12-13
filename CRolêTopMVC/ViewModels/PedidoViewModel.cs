using System.Collections.Generic;
using RolêTopMVC.Models;

namespace RolêTopMVC.ViewModels
{
    public class PedidoViewModel : BaseViewModel
    {
        public List<Hamburguer> Hamburgueres {get;set;} 
        public List<Shake> Shakes {get;set;}

        public Cliente Cliente {get;set;} 
        public string NomeCliente {get;set;} 

        public PedidoViewModel()
        {
            this.Hamburgueres = new List<Hamburguer>();
            this.Shakes = new List<Shake>();
            this.Cliente = new Cliente();
            this.NomeCliente = "";
        }

    }
}