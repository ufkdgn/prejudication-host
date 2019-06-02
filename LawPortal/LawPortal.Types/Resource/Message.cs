using LawPortal.Types.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LawPortal.Types.Resource
{
    public class Message : Entity
    {
        public int LanguageId { get; set; }
        public string MessageCode { get; set; }
        public string Description { get; set; }
        public MessageType MessageType { get; set; }

        [NotMapped]
        public Language Language { get; set; }
    }

    public enum MessageType
    {
        Home
    }
}
