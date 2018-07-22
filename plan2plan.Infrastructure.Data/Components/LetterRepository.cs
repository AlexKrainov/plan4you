using plan2plan.Domain.Core;
using plan2plan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace plan2plan.Infrastructure.Data.Components
{
    public class LetterRepository : ILetterRepository
    {
        private plat2platContext context;

        public LetterRepository(plat2platContext context)
        {
            this.context = context;
        }
        public void CreateLetter(Letter letter)
        {
            context.Letters.Add(letter);
        }

        public Letter GetLetter(string email)
        {
            return context.Letters
                .Include(x => x.email)
                .Where(x => x.email.Mail == email && x.LetterTypeID == 2).ToList().Last(); //2 - Confirm email;
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
