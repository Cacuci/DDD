using Store.Core.Messages;
using System;
using System.Collections.Generic;

namespace Store.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid ID { get; set; }

        private List<Event> _notificacoes;
        public IReadOnlyCollection<Event> Notificacoes => _notificacoes?.AsReadOnly();

        protected Entity()
        {
            ID = Guid.NewGuid();
        }

        public void AdicionarEvento(Event evento)
        {
            _notificacoes = _notificacoes ?? new List<Event>();
            _notificacoes.Add(evento);
        }

        public void RemoverEvento(Event eventItem)
        {
            _notificacoes?.Remove(eventItem);
        }

        public void LimparEventos()
        {
            _notificacoes?.Clear();
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
