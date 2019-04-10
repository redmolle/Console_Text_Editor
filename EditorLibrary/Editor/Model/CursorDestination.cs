namespace EditorLibrary.Editor
{
    public class CursorDestination
    {
        private int? _position;

        public int? Position { get { return _position.Value - 1; } set { _position = value; } }
        public string Word { get; set; }
    }
}
