using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Adneom.CoffeMachine.Domain.Entities
{
    public partial class ServiceMachine
    {
        public int Id { get; set; }
        public int? MachineId { get; set; }
        public int TypeBoissonId { get; set; }
        public int? QuantiteSucre { get; set; }
        public int? OperateurId { get; set; }
        [DisplayName("Avec MUG ?")]
        public bool? AvecMug { get; set; } = false;

        public virtual Machine Machine { get; set; }
        public virtual OperateurService Operateur { get; set; }
        public virtual TypeBoisson TypeBoisson { get; set; }
    }
}
