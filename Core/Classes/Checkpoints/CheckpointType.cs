namespace Core.Classes.Checkpoints
{
	public enum CheckpointType
    {
        Report = 0,
		Buyin = 1,
		Cashout = 2
	}

    public class CheckpointTypeName
    {
        public static string GetName(CheckpointType type)
        {
            switch (type)
            {
                case CheckpointType.Cashout:
                    return "Cashout";
                case CheckpointType.Buyin:
                    return "Buyin";
                default:
                    return "Report";
            }
        }
    }
}