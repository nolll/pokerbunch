using Core.Entities;
using Core.Urls;
using Core.UseCases.AppContext;

namespace Core.UseCases.BunchContext
{
    public class BunchContextResult
    {
        private readonly Role _userRole;
        private readonly int _userPlayerId;
        
        public string BunchName { get; private set; }
        public int BunchId { get; private set; }
        public bool HasBunch { get; private set; }
        public Url BunchUrl { get; private set; }
        public Url CashgameUrl { get; private set; }
        public Url PlayerUrl { get; private set; }
        public Url EventUrl { get; private set; }
        public AppContextResult AppContext { get; private set; }

        public BunchContextResult(AppContextResult appContextResult)
        {
            AppContext = appContextResult;
        }

        public BunchContextResult(AppContextResult appContextResult, string slug, int bunchId, string bunchName, Role userRole, int userPlayerId)
            : this(appContextResult)
        {
            _userRole = userRole;
            _userPlayerId = userPlayerId;
            
            BunchId = bunchId;
            BunchName = bunchName;
            HasBunch = true;
            BunchUrl = new BunchDetailsUrl(slug);
            CashgameUrl = new CashgameIndexUrl(slug);
            PlayerUrl = new PlayerIndexUrl(slug);
            EventUrl = new EventListUrl(slug);
        }

        public bool IsAdmin
        {
            get { return IsInRole(Role.Admin); }
        }

        public bool IsManager
        {
            get { return IsInRole(Role.Manager); }
        }

        public bool IsPlayer
        {
            get { return IsInRole(Role.Player); }
        }

        public bool IsCurrentPlayer(int playerId)
        {
            return IsAdmin || playerId == _userPlayerId;
        }

        private bool IsInRole(Role requestedRole)
        {
            return requestedRole <= _userRole;
        }
    }
}