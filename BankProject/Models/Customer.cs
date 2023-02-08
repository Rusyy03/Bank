using System;
using System.Collections.Generic;

namespace BankProject.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<BankCard> BankCards { get; } = new List<BankCard>();

    public virtual ICollection<Loan> Loans { get; } = new List<Loan>();
}
