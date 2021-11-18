using System;

namespace Store.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid ID { get; set; }

        protected Entity()
        {
            ID = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo))
            {
                return true;
            }

            if (!ReferenceEquals(null, obj))
            {
                return false;
            }

            return base.Equals(compareTo.ID);
        }

        public static bool operator ==(Entity x, Entity y)
        {
            if (ReferenceEquals(x, null) && ReferenceEquals(y, null))
            {
                return true;
            }

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }

            return x.Equals(y);
        }

        public static bool operator !=(Entity x, Entity y)
        {
            return !(x == y);
        }

        public override int GetHashCode() => (GetType().GetHashCode() * 907) + ID.GetHashCode();

        public override string ToString() =>  $"{GetType().Name} [ID={ID}]";    

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
