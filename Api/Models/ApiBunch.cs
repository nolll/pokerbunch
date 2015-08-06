using System.Runtime.Serialization;

namespace Api.Models
{
    [DataContract(Namespace = "", Name = "bunch")]
    public class ApiBunch
    {
        [DataMember(Name = "slug")]
        public string Slug { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }

        public ApiBunch(string slug, string name)
        {
            Slug = slug;
            Name = name;
        }

        public ApiBunch()
        {
        }
    }

    [DataContract(Namespace = "", Name = "player")]
    public class ApiPlayer
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        public ApiPlayer(string name)
        {
            Name = name;
        }

        public ApiPlayer()
        {
        }
    }
}