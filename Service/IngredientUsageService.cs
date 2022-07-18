using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WpfApp1.Model;
using WpfApp1.Data;
using System.Linq;

namespace WpfApp1.Service
{
    public class IngredientUsageService
    {

        private UserWPFContext _context { get; set; }


        public IngredientUsageService()
        {
            _context = new UserWPFContext();
        }

        public IngredientUsageService(UserWPFContext userWPFContext)
        {
            _context = userWPFContext;
        }

        public List<IngredientUsage> refreshPatients()
        {
            return findAll();
        }

        public IngredientUsage savePatient(IngredientUsage ingredient)
        {
            if (ingredient is null)
                return null;


            IngredientUsage saved = _context.ingredientUsage.Add(ingredient).Entity;
            _context.SaveChanges();

            return saved;
        }

        public List<IngredientUsage> findAll()
        {
            return _context.ingredientUsage.Select(e => e)
                                  .Include(e => e.Ingredient)
                                  .Include(e => e.order)
                                  .ToList();
        }

        public void deletePatient(IngredientUsage ingredient)
        {
            if (ingredient == null)
            {
                return;
            }
            _context.ingredientUsage.Remove(ingredient);
            _context.SaveChanges();



        }

        public void saveIngredientUsageList(List<IngredientUsage> ingredientUsages)
        {
            _context.ingredientUsage.AddRange(ingredientUsages);
            _context.SaveChanges();
        }
    }
}
