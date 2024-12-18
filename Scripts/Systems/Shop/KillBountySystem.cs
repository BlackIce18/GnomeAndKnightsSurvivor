using Leopotam.Ecs;
using System.Diagnostics;
using UnityEngine;

public class KillBountySystem : IEcsRunSystem
{
    private EcsFilter<ShopBuyItemEventComponent> _shopBuyItemFilter;
    private EcsFilter<WalletComponent> _walletFilter;
    private EcsFilter<WalletComponent, KillBountyEventComponent> _walletComponent = null;

    private void UpdateWallet(ref WalletComponent walletComponent, int price)
    {
        ref var walletEntity = ref _walletComponent.GetEntity(0);
        ref WalletUpdateComponent walletUpdateComponent = ref walletEntity.Get<WalletUpdateComponent>();
        walletComponent.money += price;
        walletUpdateComponent.money = walletComponent.money;
        walletUpdateComponent.killBounty = walletComponent.killBounty;
        walletUpdateComponent.moneyIncome = walletComponent.moneyIncome;
    }

    public void Run()
    {
        foreach (var index in _shopBuyItemFilter)
        {
            ref var shopBuyEvent = ref _shopBuyItemFilter.Get1(index);
            ref var entity = ref _shopBuyItemFilter.GetEntity(index);

            if (shopBuyEvent.item is ShopItemKillBountyData killBountyData)
            {
                foreach (var walletComponent in _walletFilter)
                {
                    ref var walletEntity = ref _walletFilter.GetEntity(walletComponent);
                    ref WalletComponent wallet = ref _walletFilter.Get1(walletComponent);
                    ref var walletUpdateComponent = ref walletEntity.Get<WalletUpdateComponent>();
                    walletUpdateComponent.moneyIncome = wallet.moneyIncome;
                    walletUpdateComponent.money = wallet.money;

                    walletUpdateComponent.killBounty = wallet.killBounty + killBountyData.additionalKillBountyPercantages;
                }

                entity.Del<ShopBuyItemEventComponent>();
            }
        }

        foreach (var index in _walletComponent)
        {
            ref WalletComponent walletComponent = ref _walletComponent.Get1(index);
            ref KillBountyEventComponent killBountyComponent = ref _walletComponent.Get2(index);
            int price = killBountyComponent.killBounty * walletComponent.killBounty / 100;

            UpdateWallet(ref walletComponent, price);

            _walletComponent.GetEntity(index).Del<KillBountyEventComponent>();
        }
    }
}
