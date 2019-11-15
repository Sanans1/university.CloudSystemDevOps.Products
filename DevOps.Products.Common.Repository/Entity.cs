using System;
using System.Collections.Generic;
using System.Text;

namespace DevOps.Products.Common.Repository
{
    public abstract class Entity : IEntity
    {
        public int ID { get; set; }
        public bool IsActive { get; set; }
    }
}
