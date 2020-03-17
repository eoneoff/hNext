using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hNext.Model
{
    public class DrugComponent
    {
        public int DrugId { get; set;}
        public int DrugDosageId { get; set; }

        [ForeignKey(nameof(DrugId))]
        public virtual Drug Drug {get; set;}

        [ForeignKey(nameof(DrugDosageId))]
        public virtual DrugDosage DrugDosage {get; set; }
    }
}