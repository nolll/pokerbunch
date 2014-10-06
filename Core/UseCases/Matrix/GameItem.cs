using Core.Entities;
using Core.Urls;

namespace Core.UseCases.Matrix
{
    public class GameItem
    {
        public Date Date { get; private set; }
        public Url Url { get; private set; }

        public GameItem(Date date, Url url)
        {
            Date = date;
            Url = url;
        }
    }
}