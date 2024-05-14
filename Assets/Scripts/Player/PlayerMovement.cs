using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : Movement
    {                        
        public override void PushInAttack()
            => MoveRight();

        public override void PushInDefence()
            => MoveLeft();        
    }
}
