﻿namespace Core.Urls
{
    public class EditBunchUrl : SlugUrl
    {
        public EditBunchUrl(string slug)
            : base(Routes.BunchEdit, slug)
        {
        }
    }
}