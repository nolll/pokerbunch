using System;

namespace Infrastructure.Data.Classes{

	public class RawCheckpoint{

        public int GameId { get; set; }
        public int PlayerId { get; set; }
	    public int Amount { get; set; }
	    public int Stack { get; set; }
	    public DateTime Timestamp { get; set; }
	    public int Id { get; set; }
        public int Type { get; set; }

	}

}