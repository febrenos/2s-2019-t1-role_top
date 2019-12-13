using System;
using RolêTopMVC.Enums;

namespace RolêTopMVC.Models
{
    public class Pedido
    {

        public ulong Id {get;set;}
        public Cliente Cliente {get;set;}

        public Local Local {get;set;}

        public Adicional Adicional {get;set;}

        public DateTime DataDoPedido {get;set;}

        public double PrecoTotal {get;set;}

        public uint Status {get;set;}

        public Pedido()
        {
            this.Cliente = new Cliente();
            this.Local = new Local();
            this.Adicional = new Adicional();
            this.Id = 0;
            this.Status = (uint) StatusPedido.PENDENTE;
        }
    }
}