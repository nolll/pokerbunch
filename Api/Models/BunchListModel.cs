using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Api.Models
{
    [CollectionDataContract(Namespace = "", Name = "bunches", ItemName = "bunch")]
    public class BunchListModel : List<BunchModel>
    {
    }
}