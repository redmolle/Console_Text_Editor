namespace EditorLibrary.Editor {
    public class CursorDestination {
        public int? Position { get; set; }

        //public int? Position
        //{
        //    get
        //    {
        //        return _position.HasValue ?
        //            _position - 1 : null;
        //    }
        //    set { _position = value; }
        //}
        public string Word { get; set; }
    }
}
