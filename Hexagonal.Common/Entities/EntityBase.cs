using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hexagonal.Common.Entities
{
    public class EntityBase
    {
        [Key]
        public Guid? Id { get; set; }

        //
        // Summary:
        //     Created At
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        //
        // Summary:
        //     Updated at
        public DateTime? UpdatedAt { get; protected set; }

        //
        // Summary:
        //     Generates a new guid for any class.
        //
        // Parameters:
        //   aReplace:
        //     Use this flag if you want to replace existant value. Default false.
        //
        // Returns:
        //     Guid value
        //
        // Remarks:
        //     If class has no Id, it will be created. If class already have an Id, the flag
        //     that allows or not the replacement must be placed. If true, it will replace with
        //     a new one. Both cases will return the most recent Guid. If not modified, current
        //     one.
        public virtual Guid NewId(bool replace = false)
        {
            if (!Id.HasValue || replace)
            {
                Id = System.Guid.NewGuid();
            }

            if (UpdatedAt.HasValue)
            {
                Update();
            }

            return Id.Value;
        }

        /// <summary>
        /// Change the UpdatedAt information to UtcNow
        /// </summary>
        /// <returns></returns>
        public virtual EntityBase Update()
        {
            UpdatedAt = DateTime.UtcNow;
            return this;
        }
    }
}
