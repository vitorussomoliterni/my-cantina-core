using MyCantinaCore.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCantinaCore.Commands.Review;

namespace MyCantinaCore.Services
{
    public class ReviewApplicationService
    {
        private readonly MyCantinaCoreDbContext _context;

        public ReviewApplicationService(MyCantinaCoreDbContext context)
        {
            _context = context;
        }

        public async Task<Review> AddReview(ReviewCommand command)
        {
            var consumer = await _context.Consumers.FirstOrDefaultAsync(c => c.Id == command.ConsumerId);
            var bottle = await _context.Bottles.FirstOrDefaultAsync(b => b.Id == command.BottleId);

            if (consumer == null || bottle == null)
                throw new InvalidOperationException($"No consumer bottle found for consumer id {command.ConsumerId} and bottle id {command.BottleId}");

            var review = new Review()
            {
                ConsumerId = command.ConsumerId,
                BottleId = command.BottleId,
                Body = command.Body,
                Rating = command.Rating,
                DatePosted = DateTime.UtcNow
            };

            await _context.Reviews.AddAsync(review);

            bottle.AverageRating = bottle.Reviews.Select(r => r.Rating).Average(); // Calculates the votes average

            await _context.SaveChangesAsync();

            return review;
        }

        public async Task<Review> UpdateReview(ReviewCommand command)
        {
            var review = await _context.Reviews.Include(r => r.Bottle).FirstOrDefaultAsync(r => r.Id == command.Id);

            if (review == null)
                throw new InvalidOperationException("No review found");

            if (!review.Body.Equals(command.Body))
                review.DateModified = DateTime.UtcNow; // Updates the date modified field if the review body has been modified

            review.Body = command.Body;
            review.Rating = command.Rating;

            review.Bottle.AverageRating = review.Bottle.Reviews.Select(r => r.Rating).Average(); // Calculates the votes average

            await _context.SaveChangesAsync();

            return review;
        }

        public async Task DeleteReview(int id)
        {
            var review = await _context.Reviews.Include(r => r.Bottle).FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
                throw new InvalidOperationException("No review found");
            
            _context.Reviews.Remove(review);

            if (review.Bottle.Reviews.Count > 0)
                review.Bottle.AverageRating = review.Bottle.Reviews.Select(r => r.Rating).Average(); // Calculates the votes average

            else if (review.Bottle.Reviews.Count == 0)
                review.Bottle.AverageRating = 0;

            await _context.SaveChangesAsync();
        }

        public IQueryable<Review> GetAllReviewsByConsumerId(int consumerId)
        {
            var reviews = _context.Reviews.Where(r => r.ConsumerId == consumerId);

            return reviews;
        }

        public IQueryable<Review> GetAllReviewsByBottleId(int bottleId)
        {
            var reviews = _context.Reviews.Where(r => r.BottleId == bottleId);

            return reviews;
        }

        public IQueryable<Review> GetReview(int id)
        {
            var review = _context.Reviews.Where(r => r.Id == id);

            if (review == null)
                throw new InvalidOperationException("No review found");

            return review;
        }
    }
}
