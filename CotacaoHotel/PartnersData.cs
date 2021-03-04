using System;
using System.Collections.Generic;
using System.Text;

namespace CotacaoHotel
{
    class PartnersData
    {
        public string PartnersName { get; set; }
        public string PartnersLink { get; set; }
        public string PartnersPhoto { get; set; }
        public string PartnersNumber { get; set; } //verificar se o numero no banco está sendo salvo como string mesmo
        public string AgencyName { get; set; }
        
        public static PartnersData GetAgentData()
        {
            return new PartnersData
            {
                PartnersName = "Cristina Pereira",
                AgencyName = "Pereira Travel",
                PartnersLink = "app.p2d.travel/Pereira-Travel",
                PartnersPhoto = "https://blog.unyleya.edu.br/wp-content/uploads/2017/12/saiba-como-a-educacao-ajuda-voce-a-ser-uma-pessoa-melhor.jpeg",
                PartnersNumber = "(41) 99999-8888"
            };
        }
    }
}
