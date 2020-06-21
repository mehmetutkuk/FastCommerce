using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Entities.Entities
{
    public class RoleObject
    {
        public int RoleObjectID { get; set; }
        public string ObjectName { get; set; }
        public int Read { get; set; }
        public int Insert { get; set; }
        public int Update { get; set; }
        public int Delete { get; set; }

    }
}
