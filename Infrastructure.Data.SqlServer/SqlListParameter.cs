using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Data.SqlServer
{
    public class SqlListParameter
    {
        public string ParameterName { get; private set; }
        private readonly IList<int> _idList;

        public SqlListParameter(string parameterName, IList<int> idList)
        {
            ParameterName = parameterName;
            _idList = idList;
        }

        public string ParameterNameList
        {
            get
            {
                var list = new List<string>();
                for (var i = 0; i < _idList.Count; i++)
                {
                    list.Add(GetIdParameterName(i));
                }
                return string.Join(",", list);
            }
        }

        public IList<SqlParameter> ParameterList
        {
            get
            {
                return _idList.Select((t, i) => new SqlParameter(GetIdParameterName(i), t)).ToList();
            }
        }

        private string GetIdParameterName(int index)
        {
            return string.Format("@param{0}", index);
        }
    }
}