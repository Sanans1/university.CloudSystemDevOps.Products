using System;
using System.Collections.Generic;
using System.Text;

namespace DevOps.Products.Common.Repository
{
    public interface IEntity
    {
        int? ID { get; set; }
        bool IsActive { get; set; }
    }
}
