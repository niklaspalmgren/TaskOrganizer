namespace TaskOrganizer.AppServer
{
    public class SearchTaskEventArgs : EventArgs
    {
        public string Wildcard { get; }
        public int? UserId { get; }

        public SearchTaskEventArgs(string wildcard, int? userId)
        {
            Wildcard = wildcard;
            UserId = userId;
        }
    }
}
