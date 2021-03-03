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

            string Disclaim = ($"{input.NumPessoas} adultos, com entrada no dia {melhorprecohotel.Checkin.ToString("dd/MM/yyyy")}, e saída no dia {melhorprecohotel.Checkout.ToString("dd/MM/yyyy")} no Hotel {melhorprecohotel.NomeHotel} {input.NumEstrelas} estrelas {melhorprecohotel.Extras}. Valor da diária: R${melhorprecohotel.PrecoDiaria}");
            

            //Gerar Template de Imagem
            GeraImagem.GerarImagem(agente.NomeAgente, agente.NomeAgencia, agente.LinkAgente, agente.NumeroAgente, agente.FotoAgente, agente.CorPrincipal, agente.CorSecundaria, agente.CorTextosPrincipal, agente.CorTextosSecundaria, input.Cidade, melhorprecohotel.PrecoDiaria * 5, Disclaim, 170, 170, "agente.jpg", melhorprecohotel.FotoHotel);            

            //Gerar template de texto com o restante das informações do hotel, saidas, etc.
            string TemplateResposta = $"\n\nObrigado!\nPreparamos uma sugestão especialmente para você.\nConsiderando as suas necessidades:\n*{input.NumPessoas} pessoas - de {input.Checkin} a {melhorprecohotel.Checkout}* \nHotel: {melhorprecohotel.NomeHotel}\nValor: {melhorprecohotel.PrecoDiaria * input.NumDias} \nCheck in dia:{input.Checkin} \nCheck out dia:{melhorprecohotel.Checkout} \nMais Informações: {melhorprecohotel.Extras}\nGostou? Quer que eu faça uma reserva? Ou quer ver outra opção?\n\n";
            Console.WriteLine(TemplateResposta);
            //Ver como colocar as quebras de linha, usar <br>?
            
            var fotoHotel = melhorprecohotel.FotoHotel;
        }


    }
}
