using System;


namespace CotacaoHotel
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = InputAgente.RecebeDadosCotacao();
            var melhorprecohotel = BuscaHotel.BuscarMelhorPreco(input.Cidade, input.Checkin, input.Checkin.AddDays(input.NumDias), input.NumEstrelas, input.NumPessoas);
            var agente = BuscaAgente.BuscarAgente();

            string Disclaim = ($"{input.NumPessoas} adultos e X crianças, com entrada no dia {melhorprecohotel.Checkin.ToString("dd/MM/yyyy")}, e saída no dia {melhorprecohotel.Checkout.ToString("dd/MM/yyyy")} no Hotel {melhorprecohotel.NomeHotel} {input.NumEstrelas} estrelas {melhorprecohotel.Extras}. Valor da diária: a partir de R${melhorprecohotel.PrecoDiaria}. Sujeito a Disponibilidade.");
            
            //Gerar Template de Imagem
            GeraImagem.GerarImagem(agente.NomeAgente, agente.NomeAgencia, agente.LinkAgente, agente.NumeroAgente, agente.FotoAgente, agente.CorPrincipal, agente.CorSecundaria, agente.CorTextosPrincipal, agente.CorTextosSecundaria, input.Cidade, melhorprecohotel.PrecoDiaria * 5, Disclaim, 170, 170, "agente.jpg", melhorprecohotel.FotoHotel);            
            
            var fotoHotel = melhorprecohotel.FotoHotel;
        }


    }
}
