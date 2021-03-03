using System;
using System.Collections.Generic;
using System.Text;

namespace CotacaoHotel
{
    class BuscaHotel
    {
        public int IdHotel { get; set; }

        public string NomeHotel { get; set; }

        public int PrecoDiaria { get; set; }
        public string Cidade { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public string Extras { get; set; }
        public string FotoHotel { get; set; }

        public static BuscaHotel BuscarMelhorPreco(string Destino, DateTime Dia, DateTime dateTime, int numEstrelas, int numPessoas)
        {
            return new BuscaHotel()
            {
                IdHotel = 88,
                NomeHotel = "Pousada Ecos do Mar Praia do Frances",
                PrecoDiaria = 1761,
                Cidade = Destino,
                Checkin = Dia,
                Checkout = dateTime,
                Extras = "com café da manhã incluído",
                FotoHotel = "https://www.ahstatic.com/photos/9399_ho_00_p_1024x768.jpg"
            };
        }
    }
}
