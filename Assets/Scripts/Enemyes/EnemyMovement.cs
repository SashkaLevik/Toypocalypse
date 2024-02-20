using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemyes
{
    public class EnemyMovement : Movement
    {                    
        public override void MoveRight()
        {
            base.MoveRight();                        
        }

        public override void MoveLeft()
        {
            base.MoveLeft();                       
        }        

        public override void Push()
            => MoveRight();

        public override void Pull()
            => MoveLeft();        
    }
}
