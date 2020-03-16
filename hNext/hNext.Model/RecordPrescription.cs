using System.ComponentModel.DataAnnotations.Schema;

namespace hNext.Model
{
    public class RecordPrescription
    {
        public long RecordId { get; set;}

        public long PrescriptionId { get; set; }

        [ForeignKey(nameof(RecordId))]
        public virtual Record Record { get; set; }

        [ForeignKey(nameof(PrescriptionId))]
        public virtual Prescription Prescription { get; set; }
    }
}