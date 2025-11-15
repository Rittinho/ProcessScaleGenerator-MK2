namespace ProcessScaleGenerator.Shared.ValueObjects
{
    public class TokenAction
    {
        public string ActionTitle { get; set; }
        public string ActionDescription { get; set; }
        public bool CanConfirm { get; set; }

        public TokenAction(string title, string description, bool canConfirm)
        {
            ActionTitle = title;
            ActionDescription = description;
            CanConfirm = canConfirm;
        }
        public TokenAction(string title, string description)
        {
            ActionTitle = title;
            ActionDescription = description;
            CanConfirm = false;
        }
    }
}
