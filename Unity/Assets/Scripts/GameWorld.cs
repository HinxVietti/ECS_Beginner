using Assets.Scripts.Base;
using Assets.Scripts.Components;
using Assets.Scripts.Entity;
using Assets.Scripts.System;
using UnityEngine;
public class GameWorld : MonoBehaviour
{

    public DataArray<Entity> entitys;
    public DataArray<SpeedComponent> speeds;
    public DataArray<MoveAbleEntity> playerEntitys;
    public DataArray<EatingComponent> eatings;

    public EntitySystem entitySystem;
    public MoveSystem moveSystem;
    public GameObjectSystem gameObjectSystem;
    public InputSystem inputSystem;
    public CirclePushSystem circlePushSystem;
    public EatSystem eatSystem;
    public EatingSystem eatingSystem;

    public Camera mainCamera;
    public Rect screenRect;


    void Start()
    {
        if (Camera.main == null)
        {
            GameObject go = new GameObject("Camera");
            mainCamera = go.AddComponent<Camera>();
        }
        else
        {
            mainCamera = Camera.main;
        }
        mainCamera.clearFlags = CameraClearFlags.Color;
        mainCamera.backgroundColor = Color.gray;
        mainCamera.orthographic = true;
        mainCamera.orthographicSize = 1f;
        mainCamera.nearClipPlane = 0f;

        screenRect = Rect.MinMaxRect(-mainCamera.aspect, -1f, mainCamera.aspect, 1f);

        entitys = new DataArray<Entity>();
        playerEntitys = new DataArray<MoveAbleEntity>();
        speeds = new DataArray<SpeedComponent>();
        eatings = new DataArray<EatingComponent>();

        entitySystem = new EntitySystem(this);
        moveSystem = new MoveSystem(this);
        gameObjectSystem = new GameObjectSystem(this);

        inputSystem = new InputSystem(this);

        eatSystem = new EatSystem(this);
        eatingSystem = new EatingSystem(this);
        circlePushSystem = new CirclePushSystem(this);

        entitySystem.InitScene();
        ApplyDelayCommands();//执行延迟增删数组内容的操作
    }

    public void ApplyDelayCommands()
    {
        entitys.ApplyDelayCommands();
        playerEntitys.ApplyDelayCommands();
        speeds.ApplyDelayCommands();
        eatings.ApplyDelayCommands();
    }

    void Update()
    {
        //遍历所有Entity并执行所有相关System
        foreach (Entity item in entitys)
        {
            if (item.destroyed)
                continue;

            gameObjectSystem.Update(item.gameObject, item.position, item.size, item.color);
        }
        //多对多关系
        foreach (MoveAbleEntity player in playerEntitys)
        {
            if (player.destroyed)
                continue;

            inputSystem.Update(player.speed, player.position);
            foreach (Entity item in entitys)
            {
                if (item == player || item.destroyed)
                    continue;

                if (item.team.id == 0) //是食物，执行吃逻辑
                    eatSystem.Update(player.position, player.size, item.position, item.size, item);
                else if (item.team.id == 1) //是玩家控制角色，执行圆推挤逻辑
                    circlePushSystem.Update(player.position, player.size, item.position, item.size);
            }
        }
        //单独遍历某些Component
        foreach (SpeedComponent speed in speeds)
        {
            if (speed.destroyed)
                continue;

            Entity enity = speed.entity;
            moveSystem.Update(speed, enity.position, enity.size);
        }
        //和Entity无关的Component
        foreach (EatingComponent item in eatings)
        {
            if (item.destroyed)
                continue;

            eatingSystem.Update(item);
        }

        ApplyDelayCommands();
    }


}

