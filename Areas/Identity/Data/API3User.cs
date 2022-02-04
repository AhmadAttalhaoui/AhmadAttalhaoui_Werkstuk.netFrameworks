﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API3.Areas.Identity.Data;

// Add profile data for application users by adding properties to the API3User class
public class API3User : IdentityUser
{
    public string Voornaam { get; set; }
    public string Achternaam { get; set; }
}

