using Engine.Entity;
using Engine.Map;

namespace Engine.Strategy
{
    public interface IActionStrategy
    {
        void Act(Character character, World world);
    }
}