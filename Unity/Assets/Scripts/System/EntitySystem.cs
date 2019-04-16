using Assets.Scripts.Entity;
using UnityEngine;

namespace Assets.Scripts.System
{
    using Entity = Entity.Entity;
    public class EntitySystem : SystemBase
    {
        public EntitySystem(GameWorld world) : base(world) { }

        public void AddEntity(Entity e,bool isplayer = false)
        {
            world.entitys.DelayAdd(e);
            world.gameObjectSystem.Add(e.gameObject, e.position, e.size, e.color,isplayer);
        }

        public void RemoveEntity(Entity e)
        {
            world.entitys.DelayRemove(e);
            if (e.gameObject != null)
                world.gameObjectSystem.Remove(e.gameObject);
        }

        public void AddRandomEntity()
        {
            Entity e = new Entity();
            e.size.value = 0.025f;
            e.team.id = 0;
            e.position.value = new Vector2(Random.Range(world.screenRect.xMin + e.size.value, world.screenRect.xMax - e.size.value), Random.Range(world.screenRect.yMin + e.size.value, world.screenRect.yMax - e.size.value));
            AddEntity(e);
        }

        public void AddMoveAbleEnity(MoveAbleEntity e)
        {
            this.AddEntity(e,true);
            world.playerEntitys.Add(e);

            world.moveSystem.Add(e.speed);
            world.gameObjectSystem.SetToTop(e.gameObject);
        }

        public void InitScene()
        {
            for (int i = 0; i < 50; i++)
            {
                AddRandomEntity();
            }
            //仅仅创建一条
            for (int i = 0; i < 1; i++)
            {
                MoveAbleEntity playerEntity = new MoveAbleEntity();
                playerEntity.position.value = Vector2.zero;
                playerEntity.size.value = 0.05f;
                playerEntity.color.value = Color.yellow;
                playerEntity.speed.maxValue = 1f;
                playerEntity.team.id = 1;
                playerEntity.position.value = new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
                AddMoveAbleEnity(playerEntity);
            }
        }
    }
}
