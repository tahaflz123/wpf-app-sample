using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WpfApp1.Data;
using WpfApp1.Model;
using System.Windows;


namespace WpfApp1.Service
{
    public class IngredientService
    {

        private UserWPFContext _context { get; set; }


        public IngredientService()
        {
            _context = new UserWPFContext();
        }

        public IngredientService(UserWPFContext userWPFContext)
        {
            _context = userWPFContext;
        }



        public List<Ingredient> refreshIngredients()
        {
            return findAll();
        }

        public Ingredient saveIngredient(Ingredient ingredient)
        {
            if (ingredient is null)
                return null;


            Ingredient saved = _context.ingredient.Add(ingredient).Entity;
            _context.SaveChanges();

            return saved;
        }

        public List<Ingredient> findAll()
        {
            return _context.ingredient.Select(e => e)
                                  .ToList();
        }

        public bool deleteIngredient(Ingredient ingredient)
        {
            if (ingredient == null)
            {
                return false;
            }
            IngredientUsage ingredientUsage = _context.ingredientUsage.Where(e => e.Ingredient.Id == ingredient.Id).FirstOrDefault();
            if(ingredientUsage == null)
            {
                _context.ingredient.Remove(ingredient);
                _context.SaveChanges();
                return true;
            }

            MessageBox.Show("You can't delete this ingredient because ingredient is in usage", "Deletion failed", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            return false;

        }
    }
}
