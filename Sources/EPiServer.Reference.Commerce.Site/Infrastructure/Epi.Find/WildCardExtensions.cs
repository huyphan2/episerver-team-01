using System;
using System.Linq.Expressions;
using EPiServer.Find;
using EPiServer.Find.Api.Querying.Queries;

namespace EPiServer.Reference.Commerce.Site.Infrastructure.Epi.Find
{
    public static class WildCardExtensions
    {
        public static ITypeSearch<T> WildcardSearch<T>(this ITypeSearch<T> search,
            string query, Expression<Func<T, string>> fieldSelector, double? boost = null)
        {
             if (string.IsNullOrWhiteSpace(query))
                return search;
            query = query?.ToLowerInvariant();
            query = WrapInAsterisks(query);

            var fieldName = search.Client.Conventions
                .FieldNameConvention
                .GetFieldNameForAnalyzed(fieldSelector);

            var wildcardQuery = new WildcardQuery(fieldName, query)
            {
                Boost = boost
            };

            return new Search<T, WildcardQuery>(search, context =>
            {
                if (context.RequestBody.Query != null)
                {
                    var boolQuery = new BoolQuery();
                    boolQuery.Should.Add(context.RequestBody.Query);
                    boolQuery.Should.Add(wildcardQuery);
                    boolQuery.MinimumNumberShouldMatch = 1;
                    context.RequestBody.Query = boolQuery;
                }
                else
                {
                    context.RequestBody.Query = wildcardQuery;
                }
            });
        }

        public static string WrapInAsterisks(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? "*" : $"*{input.Trim().Trim('*')}*";
        }
    }
}