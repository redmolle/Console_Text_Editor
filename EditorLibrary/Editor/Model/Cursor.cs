namespace EditorLibrary.Editor {
    public class Cursor : IElement {
        public string Name { get; set; }
        public Text Target { get; set; }
        public CursorDestination From { get; set; }
        public bool Ahead { get; set; }
        public CursorDestination To { get; set; }

        public int TargetSize { get; set; }
        public string Data { get; set; }
    }
}
