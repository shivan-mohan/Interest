﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestIdentityServer
{
    using System;
    using System.Collections.Generic;
    using EFTours.Utilities.Encryption;

    public partial class IdentityClient
    {
        public string ClientSecretDecrypted
        {
            get
            {
                return Encryption.Decrypt(this.ClientSecret);
            }
        }
    }
}