using System;

namespace Sketch.Shared.Data.Users
{
    public class User
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Guid == ((User)obj).Guid;
        }
    }
}
