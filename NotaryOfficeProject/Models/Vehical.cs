using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NotaryOfficeProject.Models;

[Table("Vehical")]
public partial class Vehical
{
    [Key]
    [Column("VIN")]
    [StringLength(17)]
    [Unicode(false)]
    public string Vin { get; set; } = null!;

    [StringLength(14)]
    [Unicode(false)]
    public string OwnerId { get; set; } = null!;

    [StringLength(17)]
    [Unicode(false)]
    public string? LicenseNum { get; set; }

    [StringLength(17)]
    [Unicode(false)]
    public string? LicenseEndDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Brand { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string Engine { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string Color { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string Modle { get; set; } = null!;

    [InverseProperty("Vehical")]
    public virtual ICollection<Contract> Contracts { get; } = new List<Contract>();

    [ForeignKey("OwnerId")]
    [InverseProperty("Vehicals")]
    public virtual Visitor Owner { get; set; } = null!;
}
