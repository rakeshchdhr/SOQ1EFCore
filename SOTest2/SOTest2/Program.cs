using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOTest2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new SOTestDBContext())
            {
                var serviceReview = new ServiceReview()
                {
                    Review = new Review()
                    {
                        Name = "TestReivew",
                        ServiceReviews = null
                    },
                    Service = new Service()
                    {
                        Name = "TestService",
                        ServiceReviews = null
                    }
                };
                db.ServiceReviews.Add(serviceReview);

                var review = new Review() { Name = "TestReivew Only", ServiceReviews = null };
                db.Review.Add(review);

                db.SaveChanges();
            }

            Console.WriteLine("Done !");
        }
    }

    public class SOTestDBContext : DbContext
    {
        public DbSet<Review> Review { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<ServiceReview> ServiceReviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=.;Database=SOTestDB;Integrated Security=True");
        }
    }

    public class Service
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ServiceReview> ServiceReviews { get; set; } = new List<ServiceReview>();
    }
    public class Review
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ServiceReview> ServiceReviews { get; set; } = new List<ServiceReview>();
    }

    public class ServiceReview
    {
        [Key]
        public long Id { get; set; }
        public long ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; } = new Service();
        public long ReviewId { get; set; }
        [ForeignKey("ReviewId")]
        public virtual Review Review { get; set; } = new Review();
    }
}
