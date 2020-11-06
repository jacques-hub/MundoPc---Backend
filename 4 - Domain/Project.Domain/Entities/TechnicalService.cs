namespace Project.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TechnicalService:EntityBase
    {
        public string SerialNumber { get; set; }
        public long UserId { get; set; }
        public long ProductRepairId { get; set; }
        public string Observations { get; set; }
        public string AccessoriesReceived { get; set; }
        public string EquipmentFailure { get; set; }
        public DateTime DateReceived { get; set; }
        public ServiceStatus ServiceStatus { get; set; }
        public DateTime DateStatus { get; set; }
        public decimal TotalInputs { get; set; }//total insumos
        public decimal TotalLabor { get; set; }//mano de obra
        public decimal Total { get; set; }
        public string Diagnostic { get; set; }

        //propiedades de navegacion
        public virtual User User { get; set; }

        public virtual ProductRepair ProductRepair { get; set; }
    }
}
