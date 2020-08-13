namespace View.VisualStudio.TextTemplating
{
    public static class TemplateHelper
    {
        public static string TrimOther(this string str)
        {
            return str.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
        }

        public static string[] Split2(this string comment, char ch)
        {
            string[] _comments = new string[2];
            _comments[0] = comment.Substring(0, comment.IndexOf(ch)).TrimOther();
            _comments[1] = comment.Substring(comment.IndexOf(ch) + 1).TrimOther();
            return _comments;
        }
    }
}
