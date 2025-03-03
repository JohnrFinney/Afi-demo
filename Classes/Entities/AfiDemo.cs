using System;
using System.Collections.Generic;

namespace afi_demo.Classes.Entities;

public partial class AfiDemo
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string ReferenceNumber { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; }

    public string? EmailAddress { get; set; }
}
