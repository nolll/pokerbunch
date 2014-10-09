using System.Collections.Generic;
using Core.Entities;
using Core.Urls;

namespace Core.UseCases.Matrix
{
    public class MatrixResult
    {
        public IList<GameItem> GameItems { get; private set; }
        public IList<MatrixPlayerItem> PlayerItems { get; private set; }
        public bool SpansMultipleYears { get; private set; }

        public MatrixResult(IList<GameItem> gameItems, IList<MatrixPlayerItem> playerItems, bool spansMultipleYears)
        {
            GameItems = gameItems;
            PlayerItems = playerItems;
            SpansMultipleYears = spansMultipleYears;
        }
    }

    public class MatrixPlayerItem
    {
        public int Rank { get; private set; }
        public string Name { get; private set; }
        public Url PlayerUrl { get; private set; }
        public IDictionary<int, MatrixResultItem> ResultItems { get; private set; }
        public Money TotalResult { get; private set; }

        public MatrixPlayerItem(int rank, string name, Url playerUrl, IDictionary<int, MatrixResultItem> resultItems, Money totalResult)
        {
            Rank = rank;
            Name = name;
            PlayerUrl = playerUrl;
            ResultItems = resultItems;
            TotalResult = totalResult;
        }
    }

    public class MatrixResultItem
    {
        public Money Buyin { get; private set; }
        public Money Cashout { get; private set; }
        public Money Winnings { get; private set; }
        public bool HasBestResult { get; private set; }
        public bool HasTransactions { get; private set; }

        public MatrixResultItem(Money buyin, Money cashout, Money winnings, bool hasBestResult, bool hasTransactions)
        {
            Buyin = buyin;
            Cashout = cashout;
            Winnings = winnings;
            HasBestResult = hasBestResult;
            HasTransactions = hasTransactions;
        }
    }
}