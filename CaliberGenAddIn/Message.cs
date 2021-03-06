///////////////////////////////////////////////////////////
//  Message.cs
//  Implementation of the Class Message
//  Generated by Enterprise Architect
//  Created on:      11-Nov-2009 16:48:59
//  Original author: Colin Richardson
///////////////////////////////////////////////////////////


using EAAddIn.Windows.Interfaces;

namespace EAAddIn {
	public class Message {

        public delegate void MessagesHandler(object sender, MessagesEventArgs e);

	    private string type = string.Empty; 

	    public virtual string Type
	    {
	        get { return type; }
	        set { type = value;}
	    }
        public string Text { get; set; }
        public object Tag { get; set; }

	}//end Message

    public class InformationMessage : Message
    {
        public InformationMessage( string text)
        {
            Text = text;
        }
        public override string Type { get { return MessageType.Information;} }

    }

    public class ErrorMessage : Message
    {
        public ErrorMessage( string text)
        {
            Text = text;
        }
        public override string Type { get { return MessageType.Error; } }

    }

    public class WarningMessage : Message
    {
        public WarningMessage( string text)
        {
            Text = text;
        }
        public override string Type { get { return MessageType.Warning; } }

    }
    public class QuestionMessage : Message
    {
        public QuestionMessage(string text)
        {
            Text = text;
        }
        public override string Type { get { return MessageType.Question; } }

    }
}