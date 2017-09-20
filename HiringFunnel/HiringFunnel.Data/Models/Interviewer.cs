using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiringFunnel.Data.Models
{
    public class Interviewer
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public Phase Type { get; set; }

        [Required]
        [ForeignKey("InterviewerOnPhase")]
        public int InterviewerId { get; set; }

        public virtual User InterviewerOnPhase { get; set; }

        [Required]
        [ForeignKey("InstanceOfProcess")]
        public int PInsId { get; set; }

        public virtual ProcessInstance InstanceOfProcess { get; set; }

    }
}
