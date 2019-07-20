namespace Balakin.VSOutputEnhancer.Logic
{
    public interface IDispatcher
    {
        void Dispatch(IEvent @event, DataContainer data);
    }
}