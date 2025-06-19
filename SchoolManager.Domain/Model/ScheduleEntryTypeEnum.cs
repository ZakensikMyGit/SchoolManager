using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Domain.Model
{
    public enum ScheduleEntryTypeEnum
    {
        ZMIANA_PODSTAWOWA,
        NADGODZINY_STALE,
        NADGODZINY_NIEPLANOWANE,
        URLOP,
        CHOROBA,
        ZASTEPSTWO,
        INNE_WYDARZENIA,
    }
}
