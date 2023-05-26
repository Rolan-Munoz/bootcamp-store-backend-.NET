using System;
using bootcamp_store_backend.Application;

namespace bootcamp_store_backend.Infraestructure.Specs
{
	public class SpecificationParser<T>:ISpecificationParser<T>
		where T : class
	{
        public Specification<T> ParseSpecification(string filter)
        {
            List<Criterion> criteria = new List<Criterion>();

            var criteriaString = filter.Split(',');
            foreach (var criterionString in criteriaString)
            {
                var parts = criterionString.Split(':');
                if (parts.Length != 3)
                {
                    throw new MalformedFilterException();
                }

                var criterion = new Criterion
                {
                    Field = Char.ToUpper(parts[0][0]) + parts[0][1..],
                    Operator = parts[1].ToUpper(),
                    Value = parts[2]
                };

                criteria.Add(criterion);
            }

            return new Specification<T>(criteria);
        }

       
	}
}

 