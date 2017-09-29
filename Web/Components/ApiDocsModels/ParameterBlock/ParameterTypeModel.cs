namespace Web.Components.ApiDocsModels.ParameterBlock
{
    public class ParameterTypeModel
    {
        public string Name { get; }

        private ParameterTypeModel(string name)
        {
            Name = name;
        }

        public static ParameterTypeModel String => new ParameterTypeModel("string");
        public static ParameterTypeModel Integer => new ParameterTypeModel("integer");
        public static ParameterTypeModel Boolean => new ParameterTypeModel("boolean");
    }
}