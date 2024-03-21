using System.Runtime.Serialization;

[DataContract]
[Serializable]
public class Category
{
    [DataMember]
    public int CategoryID { get; set; }
    [DataMember]
    public string CategoryName { get; set; }
}

