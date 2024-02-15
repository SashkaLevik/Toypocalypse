using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Items.Potions;
using UnityEngine;

namespace Assets.Scripts.GameEnvironment.RoutEvents.EventWindows
{
    public class BuyButton : MonoBehaviour
    {
        private Potion _potion;

        public Potion Potion => _potion;

        public void GetPotion(Potion potion)
            => _potion = potion;
    }
}
