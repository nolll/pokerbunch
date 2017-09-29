namespace Web.Components.ApiDocsModels.ParameterBlock
{
    public class ParameterModel
    {
        public string Name { get; }
        public string Type { get; }
        public string Description { get; }

        public ParameterModel(string name, ParameterTypeModel type, string description)
        {
            Name = name;
            Type = type.Name;
            Description = description;
        }
    }
}