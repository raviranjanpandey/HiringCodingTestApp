using MediatR;

namespace HiringCodingTestApis.Core
{
    public class Query<T> : IRequest<T> where T : class
    {
        private const int DEFAULT_TAKE = 10;
        private const int DEFAULT_SKIP = 0;

        public int? Take { get; set; } //How many items are we going to return (Take)
        public int? Skip { get; set; } //How many items are we going to skip (Skip)

        public Query(int? take, int? skip)
        {
            Take = take == 0 ? DEFAULT_TAKE : take ?? DEFAULT_TAKE;
            Skip = skip ?? DEFAULT_SKIP;
        }
    }
}