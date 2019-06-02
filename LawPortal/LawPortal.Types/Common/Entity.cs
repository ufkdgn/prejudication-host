using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LawPortal.Types.Common
{
    public class Entity
    {
        [Key]
        public long Id { get; set; }
    }

    public class BasicEntity : Entity
    {
        public string UserName { get; set; }
        public DateTime SystemDate { get; set; }
        public string UpdateUserName { get; set; }
        public DateTime? UpdateSystemDate { get; set; }
    }
}
