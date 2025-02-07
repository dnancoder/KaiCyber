using KaiCyberScanner.Model;

namespace KaiCyberScanner.Processor
{
    public interface IProcessor<T>
    {
        Task<T> ProcessAsync(IModelBase modelBase);
    }
}
