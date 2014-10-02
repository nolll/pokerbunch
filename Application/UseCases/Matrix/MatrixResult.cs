using System.Collections.Generic;

namespace Application.UseCases.Matrix
{
    public class MatrixResult
    {
        public IList<GameItem> GameItems { get; private set; }

        public MatrixResult(IList<GameItem> gameItems)
        {
            GameItems = gameItems;
        }
    }
}