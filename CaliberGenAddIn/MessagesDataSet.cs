namespace EAAddIn
{
    
    
    public partial class MessagesDataSet {

        public void AddMessage(string type, string description)
        {
            var message = Messages.NewMessagesRow();

            message.Type = type;
            message.Description = description;

            Messages.Rows.Add(message);
        }
        /// <summary>
        /// This adds an information message
        /// </summary>
        /// <param name="description"></param>
        public void AddMessage(string description)
        {
            var message = Messages.NewMessagesRow();

            message.Type = "Info";
            message.Description = description;

            Messages.Rows.Add(message);
        }
        public void AddErrorMessage(string description)
        {
            var message = Messages.NewMessagesRow();

            message.Type = MessageType.Error;
            message.Description = description;

            Messages.Rows.Add(message);
        }
    }
}
