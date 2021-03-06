﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public abstract class DateTimeAdjuster {
        public abstract IEnumerable<DateTime?> Adjust(DateTimeFormula Source, DateTime MinDate, DateTime MaxDate, DateTime StartDate);
    }
    



}
