﻿using System;
using System.Collections.Generic;

namespace Ecommerce;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Password { get; set; }
}
