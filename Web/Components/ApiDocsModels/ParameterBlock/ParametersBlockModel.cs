using Web.Components.ApiDocsModels.CodeBlock;

namespace Web.Components.ApiDocsModels.ParameterBlock
{
    public class ParametersBlockModel : CodeBlockModel
    {
        public ParameterModel[] Parameters { get; }

        public ParametersBlockModel(params ParameterModel[] parameters)
        {
            Parameters = parameters;
        }
    }
}