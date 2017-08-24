using System;

namespace ChatDemo.Data
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime CreatedOnUtc { get; set; }
        DateTime UpdatedOnUtc { get; set; }
    }
}
