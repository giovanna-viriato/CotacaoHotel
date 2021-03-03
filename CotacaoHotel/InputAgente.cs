using System;
using System.Collections.Generic;
using System.Text;

namespace CotacaoHotel
{
    class InputAgente
    {
        public int NumEstrelas { get; set; }

        public int NumPessoas { get; set; }

        public int NumDias { get; set; }
        public string Cidade { get; set; }
        public DateTime Checkin { get; set; }

        public static InputAgente RecebeDadosCotacao()
        {
            return new InputAgente()
            {
                NumEstrelas = 3,
                Cidade = "Santa Catarina",
                Checkin = new DateTime(2019, 05, 09),
                NumDias = 6,
                NumPessoas = 2
            };
        }
    }
}
