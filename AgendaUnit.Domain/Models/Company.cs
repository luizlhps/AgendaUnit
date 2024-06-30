using System.Collections.Generic;

namespace AgendaUnit.Domain.models
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }

        public string TypeCompany { get; set; }

        public int OwnerId { get; set; }

        public User Owner { get; set; }

        public List<User>? Users { get; set; }

        public List<Customer>? Customers { get; set; }

        public List<Scheduling>? Scheduling { get; set; }

        public List<Service> Services { get; set; }
    }
}
