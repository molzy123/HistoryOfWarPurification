using DefaultNamespace;

namespace UI
{
    public interface IBaseLifeCycle
    {
        void initialize();
        
        void start();
        
        void OnDestroy();
    }
}