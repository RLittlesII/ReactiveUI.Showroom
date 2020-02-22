using System;

namespace Showroom.Base
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