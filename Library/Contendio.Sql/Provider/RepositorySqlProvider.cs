using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Linq.SqlClient;
using Contendio.Model;
using System.Data.Common;
using System.Collections;
using System.Text.RegularExpressions;

namespace Contendio.Sql.Provider
{
    class RepositorySqlProvider : IQueryProvider
    {
        public string Workspace { get; private set; }

        private readonly Func<IQueryable, DbCommand> translator;
        private readonly Func<Type, string, object[], IEnumerable> executor;

        public RepositorySqlProvider(string workspace, Func<IQueryable, DbCommand> translator, Func<Type, string, object[], IEnumerable> executor)
        {
            this.Workspace = workspace;
            this.translator = translator;
            this.executor = executor;
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new RepositoryQueryable<TElement>(Workspace, this, expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            bool isCollection = typeof(TResult).IsGenericType && typeof(TResult).GetGenericTypeDefinition() == typeof(IEnumerable<>);
            Type itemType = isCollection ? typeof(TResult).GetGenericArguments().Single() : typeof(TResult);
            IQueryable queryable = Activator.CreateInstance(
                typeof(RepositoryQueryable<>).MakeGenericType(itemType), Workspace, this, expression) as IQueryable;

            IEnumerable queryResult;

            // Translates LINQ query to SQL.
            using (DbCommand command = this.translator(queryable))
            {
                string commandText = command.CommandText;
                commandText = commandText.Replace("replaceme_", Workspace + "_");
                
                // Executes the transalted SQL.
                queryResult = this.executor(
                    itemType,
                    commandText,
                    command.Parameters.OfType<DbParameter>()
                                      .Select(parameter => parameter.Value)
                                      .ToArray());
            }

            if (isCollection)
                return (TResult)queryResult;
            else
            {
                var tmp = queryResult.OfType<TResult>();
                return tmp.SingleOrDefault();
            }
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}
