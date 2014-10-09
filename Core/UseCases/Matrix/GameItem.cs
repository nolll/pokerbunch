using Core.Entities;
using Core.Urls;

namespace Core.UseCases.Matrix
{
    public class GameItem
    {
        public int Id { get; private set; }
        public Date Date { get; private set; }
        public Url Url { get; private set; }
        
        public GameItem(int id, Date date, Url url)
        {
            Id = id;
            Date = date;
            Url = url;
        }
    }
}