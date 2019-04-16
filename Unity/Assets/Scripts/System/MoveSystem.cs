using Assets.Scripts.Components;
using UnityEngine;

namespace Assets.Scripts.System
{
    public class MoveSystem : SystemBase
    {
        public MoveSystem(GameWorld world) : base(world) { }
        public void Add(SpeedComponent speed)
        {
            world.speeds.DelayAdd(speed);
        }

        public void Remove(SpeedComponent speed)
        {
            world.speeds.DelayRemove(speed);
        }

        public void Update(SpeedComponent speed, PositionComponent position, SizeComponent size)
        {
            position.value += speed.value * Time.deltaTime;
            if (position.value.x > world.screenRect.xMax - size.value)
            {
                position.value.x = world.screenRect.xMax - size.value;
                speed.value.x = 0f;
            }
            else if (position.value.x < world.screenRect.xMin + size.value)
            {
                position.value.x = world.screenRect.xMin + size.value;
                speed.value.x = 0f;
            }
            if (position.value.y > world.screenRect.yMax - size.value)
            {
                position.value.y = world.screenRect.yMax - size.value;
                speed.value.y = 0f;
            }
            else if (position.value.y < world.screenRect.yMin + size.value)
            {
                position.value.y = world.screenRect.yMin + size.value;
                speed.value.y = 0f;
            }
        }
    }

}
