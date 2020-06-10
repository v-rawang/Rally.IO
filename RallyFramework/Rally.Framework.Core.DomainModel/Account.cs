using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class Account
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }

        public int? Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Title { get; set; }

        public string SID { get; set; }

        public string Alias { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string Mobile { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string QQNumber { get; set; }

        public string WeChatNumber { get; set; }

        public string SinaWeibo { get; set; }

        public string SystemAccount { get; set; }

        public string BloodType { get; set; }

        public string Constellation { get; set; }

        public string Hobby { get; set; }

        public string Education { get; set; }

        public string PoliticsStatus{ get; set; }

        public string Industry { get; set; }

        public Attachment HeadImage { get; set; }

        public string Organization { get; set; }

        public string Department { get; set; }

        public string WorkGroup { get; set; }

        public string Position { get; set; }

        public string Headline { get; set; }

        public string Biography { get; set; }

        public string Description { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<string, object> ExternalAccounts { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<string, object> DynamicProperties { get; set; }
    }
}
