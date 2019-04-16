using Assets.Scripts.Components;
using UnityEngine;

namespace Assets.Scripts.System
{
    public class EatingSystem : SystemBase
    {
        public EatingSystem(GameWorld world) : base(world) { }
        public void Update(EatingComponent e)
        {
            e.go.transform.position = e.GetCurPosition();
            if (Time.time >= e.endTime)
            {
                world.eatings.DelayRemove(e);
                world.gameObjectSystem.Remove(e.go);
            }
        }

        public void CreateFrom(GameObjectComponent gameObject, PositionComponent source, PositionComponent target)
        {
            gameObject.entity.gameObject = null;//解除和原entity的关系

            EatingComponent comp = new EatingComponent();
            comp.go = gameObject;
            comp.target = target;
            comp.startOffest = source.value - target.value;
            comp.endOffest = Vector2.Lerp(source.value, target.value, 0.5f) - target.value;
            comp.Start();
            world.eatings.DelayAdd(comp);
        }
    }
}
