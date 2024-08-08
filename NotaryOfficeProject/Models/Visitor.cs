using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace NotaryOfficeProject.Models;

[Table("Visitor")]
[Index("FactoryNum", IsUnique = true)]
public partial class Visitor : IdentityUser
{
    [Key]
    [Column("ID")]
    [StringLength(14)]
    [Unicode(false)]
    public override string Id { get; set; } = null!;

    [StringLength(9)]
    [Unicode(false)]
    public string? FactoryNum { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(20)]
    public string? MomName { get; set; }

    [StringLength(14)]
    public string? Governorate { get; set; }

    [StringLength(50)]
    public string? Address { get; set; }

    [StringLength(14)]
    public string? Nationality { get; set; }

    [StringLength(9)]
    public string? Religon { get; set; }

    [InverseProperty("Creator")]
    public virtual ICollection<Contract> ContractCreators { get; } = new List<Contract>();

    [InverseProperty("FirstParty")]
    public virtual ICollection<Contract> ContractFirstParties { get; } = new List<Contract>();

    [InverseProperty("SecondParty")]
    public virtual ICollection<Contract> ContractSecondParties { get; } = new List<Contract>();

    [InverseProperty("Owner")]
    public virtual ICollection<Property> Properties { get; } = new List<Property>();

    [InverseProperty("Owner")]
    public virtual ICollection<Vehical> Vehicals { get; } = new List<Vehical>();
}
