using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectRaymondIgbineweka.Manager_s
{
    internal class PlayerManager
    {
        public Vector2 Position { get; private set; }
        private Texture2D playerTexture { get; set; }
        private int lives;
        public int score { get; set; };

        private Vector2 velocity;
        private bool isJumping;
        private float jumpSpeed = -10f;
        private float gravity = 0.5f;

        private bool isInvincible = false; // invincibility na schade
        private double invincibleTimer = 0; // tijd dat je invincible bent

       
        public PlayerManager(Texture2D texture)
        {
            playerTexture = texture;
            Position = new Vector2(100, 300);
            lives = 3;
            score = 0;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                velocity.X = -5f;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                velocity.X = 5f;
            }
            else
            {
                velocity.X = 0; // Stop met bewegen
            }

            // Om te springen
            if (keyboardState.IsKeyDown(Keys.Space) && !isJumping)
            {
                isJumping = true;
                velocity.Y = jumpSpeed; // Geef een sprong omhoog
            }

            //Gravitie configuratie
            if (isJumping)
            {
                velocity.Y += gravity;
            }

            Position += velocity;

            // Simuleer grond (zodat speler niet blijft vallen)
            
            
            if (Position.Y >= 300)
            {
                Vector2 tempPosition = Position;
                tempPosition.Y = 300;
                Position = tempPosition;
                isJumping = false; // Speler staat weer op de grond
            }

            if (isInvincible)
            {
                invincibleTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                if(invincibleTimer <= 0)
                {
                    isInvincible = false;
                }
            }
        }

        public void TakeDamage()
        {
            if (!isInvincible)
            {
                lives--;
                isInvincible = true;
                invincibleTimer = 2.0;

                if (lives <= 0)
                {
                    Game1.GameOver = true;
                }
            }
        }


        //Deze methode ga ik niet nodig hebben maar kan handig zijn voor later
        public bool IsjumpingOn(Enemy enemy)
        {
            return Position.Y + playerTexture.Height < enemy.Position.Y + 10;
        }

        

        public void AddScore(int amount)
        {
            score += amount;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            Color tint = isInvincible ? Color.Red * 0.5f : Color.White; // Laat de speler hier flikkeren
            spriteBatch.Draw(playerTexture, Position, tint);
        }

        public Rectangle BoundingBox
        {
            get
            {
                if (Texture == null)
                {
                    throw new InvalidOperationException("Texture is null. Zorg ervoor dat Texture is toegewezen.");
                }

                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

            }
        }
    }
}
