using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommunityLibrary.Application.Pagination
{
    public static class ExpressionMapper
    {
        /// <summary>
        /// Mapeia um predicado do tipo fonte (TSource) para o tipo destino (TDestination).
        /// </summary>
        /// <typeparam name="TSource">Tipo fonte da expressão.</typeparam>
        /// <typeparam name="TDestination">Tipo destino da expressão.</typeparam>
        /// <param name="sourcePredicate">Predicado do tipo fonte.</param>
        /// <returns>Predicado convertido para o tipo destino.</returns>
        public static Expression<Func<TDestination, bool>> MapPredicate<TSource, TDestination>(
            Expression<Func<TSource, bool>> sourcePredicate)
        {
            // Cria um parâmetro para o tipo destino
            var parameter = Expression.Parameter(typeof(TDestination), "destination");

            // Substitui as propriedades do tipo fonte para o tipo destino
            var body = new ParameterReplacerVisitor<TSource, TDestination>(parameter)
                .Visit(sourcePredicate.Body);

            // Retorna uma nova expressão para o tipo destino
            return Expression.Lambda<Func<TDestination, bool>>(body, parameter);
        }

        /// <summary>
        /// Visitor para substituir os parâmetros na expressão.
        /// </summary>
        private class ParameterReplacerVisitor<TSource, TDestination> : ExpressionVisitor
        {
            private readonly ParameterExpression _destinationParameter;

            public ParameterReplacerVisitor(ParameterExpression destinationParameter)
            {
                _destinationParameter = destinationParameter;
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                // Substitui o membro do tipo fonte pelo membro correspondente no tipo destino
                if (node.Member.DeclaringType == typeof(TSource))
                {
                    var destinationMember = typeof(TDestination).GetProperty(node.Member.Name);
                    if (destinationMember != null)
                    {
                        return Expression.Property(_destinationParameter, destinationMember);
                    }
                }

                return base.VisitMember(node);
            }
        }
    }
}