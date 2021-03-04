using System;
using System.Collections.Generic;
using System.Text;

namespace CotacaoHotel
{
    class PartnersChoice
    {
        public int HotelRating { get; set; }
        public int AdultsNumber { get; set; }
        public int ChildrenNumber { get; set; }
        public int NumberOfNights { get; set; }
        public string City { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }

        public static PartnersChoice GetPartnersChoice()
        {
            return new PartnersChoice()
            {
                HotelRating = 3,
                City = "Santa Catarina",
                Checkin = new DateTime(2019, 05, 09),
                Checkout = new DateTime(2019, 05, 15),
                NumberOfNights = 6,
                AdultsNumber = 2,
                ChildrenNumber = 1
            };
        }
    }
}
