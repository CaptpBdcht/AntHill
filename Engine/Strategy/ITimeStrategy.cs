using Engine.Map;

namespace Engine.Strategy
{
    public interface ITimeStrategy
    {
        void Endure(Entity.Entity entity, World world);
    }
}