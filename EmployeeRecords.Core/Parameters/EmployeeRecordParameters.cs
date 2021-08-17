using System.ComponentModel.DataAnnotations;

namespace EmployeeRecords.Core.Parameters
{
    public class EmployeeRecordParameters
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddelName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
