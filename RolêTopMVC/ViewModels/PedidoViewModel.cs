using System.Collections.Generic;
using RolêTopMVC.Models;

namespace RolêTopMVC.ViewModels
{
    public class PedidoViewModel : BaseViewModel
    {
        public List<Local> Hamburgueres {get;set;} 
        public List<Adicional> Shakes {get;set;}

        public Cliente Cliente {get;set;} 
        public string NomeCliente {get;set;} 

        public PedidoViewModel()
        {
            this.Hamburgueres = new List<Local>();
            this.Shakes = new List<Adicional>();
            this.Cliente = new Cliente();
            this.NomeCliente = "";
        }

    }
}