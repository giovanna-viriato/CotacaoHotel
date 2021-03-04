using System;


namespace CotacaoHotel
{
    class Program
    {
        static void Main(string[] args)
        {
            var reservation = PartnersChoice.GetPartnersChoice();
            var hotelInfo = HotelSearch.BuscarMelhorPreco(reservation.City, reservation.Checkin, reservation.Checkin.AddDays(reservation.NumberOfNights), reservation.HotelRating, reservation.AdultsNumber);
            var partner = PartnersData.GetAgentData();

            string Disclaim = ($"{reservation.AdultsNumber} adultos e X crianças, com entrada no dia {hotelInfo.Checkin.ToString("dd/MM/yyyy")}, e saída no dia {hotelInfo.Checkout.ToString("dd/MM/yyyy")} no Hotel {hotelInfo.HotelName} {reservation.HotelRating} estrelas {hotelInfo.Amenities}. Valor da diária: a partir de R${hotelInfo.PricePerNight}. Sujeito a Disponibilidade.");
            
            //Gerar Template de Imagem
            GeneratesImage.GenerateImage(partner.PartnersName, partner.AgencyName, partner.PartnersLink, partner.PartnersNumber, partner.PartnersPhoto, reservation.City, hotelInfo.PricePerNight * reservation.NumberOfNights, Disclaim, 170, 170, "agente.jpg", hotelInfo.HotelPhoto);            
            
            var fotoHotel = hotelInfo.HotelPhoto;
        }


    }
}
