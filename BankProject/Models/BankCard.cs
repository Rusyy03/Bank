using System;
using System.Collections.Generic;

namespace BankProject.Models;

public partial class BankCard
{
    public int Id { get; set; }

    public int? Holder { get; set; }

    public string Number { get; set; } = null!;

    public decimal Balance { get; set; }

    public virtual Customer? HolderNavigation { get; set; }
}
