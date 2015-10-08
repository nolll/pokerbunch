using System;
using System.Runtime.Serialization;
using Core.UseCases;

namespace Api.Models
{
    [DataContract(Namespace = "", Name = "result")]
    public class ApiRunningResult
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "buyin")]
        public int Buyin { get; set; }
        [DataMember(Name = "stack")]
        public int Stack { get; set; }
        [DataMember(Name = "winnings")]
        public int Winnings { get; set; }
        [DataMember(Name = "lastupdate")]
        public DateTime LastUpdate { get; set; }

        public ApiRunningResult(RunningCashgame.RunningCashgamePlayerItem playerItem)
        {
            Name = playerItem.Name;
            Buyin = playerItem.Buyin;
            Stack = playerItem.Stack;
            Winnings = playerItem.Winnings;
            LastUpdate = playerItem.LastReport;
        }
    }
}