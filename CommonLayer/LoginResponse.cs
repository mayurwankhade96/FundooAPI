﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class LoginResponse
    {        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
