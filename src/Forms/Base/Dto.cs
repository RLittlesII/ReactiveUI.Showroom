using System;

namespace Showroom
{
    public abstract class Dto
    {
        protected Dto()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}