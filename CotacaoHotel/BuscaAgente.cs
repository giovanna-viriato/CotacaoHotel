using System;
using System.Collections.Generic;
using System.Text;

namespace CotacaoHotel
{
    class BuscaAgente
    {
        public string NomeAgente { get; set; }
        public string LinkAgente { get; set; }
        public string FotoAgente { get; set; }
        public string NumeroAgente { get; set; } //verificar se o numero no banco está sendo salvo como string mesmo
        public string NomeAgencia { get; set; }
        public string CorPrincipal { get; set; }
        public string CorSecundaria { get; set; }
        public string CorTextosPrincipal { get; set; }
        public string CorTextosSecundaria { get; set; }

        //talvez buscar o padrão de cores do site do agente para montar a imagem? As cores vão ser salvas em HEX ou ARGB?
        public static BuscaAgente BuscarAgente()
        {
            return new BuscaAgente
            {
                NomeAgente = "Cristina Pereira",
                NomeAgencia = "Pereira Travel",
                LinkAgente = "app.p2d.travel/Pereira-Travel",
                FotoAgente = "https://blog.unyleya.edu.br/wp-content/uploads/2017/12/saiba-como-a-educacao-ajuda-voce-a-ser-uma-pessoa-melhor.jpeg",
                NumeroAgente = "(41) 99999-8888",
                CorPrincipal = "#455ED2",
                CorSecundaria = "#FF930E",
                CorTextosPrincipal = "#FFFFFF",
                CorTextosSecundaria = "#FFEB0E"
            };
        }
    }
}
