using System;
using System.Collections.Generic;
using System.Text;

namespace CotacaoHotel
{
    class HotelSearch
    {
        public int HotelId { get; set; }

        public string HotelName { get; set; }

        public int PricePerNight { get; set; }
        public string City { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public string Amenities { get; set; }
        public string HotelPhoto { get; set; }

        public static HotelSearch BuscarMelhorPreco(string Destino, DateTime Dia, DateTime dateTime, int hotelRating, int peopleNumber)
        {
            return new HotelSearch()
            {
                HotelId = 88,
                HotelName = "Pousada Ecos do Mar Praia do Frances",
                PricePerNight = 1761,
                City = Destino,
                Checkin = Dia,
                Checkout = dateTime,
                Amenities = "com café da manhã incluído",
                HotelPhoto = "https://www.ahstatic.com/photos/9399_ho_00_p_1024x768.jpg"
            };
        }
    }
}
