using System;
using System.Threading.Tasks;

namespace Lucid.PAMS.Domain.Entities
{
    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}
