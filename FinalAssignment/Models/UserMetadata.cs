using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace FinalAssignment.Models
{
    public class UserMetadata
    {
        [JsonIgnore, XmlIgnore]
        public string password { get; set; }
        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Comment> Comments { get; set; }
        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Post> Posts { get; set; }
    }
}