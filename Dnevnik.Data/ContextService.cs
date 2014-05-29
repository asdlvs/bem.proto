namespace Dnevnik.Data
{
    public class ContextService : IContextService
    {
        public Context Context()
        {
            return new Context();
        }
    }
}
