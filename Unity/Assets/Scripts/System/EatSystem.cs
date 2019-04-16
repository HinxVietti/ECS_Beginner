using Assets.Scripts.Components;
using UnityEngine;

namespace Assets.Scripts.System
{
    public class EatSystem : SystemBase
    {
        public EatSystem(GameWorld world) : base(world) { }
        public void Update(PositionComponent sourcePosition, SizeComponent sourceSize, PositionComponent targetPosition, SizeComponent targetSize, Entity.Entity target)
        {
            float sizeSum = sourceSize.value + targetSize.value + 0.05f;
            if ((sourcePosition.value - target.position.value).sqrMagnitude < sizeSum * sizeSum)
            {
                //sourceSize.value = Mathf.Sqrt(sourceSize.value * sourceSize.value + targetSize.value * targetSize.value);
                Kill(target, sourcePosition);
            }
        }
        public void Kill(Entity.Entity food, PositionComponent sourcePosition)
        {
            world.eatingSystem.CreateFrom(food.gameObject, food.position, sourcePosition);

            world.entitySystem.RemoveEntity(food);
            world.entitySystem.AddRandomEntity();
        }
    }
}
