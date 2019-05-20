using EditorLibrary.Editor;

namespace EditorLibrary.Cmd
{
    public class SendCmd
    {
        public string SourceName { get; set; }
        public string TargetName { get; set; }
        public CursorDestination After { get; set; }
    }
}
