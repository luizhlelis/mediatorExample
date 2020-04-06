namespace MediatorExample.Domain.Utils
{
    public class QueryLimits
    {
        public int Limit { get; set; }
        public int Page { get; set; }
        public string Order { get; set; }
        public string Orientation { get; set; }

        public QueryLimits() { }

        public QueryLimits(int limit, int page, string order, string orientation)
        {
            Limit = limit;
            Page = page;
            Order = order;
            Orientation = orientation;
        }

        public string ToOrderByClause()
        {
            return string.Format("{0} {1}", Order, Orientation);
        }
    }
}
