using System;
using System.Collections.Generic;

namespace MvcDbApplication.Data.Baraga
{
    public partial class Member
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string MobilePhone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsReceiveEmail { get; set; }
    }
}
