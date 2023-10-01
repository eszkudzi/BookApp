using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookApp.Entities
{
    public abstract class EntityBase : IEntity
    {
        public int Id { get; set; }

    }
}
