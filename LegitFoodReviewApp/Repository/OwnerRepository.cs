using AutoMapper;
using LegitFoodReviewApp.Data;
using LegitFoodReviewApp.Interfaces;
using LegitFoodReviewApp.Models;

namespace LegitFoodReviewApp.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public OwnerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Owner GetOwner(int ownerId)
        {
            return _context.Owners.Where(o=>o.Id==ownerId).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfAFood(int foodId)
        {
            return _context.FoodOwners.Where(p => p.Food.Id == foodId).Select(o => o.Owner).ToList();
        }


        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

        public ICollection<Food> GetFoodByOwner(int ownerId)
        {
            return _context.FoodOwners.Where(p=> p.Owner.Id==ownerId).Select(p => p.Food).ToList();
        }
        public bool OwnerExists(int ownerId)
        {
            return _context.Owners.Any(o=> o.Id==ownerId);
        }

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOwner(Owner owner)
        {
            _context.Update(owner);
            return Save();
        }

        public bool DeleteOwner(Owner owner)
        {
            _context.Remove(owner);
            return Save();
        }
    }
}
