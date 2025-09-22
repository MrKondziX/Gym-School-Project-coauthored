using System;
using System.Collections.Generic;

namespace Projekt_Siłownia;

public partial class UsersType
{
    public int UsersTypeId { get; set; }

    public string UsersTypeName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
