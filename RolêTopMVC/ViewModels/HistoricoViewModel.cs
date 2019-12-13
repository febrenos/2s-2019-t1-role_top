using System.Collections.Generic;
using RolêTopMVC.Models;

namespace RolêTopMVC.ViewModels
{
    public class HistoricoViewModel : BaseViewModel
    {
        public List<Pedido> Pedidos {get;set;}
        
    }
}