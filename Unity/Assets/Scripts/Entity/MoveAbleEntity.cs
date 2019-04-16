using Assets.Scripts.Components;

namespace Assets.Scripts.Entity
{
    public class MoveAbleEntity : Entity
    {
        public SpeedComponent speed;
        public MoveAbleEntity() : base()
        {
            speed = new SpeedComponent() { entity = this };
        }
    }
}
