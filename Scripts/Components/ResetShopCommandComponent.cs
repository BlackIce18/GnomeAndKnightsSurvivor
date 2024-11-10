using Leopotam.Ecs;

public struct ResetShopComponent
{
    public ResetShopCommand resetShopCommand;
    public EcsEntity shopEntity;
    public bool isAvailable;
    public int rollsCount;
    public int currentResetPrice;
}
public struct ResetShopUpdateComponent : IEcsIgnoreInFilter { }