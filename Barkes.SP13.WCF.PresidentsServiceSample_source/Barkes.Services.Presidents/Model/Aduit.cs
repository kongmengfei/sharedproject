using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Barkes.Services.Presidents.Model
{
    [DataContract]
    public class Aduit
    {
        [DataMember]
        public Guid SiteId { get; set; }

        [DataMember]
        public Guid ItemId { get; set; }

        [DataMember]
        public SPAuditItemType ItemType { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int? AppPrincipalId { get; set; }

        [DataMember]
        public string MachineName { get; set; }

        [DataMember]
        public string MachineIP { get; set; }
        [DataMember]
        public string DocLocation { get; set; }
        [DataMember]
        public SPAuditLocationType LocationType { get; set; }
        [DataMember]
        public DateTime Occurred { get; set; }
        [DataMember]
        public SPAuditEventType Event { get; set; }
        [DataMember]
        public string EventName { get; set; }
        [DataMember]
        public SPAuditEventSource EventSource { get; set; }
        [DataMember]
        public string SourceName { get; set; }
        [DataMember]
        public string EventData { get; set; }
    }
}
