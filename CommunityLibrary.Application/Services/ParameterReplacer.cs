using System.Linq.Expressions;

namespace CommunityLibrary.Application.Services
{
    internal class ParameterReplacer
    {
        private ParameterExpression parameterExpression;
        private ParameterExpression parameter;

        public ParameterReplacer(ParameterExpression parameterExpression, ParameterExpression parameter)
        {
            this.parameterExpression = parameterExpression;
            this.parameter = parameter;
        }
    }
}