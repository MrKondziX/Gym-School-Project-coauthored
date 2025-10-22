using System;
using System.Collections.Generic;

namespace Projekt_Siłownia;

public partial class User
{
    public int UsersId { get; set; }

    public string UsersName { get; set; } = null!;

    public string UsersSurname { get; set; } = null!;

    public string UsersEmail { get; set; } = null!;

    public string UsersLogin { get; set; } = null!;

    public string UsersPassword { get; set; } = null!;

    public int UsersTypeId { get; set; }
}
