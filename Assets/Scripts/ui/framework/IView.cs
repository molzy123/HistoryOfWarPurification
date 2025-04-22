using game_core;
using ui.framework;

namespace ui.frame
{
    public interface IView
    {
        void initialize();
        
        void show();

        void hide();
    }
}