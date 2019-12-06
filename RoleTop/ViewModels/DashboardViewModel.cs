using System.Collections.Generic;
using RoleTop.Models;

namespace RoleTop.ViewModels {
    public class DashboardViewModel : BaseViewModel {
        public List<Reserve> Reserve { get; set; }
        public uint ReserveAprovados { get; set; }
        public uint ReserveReprovados { get; set; }
        public uint ReservePendentes { get; set; }

        public DashboardViewModel ()
        {
            this.Reserve = new List<Reserve>();
        }
    }
}