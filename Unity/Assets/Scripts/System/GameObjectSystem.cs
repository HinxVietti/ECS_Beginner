using Assets.Scripts.Components;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts.System
{
    public class GameObjectSystem : SystemBase
    {
        public GameObjectSystem(GameWorld world) : base(world) { }
        public void Add(GameObjectComponent e, PositionComponent position, SizeComponent size, ColorComponent color, bool isplayer = false)
        {
            e.gameObject = new GameObject("Entity");
            e.transform = e.gameObject.transform;
            e.transform.localScale = Vector2.one * 0.001f;
            e.spriteRenderer = e.gameObject.AddComponent<SpriteRenderer>();
            //e.spriteRenderer.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");
            if (isplayer == false) e.spriteRenderer.sprite = Resources.Load<Sprite>("ball");
            else e.spriteRenderer.sprite = Resources.Load<Sprite>("player");
            //e.lineRenderer = e.gameObject.AddComponent<LineRenderer>();
            ////e.lineRenderer.material = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Sprites-Default");
            //e.lineRenderer.material = Resources.Load<Material>("1");
            //e.lineRenderer.useWorldSpace = false;
            //e.lineRenderer.startWidth = 0.1f;
            //e.lineRenderer.endWidth = 0;
            //e.lineRenderer.numCapVertices = 5;
            //e.trailRenderer = e.gameObject.AddComponent<TrailRenderer>();
            //e.trailRenderer.material = Resources.Load<Material>("1");
            //e.trailRenderer.startWidth = 0.1f;
            //e.trailRenderer.endWidth = 0;
            //e.trailRenderer.numCapVertices = 5;
            if (isplayer)
            {
                //添加蛇尾
                //var trail = new GameObject("TRAIL");
                var trail = GameObject.Instantiate(Resources.Load<GameObject>("trail"), e.gameObject.transform);
                trail.transform.localPosition = Vector3.zero;
                //trail.transform.parent = e.gameObject.transform;
            }
            Update(e, position, size, color);
        }

        public void Remove(GameObjectComponent go)
        {
            GameObject.Destroy(go.gameObject);
            go.transform = null;
            go.gameObject = null;
            go.spriteRenderer = null;
        }

        public void Update(GameObjectComponent go, PositionComponent position, SizeComponent size, ColorComponent color)
        {
            go.transform.position = position.value;
            go.transform.localScale = Vector2.one * Mathf.MoveTowards(go.transform.localScale.x, size.value * 11f, Mathf.Max(0.01f, Mathf.Abs(go.transform.localScale.x - size.value)) * 10f * Time.deltaTime);
            go.spriteRenderer.color = color.value;
            //go.trailRenderer.startColor = color.value;
            //go.trailRenderer.endColor = color.value;
        }

        public void SetToTop(GameObjectComponent go)
        {
            go.gameObject.AddComponent<SortingGroup>().sortingOrder = 1;
        }
    }
}
