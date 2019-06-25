namespace Balakin.VSOutputEnhancer
{
    public interface IDispatcher
    {
        void Dispatch(IEvent @event);
    }
}