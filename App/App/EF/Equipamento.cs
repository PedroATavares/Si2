//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Equipamento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Equipamento()
        {
            this.AluguerEquipamentos = new HashSet<AluguerEquipamento>();
            this.PrecoAluguers = new HashSet<PrecoAluguer>();
        }
    
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public Nullable<int> Removido { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AluguerEquipamento> AluguerEquipamentos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrecoAluguer> PrecoAluguers { get; set; }
        public virtual Tipo Tipo1 { get; set; }
    }
}