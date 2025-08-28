using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoUtils;
using MonoUtils.Graphics;
using MonoUtils.Input;
using MonoUtils.Utility;

namespace MonoSpaceShooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private Sprites sprites;
        private Shapes shapes;
        private Texture2D texture;
        private Camera camera;
        private Screen screen;
        private UtilsKeyboard keyboard = new UtilsKeyboard();
        private UtilsMouse mouse = new UtilsMouse();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.SynchronizeWithVerticalRetrace = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            sprites = new Sprites(this);
            shapes = new Shapes(this);
            screen = new Screen(this, 640, 480);
            camera = new Camera(screen);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // texture = Content.Load<Texture2D>("smiley");
        }

        protected override void Update(GameTime gameTime)
        {
            keyboard.Update();
            mouse.Update();

            if (keyboard.IsKeyClicked(Keys.Escape)) { this.Exit(); }
            if (keyboard.IsKeyClicked(Keys.F)) { this.screen.ToggleFullScreen(this.graphics); }

            //float factor = 0.5f;

            //if (keyboard.IsKeyDown(Keys.W)) { y += factor; }
            //if (keyboard.IsKeyDown(Keys.A)) { x -= factor; }
            //if (keyboard.IsKeyDown(Keys.S)) { y -= factor; }
            //if (keyboard.IsKeyDown(Keys.D)) { x += factor; }
            //if (keyboard.IsKeyDown(Keys.E)) { rotation -= factor / 10; }
            //if (keyboard.IsKeyDown(Keys.Q)) { rotation += factor / 10; }
            //if (keyboard.IsKeyDown(Keys.OemPlus)) { scale += factor; }
            //if (keyboard.IsKeyDown(Keys.OemMinus)) { scale -= factor; }


            if (mouse.IsScrollingUp())
            {
                camera.MoveZ(10f);
            }
            else if (mouse.IsScrollingDown())
            {
                camera.MoveZ(-10f);
            }

            if (keyboard.IsKeyClicked(Keys.R) && keyboard.IsKeyDown(Keys.LeftControl))
            {
                camera.ResetZ();
            }

            if (keyboard.IsKeyDown(Keys.Left)) { camera.MoveCam(new Vector2(-5f, 0f) * camera.ZoomFactor); }
            if (keyboard.IsKeyDown(Keys.Right)) { camera.MoveCam(new Vector2(5f, 0f) * camera.ZoomFactor); }
            if (keyboard.IsKeyDown(Keys.Up)) { camera.MoveCam(new Vector2(0f, 5f) * camera.ZoomFactor); }
            if (keyboard.IsKeyDown(Keys.Down)) { camera.MoveCam(new Vector2(0f, -5f) * camera.ZoomFactor); }

            if (mouse.IsMiddleButtonDown()) { camera.MoveCam(new Vector2(-mouse.DeltaX, mouse.DeltaY) * camera.ZoomFactor); }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            screen.Set();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Viewport vp = GraphicsDevice.Viewport;

            sprites.Begin(camera, false);

            sprites.End();

            shapes.Begin(camera);

            shapes.End();

            screen.Unset();
            screen.Present(sprites);

            base.Draw(gameTime);
        }
    }
}
