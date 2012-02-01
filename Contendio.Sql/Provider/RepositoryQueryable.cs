using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Contendio.Sql.Provider
{
    public class RepositoryQueryable<TSource> : IOrderedQueryable<TSource>
    {
        public string Workspace { get; set; }
        public Expression Expression { get; private set; }
        public IQueryProvider Provider { get; private set; }

        public RepositoryQueryable(string workspace, IQueryProvider provider, IQueryable<TSource> innerSource)
        {
            this.Workspace = workspace;
            this.Provider = provider;
            this.Expression = Expression.Constant(innerSource);
        }

         public RepositoryQueryable(string workspace, IQueryProvider provider, Expression expression)
         {
            this.Workspace = workspace;
            this.Provider = provider;
            this.Expression = expression;
        }

        public IEnumerator<TSource> GetEnumerator()
        {
            return this.Provider.Execute<IEnumerable<TSource>>(this.Expression).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public Type ElementType
        {
            get { return typeof(TSource); }
        }
    }
}
