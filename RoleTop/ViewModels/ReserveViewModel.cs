using System.Collections.Generic;
using RoleTop.Models;

namespace RoleTop.ViewModels
{
    public class PedidoViewModel : BaseViewModel
    {
        public List<Local> Hamburgueres {get;set;}
        public Cliente Cliente {get;set;}
            public string NomeCliente {get;set;}

        public PedidoViewModel() //lista vazia para não qubrar o código
        {   
            this.Hamburgueres = new List<Local>();
            this.Cliente = new Cliente();
            this.NomeCliente = "Jovem";
        }

    }
}