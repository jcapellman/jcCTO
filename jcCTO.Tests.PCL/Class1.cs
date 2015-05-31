using System.Runtime.Serialization;

namespace jcCTO.Tests.PCL {
    [DataContract]
    public struct UserListingResponseItem {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}