using System;
using RoleTop.Enums;

namespace RoleTop.Models {
    public class Reserve 
    { 
        public ulong Id {get;set;}
        public Cliente Cliente { get; set; }

        public Local Local { get; set; }

        public DateTime DataDoPedido { get; set; }
        
        public double PrecoTotal { get; set; }

        public uint Status {get;set;}


        public Reserve()
        {
            this.Cliente = new Cliente();
            this.Local = new Local();
            this.Id = 0;
            this.Status = (uint) StatusPedido.PENDENTE; 
        }
    }
}