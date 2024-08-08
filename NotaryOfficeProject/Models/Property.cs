using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NotaryOfficeProject.Models;

[Table("Property")]
public partial class Property
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(14)]
    [Unicode(false)]
    public string OwnerId { get; set; } = null!;

    [StringLength(14)]
    public string Governorate { get; set; } = null!;

    [StringLength(20)]
    public string City { get; set; } = null!;

    [StringLength(20)]
    public string District { get; set; } = null!;

    public int? BuldingNum { get; set; }

    public int? ApartmentNum { get; set; }

    [Column(TypeName = "numeric(12, 2)")]
    public decimal Space { get; set; }

    [StringLength(30)]
    public string NorthernLimit { get; set; } = null!;

    [StringLength(30)]
    public string SouthernLimit { get; set; } = null!;

    [StringLength(30)]
    public string EasternLimit { get; set; } = null!;

    [StringLength(30)]
    public string WasternLimit { get; set; } = null!;

    [InverseProperty("Property")]
    public virtual ICollection<Contract> Contracts { get; } = new List<Contract>();

    [ForeignKey("OwnerId")]
    [InverseProperty("Properties")]
    public virtual Visitor Owner { get; set; } = null!;
}
