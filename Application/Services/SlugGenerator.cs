namespace Application.Services{

	public class SlugGenerator : ISlugGenerator{

		public string GetSlug(string displayName){
			if(displayName == null){
				return null;
			}
		    return displayName.Replace(" ", "").ToLower();
		}

	}

}