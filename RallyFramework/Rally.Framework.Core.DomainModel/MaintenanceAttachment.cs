using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class MaintenanceAttachment
    {
        public string ID { get; set; }
        public string OrderID { get; set; }
        public string OrderLineItemID { get; set; }
        public Attachment Attachment { get; set; }
    }
}
