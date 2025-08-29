using DryIoc;

namespace Infrastructure
{
    public interface IDependancyInjectionManager : IDisposable
    {
        void Register<TService, TImplementation>() where TImplementation : TService;
        T Resolve<T>();
    }
    public class DependancyInjectionManager : IDependancyInjectionManager
    {
        private readonly IContainer container;


        public DependancyInjectionManager()
        {
            container = new Container();
        }

        public void Register<TService, TImplementation>() where TImplementation:TService    //Tservice=TInterface
        {
            container.Register<TService, TImplementation>(setup: Setup.With(allowDisposableTransient: true)); //registration
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}
