using System.Collections.Generic;
using Application.Urls;
using Core.Entities;

namespace Application.UseCases.Matrix
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
        public int Rank { get; set; }
        public string Name { get; set; }
        public Url PlayerUrl { get; set; }
        public IList<MatrixResultItem> CellModels { get; set; }
        public Money TotalResult { get; set; }

        public MatrixPlayerItem(int rank, string name, Url playerUrl, IList<MatrixResultItem> cellModels, Money totalResult)
        {
            Rank = rank;
            Name = name;
            PlayerUrl = playerUrl;
            CellModels = cellModels;
            TotalResult = totalResult;
        }
    }

    public class MatrixResultItem
    {
    }
}