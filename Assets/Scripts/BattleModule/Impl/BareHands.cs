using UnityEngine;
using ZombieShooter.BattleModule;

namespace ZombieShooter
{
    public class BareHands : Weapon
    {
        public override void Shoot()
        {
            Debug.Log("you have no gun");
        }
    }
}
