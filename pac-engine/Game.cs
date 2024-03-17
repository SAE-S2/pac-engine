using System;
using pac_engine.Graphics;

namespace pac_engine
{
	public class Game
	{
		private string title;
		private Vector2 screenSize;

		public Game(string title, float x, float y)
		{
			this.title = title;
			this.screenSize = new Vector2(x, y);
		}
	}
}