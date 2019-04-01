using System;
using System.Collections.Generic;

namespace Adneom.CoffeMachine.Domain.Entities
{
    public partial class TypeBoisson
    {
        public TypeBoisson()
        {
            ServiceMachine = new HashSet<ServiceMachine>();
        }

        public int Id { get; set; }
        public string TypeBoisson1 { get; set; }

        public virtual ICollection<ServiceMachine> ServiceMachine { get; set; }
    }
}
