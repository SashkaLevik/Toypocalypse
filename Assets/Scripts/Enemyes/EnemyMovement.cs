using Assets.Scripts.Player;

namespace Assets.Scripts.Enemyes
{
    public class EnemyMovement : Movement
    {        
        public override void PushInAttack()
            => MoveRight();

        public override void PushInDefence()
            => MoveLeft();        
    }
}
