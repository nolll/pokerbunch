namespace Core.Classes
{
	public enum GameStatus
    {
		Created = 0,
		Running = 1,
		Finished = 2,
		Published = 3
	}

    public static class GameStatusName
    {
        public static string GetName(GameStatus status)
        {
			switch(status){
				case GameStatus.Created:
					return "Created";
				case GameStatus.Running:
					return "Running";
				case GameStatus.Finished:
					return "Finished";
                default:
                    return "Published";
			}
		}
    }
}