using Mutantes.Infraestructura.Data;
using Mutantes.Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mutantes.Infraestructura.Repositories
{
    public class DnaAnalyzedRepository : IDnaAnalyzedRepository
    {
       readonly  private MutantsDbContext _context;
      

        public DnaAnalyzedRepository(MutantsDbContext context)
        {
            _context = context;
          
        }
        public async Task CreateAsync(DnaAnalyzed dnaAnalyzed)
        {          
            _context.Add(dnaAnalyzed);
            await _context.SaveChangesAsync();         

        }

  
    }
}
