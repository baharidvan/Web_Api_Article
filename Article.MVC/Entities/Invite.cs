using System.ComponentModel.DataAnnotations.Schema;

namespace Article.MVC.Entities
{
    public class Invite
    {
        public int Id { get; set; }
        public int InviteCode { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
