using System;

namespace McBonaldsMVC.Models
{
    public class Pedido
    {
        public Cliente cliente {get;set;}
        
        public Hamburguer Hamburguer {get;set;}
        public Shake Shake {get;set;}

        public DateTime DataDoPedido {get;set;}
        
        public double Precototal {get;set;}
    }
}