using System;
using System.Collections.Generic;

namespace BankProject.Models;

public partial class Loan
{
    public int Id { get; set; }

    public int? Holder { get; set; }

    public decimal Amount { get; set; }

    public int Term { get; set; }

    public virtual Customer? HolderNavigation { get; set; }
}
