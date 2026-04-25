using Handler.Handlers;

namespace Handler.Collections
{
    public class HandlerCollection : IHandlerCollection
    {
        public IDataHandler Data { get; }

        public HandlerCollection(IDataHandler dataHandler)
        {
            Data = dataHandler;
        }
    }
}
