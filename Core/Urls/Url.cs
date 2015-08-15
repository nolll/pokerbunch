﻿namespace Core.Urls
{
    public abstract class Url
    {
        private readonly string _url;

        protected Url(string url)
        {
            _url = !string.IsNullOrEmpty(url) ? string.Concat("/", url) : string.Empty;
        }

        public string Relative
        {
            get { return _url; }
        }

        public string Absolute
        {
            get { return string.Concat("http://pokerbunch.com", _url); }
        }

        public override string ToString()
        {
            return Relative;
        }

        public virtual bool IsEmpty()
        {
            return false;
        }

        public static Url Empty
        {
            get
            {
                return new EmptyUrl();
            }
        }
    }
}