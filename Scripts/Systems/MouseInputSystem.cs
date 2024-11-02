using Leopotam.Ecs;
using UnityEngine;

public class MouseInputSystem : IEcsRunSystem
{
    EcsWorld _world;
    private SceneData _sceneData;
    private EcsFilter<UserInputComponent> _filter; // фильтр, который выдаст нам все сущности, у которых есть компонент PlayerInputComponent

    public void Run()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var mouseX = Input.mousePosition.x;
        var mouseY = Input.mousePosition.y;

        foreach (var i  in _filter)
        {
            ref var playerInputComponent = ref _filter.Get1(i);
            playerInputComponent.moveX = x;
            playerInputComponent.moveY = y;
            playerInputComponent.movePosition = new Vector2(x, y);

            playerInputComponent.mouseX = mouseX;
            playerInputComponent.mouseY = mouseY;
            playerInputComponent.mousePosition = new Vector2(mouseX, mouseY);

            playerInputComponent.mousePositionAtTerrain = MousePositionAtTerrain();
        }
    }

    private Vector3 MousePositionAtTerrain()
    {
        var camera = _sceneData.mainCamera;
        var mousePosition = _filter.Get1(0);
        var ray = camera.ScreenPointToRay(mousePosition.mousePosition);

        RaycastHit hit;
        int cubeLayerIndex = LayerMask.NameToLayer("Terrain");
        int layerMask = (1 << cubeLayerIndex);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}
