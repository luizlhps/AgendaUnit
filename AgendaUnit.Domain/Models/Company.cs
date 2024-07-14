using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AgendaUnit.Domain.Models;

namespace AgendaUnit.Domain.Models
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }

        [Column(name: "type_company")]
        public string TypeCompany { get; set; }

        [Column(name: "owner_id")]
        public int OwnerId { get; set; }
        public User Owner { get; set; }

        public List<User>? Users { get; set; }

        public List<Customer>? Customers { get; set; }

        public List<Scheduling>? Scheduling { get; set; }

        public List<Service>? Services { get; set; }
    }
}
