using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    //todo:add desplay names for categories propereties
    public class Categories : ITreeNode<Categories>,IActiveFlag, IAuditInfo, Repositories.ITreeNode<Categories>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]



        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }



        private int? parentId;


        [StringLength(100)]
        [Required]

        public string CategoryName { get; set; }
        public string SLCategoryName { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }
        public bool ActiveFlag { get; set; } = false;


        public int? ParentId
        {
            get { return parentId; }

            set
            {
                if (Id == value)
                    throw new InvalidOperationException("a category cannot has it self as a parent category");
                parentId = value;
            }


        }


        public string CategoryTitle { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryImage { get; set; }
        public virtual ICollection<Products> Productses { get; set; }
        public virtual Categories Parent { get; set; }
        public IList<Categories> Children { get; set; }

        
    }
}


/*Audited By Amera Sadek*/
