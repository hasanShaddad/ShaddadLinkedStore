using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmatgar.Core.Repositories;
using Elmatgar.Core.Units;

namespace Elmatgar.persistence.GRepositories.DomainUnits
{
   public class AttributeHeaderUnit :IDisposable ,IAttributeHeadersUnit
    {
        private StoreDbContext _context = new StoreDbContext();
        private IDataRepository<Elmatgar.Core.Models.AttributeHeaders> attributeHeader = null;

        public IDataRepository<Elmatgar.Core.Models.AttributeHeaders> AttributeHeaderRepository
        {


            get
            {
                if (this.attributeHeader == null)

                {
                    this.attributeHeader = new DataRepository<Elmatgar.Core.Models.AttributeHeaders>(this._context);
                }
                return this.attributeHeader;
            }
        }

        public void AddAttributeHeader(Core.Models.AttributeHeaders entity)
        {
           AttributeHeaderRepository.Add(entity);
        }

        public void UpdateAttributeHeader(Core.Models.AttributeHeaders entity)
        {
            AttributeHeaderRepository.Update(entity);
        }

        public void DeleteAttributeHeader(int id)
        {
           AttributeHeaderRepository.Delete(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
             await _context.SaveChangesAsync();


           
        }

        public int GetAttributeHeaderCount(string attributeHeaderName)
        {
            return _context.AttributeHeaders.Count(a => a.AttributeName == attributeHeaderName);
        }

        public async Task<Core.Models.AttributeHeaders> FindAsync(int? id)
        {
          return   await AttributeHeaderRepository.GetByIdAsync(id);
        }

        public IQueryable<Core.Models.AttributeHeaders> GetAllAttributeHeaders()
        {

            return AttributeHeaderRepository.GetAll();
        }

        public void Dispose()
        {
            this._context?.Dispose();
        }
    }
}
