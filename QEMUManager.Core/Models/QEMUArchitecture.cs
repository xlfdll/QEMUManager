using System;

namespace QEMUManager.Models
{
    public class QEMUArchitecture
    {
        internal QEMUArchitecture(String identifier, String description)
        {
            this.Identifier = identifier;
            this.Description = description;
        }

        public String Identifier { get; }
        public String Description { get; }

        public override String ToString()
        {
            return $"{this.Description} ({this.Identifier})";
        }

        public override Boolean Equals(Object obj)
        {
            QEMUArchitecture architecture = obj as QEMUArchitecture;

            if (architecture != null)
            {
                return this.Identifier.Equals(architecture.Identifier, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        public override Int32 GetHashCode()
        {
            return this.Identifier.GetHashCode() * 32687 / 5651;
        }
    }
}