using plan2plan.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Interfaces
{
    public interface ILetterRepository
    {
        void CreateLetter(Letter letter);
        Letter GetLetter(string email);
        //get letter
        int Save();
    }
}
