using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace cw4.Models
{
    public class EnrollStudentDTO
    {
        [RegularExpression("^s[0-9]+$")]
        [Required]
        [NotNull]
        public string IndexNumber { get; set; }
        [Required]
        [NotNull]
        public string FirstName { get; set; }
        [Required]
        [NotNull]
        public string LastName { get; set; }
        

        private string _BirthDate;

        [Required]
        [NotNull]
        public string BirthDate { 
            get { return _BirthDate; }
            set { _BirthDate = DateTime.ParseExact(value, "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyy.MM.dd"); } 
        }
        [Required]
        [NotNull]
        public string Studies { get; set; }

    }
}
