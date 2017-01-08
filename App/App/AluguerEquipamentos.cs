namespace App
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AluguerEquipamentos
    {
        [Column(TypeName = "smallmoney")]
        public decimal? preco { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumAluguer { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodEquip { get; set; }

        public virtual EquipamentosTab Equipamentos { get; set; }

        public virtual AluguerTab Aluguer { get; set; }
    }
}
