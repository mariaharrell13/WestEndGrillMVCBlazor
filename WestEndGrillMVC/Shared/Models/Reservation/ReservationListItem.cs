﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestEndGrillMVC.Shared.Models.Reservation
{
    internal class ReservationListItem
    {
        public int NumberOfGuests { get; set; }
        public DateTime DateTime { get; set; }
        public string PartyName { get; set; }
    }
}
