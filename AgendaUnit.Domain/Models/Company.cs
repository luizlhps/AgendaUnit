using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AgendaUnit.Domain.Models;

namespace AgendaUnit.Domain.Models
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string TypeCompany { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }

        public List<User>? Users { get; set; }

        public List<Customer>? Customers { get; set; }

        public List<Scheduling>? Schedulings { get; set; }

        public List<Service>? Services { get; set; }
    }
}
