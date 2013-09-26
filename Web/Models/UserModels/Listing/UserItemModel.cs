using Core.Classes;
using Web.Models.UrlModels;

namespace Web.Models.UserModels.Listing{
	
    public class UserItemModel{

        public string Name { get; set; }
        public UrlModel UrlModel { get; set; }

		public UserItemModel(User user){
			Name = user.DisplayName;
			UrlModel = new UserDetailsUrlModel(user);
		}

	}

}