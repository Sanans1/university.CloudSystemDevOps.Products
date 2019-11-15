using System;
using DevOps.Products.Common.Repository;

namespace DevOps.Products.Reviews.DAL
{
    public class Review : Entity
    {
        public int Rating { get; set; }
        public string Text { get; set; }

        public int ProductID { get; set; }
        public int CustomerID { get; set; }
    }
}
