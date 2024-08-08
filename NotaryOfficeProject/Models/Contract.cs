using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NotaryOfficeProject.Models;

[Table("Contract")]
public partial class Contract
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public byte[] ContractImage { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [Column(TypeName = "money")]
    public decimal MonyAmount { get; set; }

    public bool Type { get; set; }

    [StringLength(14)]
    [Unicode(false)]
    public string CreatorId { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(14)]
    [Unicode(false)]
    public string FirstPartyId { get; set; } = null!;

    [StringLength(14)]
    [Unicode(false)]
    public string SecondPartyId { get; set; } = null!;

    [Column("PropertyID")]
    public int? PropertyId { get; set; }

    [Column("VehicalID")]
    [StringLength(17)]
    [Unicode(false)]
    public string? VehicalId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("ContractCreators")]
    public virtual Visitor Creator { get; set; } = null!;

    [ForeignKey("FirstPartyId")]
    [InverseProperty("ContractFirstParties")]
    public virtual Visitor FirstParty { get; set; } = null!;

    [ForeignKey("PropertyId")]
    [InverseProperty("Contracts")]
    public virtual Property? Property { get; set; }

    [ForeignKey("SecondPartyId")]
    [InverseProperty("ContractSecondParties")]
    public virtual Visitor SecondParty { get; set; } = null!;

    [ForeignKey("VehicalId")]
    [InverseProperty("Contracts")]
    public virtual Vehical? Vehical { get; set; }
}
