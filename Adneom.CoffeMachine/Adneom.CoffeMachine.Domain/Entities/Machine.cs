using System;
using System.Collections.Generic;

namespace Adneom.CoffeMachine.Domain.Entities
{
    public partial class Machine
    {
        public Machine()
        {
            ServiceMachine = new HashSet<ServiceMachine>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }

        public virtual ICollection<ServiceMachine> ServiceMachine { get; set; }
    }
}
