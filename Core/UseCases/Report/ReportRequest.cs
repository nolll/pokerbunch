using System.ComponentModel.DataAnnotations;

namespace Core.UseCases.Report
{
    public class ReportRequest
    {
        public string Slug { get; private set; }
        public int PlayerId { get; private set; }
        [Range(0, int.MaxValue, ErrorMessage = "Stack can't be negative")]
        public int Stack { get; private set; }

        public ReportRequest(string slug, int playerId, int stack)
        {
            Slug = slug;
            PlayerId = playerId;
            Stack = stack;
        }
    }
}