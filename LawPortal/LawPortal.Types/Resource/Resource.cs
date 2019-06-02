using LawPortal.Types.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LawPortal.Types.Resource
{
    public class Resource : Entity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public long ParentResourceId { get; set; }
        public ResourceType ResourceType { get; set; }
        public int Status { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ActionData { get; set; }
        public string DisplayData { get; set; }
        public string RoleSet { get; set; }
        public string IconData { get; set; }

        [NotMapped]
        public Resource ParentResource { get; set; }
    }

    public enum ResourceType
    {
        Root,
        Node
    }
}
