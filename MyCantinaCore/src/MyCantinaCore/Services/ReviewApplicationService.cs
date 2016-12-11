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

            bottle.AverageRating = bottle.Reviews.Select(r => r.Rating).Average();

            await _context.SaveChangesAsync();

            return review;
        }

        public async Task<Review> UpdateReview(ReviewCommand command)
        {
            var consumer = await _context.Consumers.FirstOrDefaultAsync(c => c.Id == command.ConsumerId);
            var bottle = await _context.Bottles.FirstOrDefaultAsync(b => b.Id == command.BottleId);

            if (consumer == null || bottle == null)
                throw new InvalidOperationException($"No consumer bottle found for consumer id {command.ConsumerId} and bottle id {command.BottleId}");

            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.BottleId == command.BottleId && r.ConsumerId == command.ConsumerId);

            if (review == null)
                throw new InvalidOperationException("No review found");

            if (!review.Body.Equals(command.Body))
                review.DateModified = DateTime.UtcNow; // Updates the date modified field if the review body has been modified
           
            review.Body = command.Body;
            review.Rating = command.Rating;

            bottle.AverageRating = bottle.Reviews.Select(r => r.Rating).Average();

            await _context.SaveChangesAsync();

            return review;
        }

        public async Task DeleteReview(int consumerId, int bottleId)
        {
            var consumer = await _context.Consumers.FirstOrDefaultAsync(c => c.Id == consumerId);
            var bottle = await _context.Bottles.FirstOrDefaultAsync(b => b.Id == bottleId);

            if (consumer == null || bottle == null)
                throw new InvalidOperationException($"No consumer bottle found for consumer id {consumerId} and bottle id {bottleId}");

            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.BottleId == bottleId && r.ConsumerId == consumerId);

            if (review == null)
                throw new InvalidOperationException("No review found");

            _context.Reviews.Remove(review);

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

        public async Task<Review> GetReview(int bottleId, int consumerId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.ConsumerId == consumerId && r.BottleId == bottleId);

            if (review == null)
                throw new InvalidOperationException("No review found");

            return review;
        }
    }
}
