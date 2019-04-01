using System;
using System.Collections.Generic;

namespace Adneom.CoffeMachine.Domain.Entities
{
    public partial class OperateurService
    {
        public OperateurService()
        {
            ServiceMachine = new HashSet<ServiceMachine>();
        }

        public int Id { get; set; }
        public string Operateur { get; set; }

        public virtual ICollection<ServiceMachine> ServiceMachine { get; set; }
    }
}
