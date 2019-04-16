using UnityEngine;

namespace Assets.Scripts.Components
{
    public class EatingComponent : BaseComponent
    {
        public GameObjectComponent go;
        public PositionComponent target;
        public Vector2 startOffest;
        public Vector2 endOffest;
        public float dur = 0.2f;
        public float endTime;

        //仅操作数据的方法可以存在
        public float GetLifePercent()
        {
            return 1f - (endTime - Time.time) / dur;
        }

        public void Start()
        {
            endTime = Time.time + dur;
        }

        public Vector2 GetCurPosition()
        {
            return target.value + Vector2.Lerp(startOffest, endOffest, GetLifePercent());
        }
    }
}
