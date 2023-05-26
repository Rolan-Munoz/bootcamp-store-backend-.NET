using System;
using System.Linq.Expressions;
using bootcamp_store_backend.Application;

namespace bootcamp_store_backend.Infraestructure.Specs
{
	public class Specification<T>
	{
		public List<Criterion> _criteria;

        public Specification(List<Criterion> criteria)
        {
            _criteria = criteria;
        }

        public IQueryable<T> ApplySpecification(IQueryable<T> query)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            foreach ( var criterion in _criteria)
            {
                var criterioFields = criterion.Field.Split('.');
                var property = GetPoperty(parameter, criterioFields);
                var constant = Expression.Constant(Convert.ChangeType(criterion.Value, property.Type));

                Expression? predicate = null;

                switch (criterion.Operator)
                {
                    case "EQUAL":
                        predicate = Expression.Equal(property, constant);
                        break;
                    case "GREATER_THAN":
                        predicate = Expression.GreaterThan(property, constant);
                        break;
                    case "LESS_THAN":
                        predicate = Expression.LessThan(property, constant);
                        break;
                    case "GREATER_THAN_EQUAL":
                        predicate = Expression.GreaterThanOrEqual(property, constant);
                        break;
                    case "LESS_THAN_EQUAL":
                        predicate = Expression.LessThanOrEqual(property, constant);
                        break;
                    case "NOT_EQUAL":
                        predicate = Expression.NotEqual(property, constant);
                        break;
                    case "MATCH":
                        predicate = Expression.Call(ToCaseInsensitiveStringExpression(property), "Contains", null, ToCaseInsensitiveStringExpression(constant));
                        break;
                    case "MATCH_START":
                        predicate = Expression.Call(ToCaseInsensitiveStringExpression(property), "StartsWith", null, ToCaseInsensitiveStringExpression(constant));
                        break;
                    case "MATCH_END":
                        predicate = Expression.Call(ToCaseInsensitiveStringExpression(property), "EndsWith", null, ToCaseInsensitiveStringExpression(constant));
                        break;
                    default:
                        throw new MalformedFilterException();
                }

                if (predicate != null)
                {
                    var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);

                    query = query.Where(lambda);
                }
            }

            return query;
        }

        

        private Expression? ToCaseInsensitiveStringExpression(Expression expression)
        {
            if (expression.Type != typeof(string))
            {
                var toStringMethod = expression.Type.GetMethod("ToLower", Array.Empty<Type>()) ?? throw new MalformedFilterException();
                expression = Expression.Call(expression, toStringMethod);
            }

            var toLowerMethod = typeof(string).GetMethod("ToLower", Array.Empty<Type>()) ?? throw new MalformedFilterException();
            expression = Expression.Call(expression, toLowerMethod);

            return expression;

        }

        private MemberExpression GetPoperty(Expression instance, string[] propertyNames, int index = 0)
        {
            var propertyName = Char.ToUpper(propertyNames[index][0]) + propertyNames[index][1..];
            var property = Expression.Property(instance, propertyName);
            if (index + 1 == propertyNames.Length) {
                return property;
            }

            return GetPoperty(property, propertyNames, index + 1);
        }
    }


}

