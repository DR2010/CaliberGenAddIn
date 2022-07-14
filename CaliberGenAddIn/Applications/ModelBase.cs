

namespace EAAddIn.Applications
{
    public class ModelBase
    {
        public EditMode Mode { get; set;}
    }

    public enum EditMode {Add,Update,
        ReadOnly
    }
}
