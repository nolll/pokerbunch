using System.Globalization;

namespace Core.Entities
{
    public class Identifier
    {
        private readonly int _id;

        public Identifier(int id)
        {
            _id = id;
        }

        public int ToInt()
        {
            return _id;
        }

        public override string ToString()
        {
            return _id.ToString(CultureInfo.InvariantCulture);
        }

        public bool Equals(Identifier other)
        {
            return other.ToInt() == _id;
        }

    }
}
