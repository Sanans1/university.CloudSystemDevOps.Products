using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevOps.Products.Common.Repository;
using DevOps.Products.Reviews.DAL;
using DevOps.Products.Reviews.REST.API.Models;

namespace DevOps.Products.Reviews.REST.API.Services.Repos
{
    public class ReviewRepository : GenericRepository<ReviewContext, Review, ReviewDTO>
    {
        public ReviewRepository(ReviewContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
