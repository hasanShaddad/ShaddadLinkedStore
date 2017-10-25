using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Units
{
   public interface IAttributeHeadersUnit
    {

        IDataRepository<AttributeHeaders> AttributeHeaderRepository { get; }

        void AddAttributeHeader(AttributeHeaders entity);

        void UpdateAttributeHeader(AttributeHeaders entity);

        void DeleteAttributeHeader(int id);
        /// <summary>
        /// save all changes
        /// </summary>
        void SaveChanges();
        Task SaveChangesAsync();


        int GetAttributeHeaderCount(string attributeHeaderName);

        Task<Core.Models.AttributeHeaders> FindAsync(int? id);
        IQueryable<AttributeHeaders> GetAllAttributeHeaders();


        /// <summary>
        /// dispose all
        /// </summary>
        void Dispose();


    }
}
