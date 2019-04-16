using Assets.Scripts.Components;

namespace Assets.Scripts.Entity
{
    public class Entity : DataBase
    {
        public GameObjectComponent gameObject;
        public PositionComponent position;
        public SizeComponent size;
        public ColorComponent color;
        public TeamComponent team;
        public Entity()
        {
            gameObject = new GameObjectComponent() { entity = this };
            position = new PositionComponent() { entity = this };
            size = new SizeComponent() { entity = this };
            color = new ColorComponent() { entity = this };
            team = new TeamComponent() { entity = this };
        }
    }
}
