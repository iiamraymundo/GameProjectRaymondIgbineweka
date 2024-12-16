using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRaymondIgbineweka.Manager_s
{
    internal class CollisionManager
    {
        private PlayerManager _player;
        private List<Enemy> _enemies;
        private List<Coin> _coins;

        public CollisionManager(PlayerManager player, List<Enemy> enemies, List<Coin> coins)
        {
            _player = player;
            _enemies = enemies;
            _coins = coins;
        }

        public void CheckCollisions()
        {
            // Botsingen met vijanden
            foreach (var enemy in _enemies)
            {
                if (_player.BoundingBox.Intersects(enemy.BoundingBox))
                {
                    _player.TakeDamage();
                }
            }

            // Botsingen met munten
            for (int i = _coins.Count - 1; i >= 0; i--)
            {
                if (_player.BoundingBox.Intersects(_coins[i].BoundingBox))
                {
                    _coins.RemoveAt(i);
                    _player.AddScore(10);                  

                }
            }
        }
    }
}
