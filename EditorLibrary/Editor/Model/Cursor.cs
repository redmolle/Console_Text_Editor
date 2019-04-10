namespace EditorLibrary.Editor
{
    public class Cursor : IElement
    {
        public Text target { get; set; }
        public int TargetSize { get; set; }
        public string Name { get; set; }
        public int From { get; set; }
        public bool Ahead { get; set; }
        public CursorDestination To { get; set; }
        public string Data { get; set; }
    }
}
