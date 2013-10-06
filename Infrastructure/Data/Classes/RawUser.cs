using Core.Classes;

namespace Infrastructure.Data.Classes {

	public class RawUser{

	    public int Id { get; set; }
	    public string UserName { get; set; }
	    public string DisplayName { get; set; }
	    public string RealName { get; set; }
	    public string Email { get; set; }
	    public int GlobalRole { get; set; }

	    public RawUser()
	    {
	        GlobalRole = -1;
	    }
	}

}