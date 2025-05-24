using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Domain.Model
{
    public class Declaration
    {
        public int Id { get; set; } // Id dokumentu

        public int ChildId { get; set; } // Przypisanie do dziecka
        public virtual Child Child { get; set; }

        public string Type { get; set; } // Typ dokumentu: "Orzeczenie o kształceniu", "Opinia PPP" itp.
        public DateTime? IssueDate { get; set; } // Data wydania
        public DateTime? ValidUntil { get; set; } // Data ważności (jeśli określona)

        public string IssuingAuthority { get; set; } // Organ wydający (np. Poradnia)
        public string Diagnosis { get; set; } // Nazwa schorzenia, np. "Autyzm", "Zespół Aspergera"
        public bool IsVoluntaryRequest { get; set; } // Czy wniosek był z inicjatywy rodziców

        public string Description { get; set; } // Dodatkowe informacje
    }
}
