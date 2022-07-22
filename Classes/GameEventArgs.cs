namespace Games.Classes
{
    public class GameEventArgs : EventArgs
    {
        public GameEventArgs(string text) { Text = text; }
        public string Text { get; } 
    }
}
