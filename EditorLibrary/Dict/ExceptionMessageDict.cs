namespace EditorLibrary.Dict {
    public static class ExceptionMessageDict {
        public static string Text { get { return "Текст"; } }
        public static string Cursor { get { return "Указатель"; } }
        public static string Cmd { get { return "Команда"; } }
        public static string ReservedName { get { return "Имя уже используется в системе"; } }
        public static string Add { get { return "Невозможно добавить элемент"; } }
        public static string WrongCmd { get { return "Введена неверная команда"; } }
        public static string AlreadyExistsText { get { return "В системе уже содержится текст"; } }
        public static string NoSuchText { get { return "В системе нет текста"; } }
        public static string NoSuchElement { get { return "В системе нет элемента"; } }

        public static string MisstakeCmd { get { return "Ошибка в команде"; } }
        public static string Expected { get { return "ожидалось встретить"; } }
        public static string Finded { get { return ", а встречено"; } }
        public static string word { get { return "слово"; } }
        public static string number { get { return "число"; } }
        public static string or { get { return "или"; } }
        public static string direction { get { return $"стрелка <- {or} ->"; } }
        public static string text { get { return "текст"; } }
        public static string EndLine {get{return "конец строки";}}

    }
}
