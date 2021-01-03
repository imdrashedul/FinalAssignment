using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace FinalAssignment.Models
{
    public class PostMetadata
    {
        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}