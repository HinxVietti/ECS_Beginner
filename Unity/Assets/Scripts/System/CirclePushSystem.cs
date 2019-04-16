using Assets.Scripts.Components;
using UnityEngine;

namespace Assets.Scripts.System
{
    public class CirclePushSystem : SystemBase
    {
        public CirclePushSystem(GameWorld world) : base(world) { }
        public void Update(PositionComponent pos1, SizeComponent size1, PositionComponent pos2, SizeComponent size2)
        {
            Vector2 center = Vector2.Lerp(pos1.value, pos2.value, size1.value / (size1.value + size2.value));
            Vector2 offest = pos1.value - center;
            float offestSqrMagnitude = offest.sqrMagnitude;
            float sqrRadius = size1.value * size1.value;
            if (offestSqrMagnitude < sqrRadius)
            {
                float offestMagnitude = Mathf.Sqrt(offestSqrMagnitude);
                if (offestMagnitude == 0)
                    offestMagnitude = 0.01f;
                float pushMul = Mathf.Min(size1.value - offestMagnitude, (1 - offestMagnitude / size1.value) * Time.deltaTime * 10f);
                pos1.value += offest.normalized * pushMul;
            }
        }
    }

}
