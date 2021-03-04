using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CotacaoHotel
{
    class GeneratesImage
    {
        public static void GenerateImage(string partnersName, string agencyName, string partnersLink, string partnersNumber, string partnersPhoto, string destination, double price, string disclaim, int partnersPhotoWidth, int partnersPhotoHeight, string fileName, string hotelPhoto)

        {

            Bitmap destinationImage = new Bitmap("SantaCatarina.jpg");
            Graphics graphics = Graphics.FromImage(destinationImage);

            //Código para buscar a imagem pelo URL e converter para bitmap

            /*System.Net.WebRequest request = System.Net.WebRequest.Create(hotelPhoto);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();
            Bitmap imagemDestino = new Bitmap(responseStream);
            Graphics graphics = Graphics.FromImage(imagemDestino);*/

            //Cria os brushes com as cores
            SolidBrush whiteBrush = new SolidBrush(ColorTranslator.FromHtml("#FFFFFF"));
            SolidBrush blueBrush = new SolidBrush(ColorTranslator.FromHtml("#455ED2"));
            SolidBrush orangeBrush = new SolidBrush(ColorTranslator.FromHtml("#FF930E"));
            SolidBrush yellowBrush = new SolidBrush(ColorTranslator.FromHtml("#FFEB0E"));

            //Puxar arquivo da imagem do Agente e redimensionar
            ResizeImage(fileName, partnersPhotoWidth, partnersPhotoHeight, partnersPhoto, orangeBrush);
            Image resizedPartnerImage = Image.FromFile("Resized." + fileName);

            //Pegar tamanho da Imagem do Agente
            float widthResizedPartnerImage = resizedPartnerImage.Width;
            float heightResizedPartnerImage = resizedPartnerImage.Height;

            //Pegar tamanho da Imagem de Destino
            float destinationWidth = destinationImage.Width;
            float destinationHeight = destinationImage.Height;

            //Definir o Grid
            float widthGrid = (destinationWidth / 50);
            float heightGrid = (destinationHeight / 50);


            int priceBy12 = Convert.ToInt32(price / 12 * 1.0459F);
            // Cria as strings que estarão na imagem
            String writePartnersName = partnersName;
            String writePartnersLink = partnersLink.ToUpper();
            String writeDestination = destination.ToUpper();
            String writePrice = "R$" + (price.ToString()).ToUpper();
            String writeFrom = "A partir de";
            String writeDisclaim = "*" + disclaim;
            String writeNumber = partnersNumber.ToUpper();
            String writeAgencyName = agencyName.ToUpper();
            String writeNumberOfNights = "HOTEL, 5 NOITES";
            String write12XFrom = "OU 12X DE R$" + priceBy12.ToString();

            // Cria e puxa as fontes de resources
            var medium = new PrivateFontCollection();
            var semibold = new PrivateFontCollection();
            var bold = new PrivateFontCollection();

            AddFontFromResource(medium, "CotacaoHotel.Kanit.Kanit-Medium.ttf");
            FontFamily kanitMedium = medium.Families[0];

            AddFontFromResource(semibold, "CotacaoHotel.Kanit.Kanit-SemiBold.ttf");
            FontFamily kanitSemiBold = semibold.Families[0];

            AddFontFromResource(bold, "CotacaoHotel.Kanit.Kanit-Bold.ttf");
            FontFamily kanitBold = bold.Families[0];

            // Define tamanho e fonte das strings
            Font partnersFont = new Font(kanitMedium, 28);
            Font linkFont = new Font(kanitMedium, 16);
            Font destinationFont = new Font(kanitSemiBold, 38);
            Font priceFont = new Font(kanitSemiBold, 50);
            Font disclaimFont = new Font(kanitMedium, 12);
            Font agencyFont = new Font(kanitBold, 24);
            Font fromFont = new Font(kanitSemiBold, 14);
            Font or12XFont = new Font(kanitMedium, 24);

            //Calcular os tamanhos das strings

            //LinkAgente
            SizeF linkSize = new SizeF();
            linkSize = graphics.MeasureString(writePartnersLink, linkFont);
            //Whatsapp
            SizeF numberSize = new SizeF();
            numberSize = graphics.MeasureString(writeNumber, linkFont);
            //Preco
            SizeF priceSize = new SizeF();
            priceSize = graphics.MeasureString(writePrice, priceFont);
            //Agente
            SizeF partnerNameSize = new SizeF();
            partnerNameSize = graphics.MeasureString(writePartnersName, partnersFont);
            //Destino
            SizeF destinationSize = new SizeF();
            destinationSize = graphics.MeasureString(writeDestination, destinationFont);
            //Disclaim
            SizeF disclaimSize = new SizeF();
            disclaimSize = graphics.MeasureString(disclaim, disclaimFont);
            //Apartir
            SizeF fromSize = new SizeF();
            fromSize = graphics.MeasureString(writeFrom, fromFont);
            //Agencia
            SizeF agencyNameSize = new SizeF();
            agencyNameSize = graphics.MeasureString(writeAgencyName, agencyFont);
            //Hotel, 5 noites
            SizeF numberOfNightsSize = new SizeF();
            numberOfNightsSize = graphics.MeasureString(writeNumberOfNights, or12XFont);
            //12x de
            SizeF write12xSize = new SizeF();
            write12xSize = graphics.MeasureString(write12XFrom, or12XFont);


            //Verificar o que é maior, o nome do agente ou o link
            float biggerString;
            if (partnerNameSize.Width > linkSize.Width)
            {
                biggerString = partnerNameSize.Width;
            }
            else
            {
                biggerString = linkSize.Width;
            }
            if (biggerString < numberSize.Width) { biggerString = numberSize.Width + widthGrid; }

            //Verificar o que é maior, o nome da agencia ou do destino
            float biggerString2, agencyWidth, widthDestino, additionalLineAgency = 0, additionalLineDestination = 0, thirdLine = 0;
            if ((agencyNameSize.Width >= destinationSize.Width) && agencyName.Length <= 22)
            {
                biggerString2 = agencyNameSize.Width;
                agencyWidth = agencyNameSize.Width;
                widthDestino = destinationSize.Width;
            }
            else if ((agencyNameSize.Width < destinationSize.Width) && destinationSize.Width <= 503)
            {
                biggerString2 = destinationSize.Width;
                widthDestino = destinationSize.Width;
                agencyWidth = agencyNameSize.Width;
            }
            else if ((agencyNameSize.Width >= destinationSize.Width) && agencyName.Length > 22)
            {
                biggerString2 = 500;
                agencyWidth = 500;
                additionalLineAgency = agencyNameSize.Height * 0.8F;
                if (destinationSize.Width > 503)
                {
                    widthDestino = 500;
                    additionalLineDestination = destinationSize.Height * 0.8F;
                }
                else { widthDestino = destinationSize.Width; }
            }
            else if ((agencyNameSize.Width < destinationSize.Width) && (destinationSize.Width > 503 && destinationSize.Width < 1006))
            {
                biggerString2 = 500;
                widthDestino = 500;
                additionalLineDestination = destinationSize.Height * 0.8F;
                if (agencyName.Length > 22)
                {
                    agencyWidth = 500;
                    additionalLineAgency = agencyNameSize.Height * 0.8F;
                }
                else { agencyWidth = destinationSize.Width; }
            }
            else if ((agencyNameSize.Width < destinationSize.Width) && (destinationSize.Width >= 1006 && destinationSize.Width < 1509))
            {
                biggerString2 = 500;
                widthDestino = 500;
                additionalLineDestination = destinationSize.Height * 1.6F;

                if (agencyName.Length > 23)
                {
                    agencyWidth = 500;
                    additionalLineAgency = agencyNameSize.Height * 0.8F;
                }
                else { agencyWidth = destinationSize.Width; }
            }
            else if ((agencyNameSize.Width < destinationSize.Width) && destinationSize.Width >= 2012)
            {
                biggerString2 = 500;
                widthDestino = 500;
                additionalLineDestination = destinationSize.Height * 2.4F;
                thirdLine = destinationSize.Height * 0.4F;
                if (agencyName.Length > 23)
                {
                    agencyWidth = 500;
                    additionalLineAgency = agencyNameSize.Height * 0.8F;
                }
                else { agencyWidth = agencyNameSize.Width; }
            }
            else
            {
                biggerString2 = agencyNameSize.Width;
                widthDestino = destinationSize.Width;
                agencyWidth = agencyNameSize.Width;
            }

            if (biggerString2 < 350) { biggerString2 = 350; }

            // Criar retangulos para as strings.
            //Retangulo de Baixo
            float xLowerRectangle = destinationWidth - (biggerString2 + widthGrid * 3.5F);
            float yLowerRectangle = (destinationHeight - (agencyNameSize.Height + additionalLineAgency + fromSize.Height + destinationSize.Height + additionalLineDestination + numberOfNightsSize.Height + priceSize.Height + disclaimSize.Height * 2 + (heightGrid * 6)));
            float widthLowerRectangle = biggerString2 + widthGrid;
            float heightLowerRectangle = (agencyNameSize.Height + additionalLineAgency + fromSize.Height + destinationSize.Height + additionalLineDestination + numberOfNightsSize.Height + priceSize.Height + disclaimSize.Height * 2 +(heightGrid * 6));
            RectangleF lowerRectangleLimitations = new RectangleF(xLowerRectangle, yLowerRectangle, widthLowerRectangle, heightLowerRectangle);

            //Destino
            float xDestinationRectangle = xLowerRectangle + (widthGrid * 0.9F);
            float yDestinationRectangle = yLowerRectangle + agencyNameSize.Height + additionalLineAgency + heightGrid * 0.8F;
            float widthDestinationRectangle = widthDestino;
            float heightDestinationRectangle = destinationSize.Height + additionalLineDestination;
            RectangleF DestinationRectangle = new RectangleF(xDestinationRectangle, yDestinationRectangle, widthDestinationRectangle, heightDestinationRectangle);

            //Link
            float xLinkRectangle = widthResizedPartnerImage + (widthGrid * 3);
            float yLinkRectangle = heightGrid * 5 + partnerNameSize.Height;
            float widthLinkRectangle = linkSize.Width;
            float heightLinkRectangle = linkSize.Height;
            RectangleF LinkRectangle = new RectangleF(xLinkRectangle, yLinkRectangle, widthLinkRectangle, heightLinkRectangle);

            //Whatsapp
            float xNumberRectangle = widthResizedPartnerImage + (widthGrid * 3);
            float yNumberRectangle = partnerNameSize.Height + linkSize.Height + heightGrid * 5;
            float widthNumberRectangle = numberSize.Width;
            float heightNumberRectangle = numberSize.Height;
            RectangleF NumberRectangle = new RectangleF(xNumberRectangle, yNumberRectangle, widthNumberRectangle, heightNumberRectangle);

            //Retangulo de cima
            float xUpperRectangle = 0F;
            float yUpperRectangle = heightGrid * 3;
            float widthUpperRectangle = widthResizedPartnerImage + biggerString + widthGrid * 4;
            float heightUpperRectangle = partnerNameSize.Height + linkSize.Height + numberSize.Height + heightGrid * 3;
            RectangleF upperRectangleLimitations = new RectangleF(xUpperRectangle, yUpperRectangle, widthUpperRectangle, heightUpperRectangle);

            //Nome Parceiro
            float xPartnerRectangle = widthResizedPartnerImage + (widthGrid * 3);
            float yPartnerRectangle = yUpperRectangle + heightGrid;
            float widthPartnerRectangle = partnerNameSize.Width;
            float heightPartnerRectangle = partnerNameSize.Height;
            RectangleF PartnerRectangle = new RectangleF(xPartnerRectangle, yPartnerRectangle, widthPartnerRectangle, heightPartnerRectangle);

            //Nome Agencia
            float xAgencyRectangle = xLowerRectangle + (widthGrid * 1);
            float yAgencyRectangle = yLowerRectangle + (heightGrid * 1);
            float widthAgencyRectangle = agencyWidth;
            float heightAgencyRectangle = agencyNameSize.Height + additionalLineAgency;
            RectangleF AgencyRectangle = new RectangleF(xAgencyRectangle, yAgencyRectangle, widthAgencyRectangle, heightAgencyRectangle);

            //Hotel, 5 noites
            float xHotelRectangle = xLowerRectangle + widthGrid;
            float yHotelRectangle = (yDestinationRectangle + (graphics.MeasureString(writeDestination, destinationFont)).Height + additionalLineDestination * 1.3F + thirdLine) - (heightGrid * 0.3F);
            float widthHotelRectangle = numberOfNightsSize.Width;
            float heightHotelRectangle = numberOfNightsSize.Height;
            RectangleF HotelRectangle = new RectangleF(xHotelRectangle, yHotelRectangle, widthHotelRectangle, heightHotelRectangle);

            //A partir
            float xFromRectangle = xLowerRectangle + (widthGrid * 1.1F);
            float yFromRectangle = yHotelRectangle + numberOfNightsSize.Height;
            float widthFromRectangle = fromSize.Width;
            float heightFromRectangle = fromSize.Height;
            RectangleF FromRectangle = new RectangleF(xFromRectangle, yFromRectangle, widthFromRectangle, heightFromRectangle);

            //Preço
            float xPriceRectangle = xLowerRectangle + (widthGrid * 0.65F);
            float yPriceRectangle = yFromRectangle + fromSize.Height - heightGrid * 0.5F; 
            float widthPriceRectangle = priceSize.Width;
            float heightPriceRectangle = priceSize.Height;
            RectangleF PriceRectangle = new RectangleF(xPriceRectangle, yPriceRectangle, widthPriceRectangle, heightPriceRectangle);

            //12x de
            float xPriceBy12Rectangle = xLowerRectangle + widthGrid;
            float yPriceBy12Rectangle = yPriceRectangle + (priceSize.Height * 0.8f);
            float widthPriceBy12Rectangle = write12xSize.Width;
            float heightPriceBy12Rectangle = write12xSize.Height;
            RectangleF PriceBy12Rectangle = new RectangleF(xPriceBy12Rectangle, yPriceBy12Rectangle, widthPriceBy12Rectangle, heightPriceBy12Rectangle);

            //Disclaim
            float widthDisclaimRectangle = widthLowerRectangle - widthGrid * 1.6F;
            float heightDisclaimRectangle = disclaimSize.Height * 5;
            float xDisclaimRectangle = xLowerRectangle + (widthGrid * 1.165F);
            float yDisclaimRectangle = yPriceBy12Rectangle + write12xSize.Height; 
            RectangleF DisclaimRectangle = new RectangleF(xDisclaimRectangle, yDisclaimRectangle, widthDisclaimRectangle, heightDisclaimRectangle);


            //Cria as Pens para desenhar as formas
            Pen penLowerRectangle = new Pen(ColorTranslator.FromHtml("#455ED2"));
            Pen penUpperRectangle = new Pen(ColorTranslator.FromHtml("#FF930E"));


            //Puxa a função para criar os retangulos
            Rectangle upperRectangle = new Rectangle(Convert.ToInt32(xUpperRectangle), Convert.ToInt32(yUpperRectangle), Convert.ToInt32(widthUpperRectangle), Convert.ToInt32(heightUpperRectangle));

            using (GraphicsPath path = RoundRectangle.CreateRoundedRectangle(upperRectangle, 0, 20, 20, 0))
            {
                graphics.FillPath(orangeBrush, path);
            }

            using (GraphicsPath path = RoundRectangle.CreateRoundedRectangle(upperRectangle, 0, 20, 20, 0))
            {
                graphics.DrawPath(penUpperRectangle, path);
            }

            Rectangle lowerRectangle = new Rectangle(Convert.ToInt32(xLowerRectangle), Convert.ToInt32(yLowerRectangle), Convert.ToInt32(widthLowerRectangle), Convert.ToInt32(heightLowerRectangle));

            using (GraphicsPath path = RoundRectangle.CreateRoundedRectangle(lowerRectangle, 20, 20, 0, 0))
            {
                graphics.FillPath(blueBrush, path);
            }

            using (GraphicsPath path = RoundRectangle.CreateRoundedRectangle(lowerRectangle, 20, 20, 0, 0))
            {
                graphics.DrawPath(penLowerRectangle, path);
            }

            // Desenha as strings na tela
            graphics.DrawString(writePartnersName, partnersFont, whiteBrush, PartnerRectangle);
            graphics.DrawString(writeDestination, destinationFont, whiteBrush, DestinationRectangle);
            graphics.DrawString(writePartnersLink, linkFont, blueBrush, LinkRectangle);
            graphics.DrawString(writePrice, priceFont, yellowBrush, PriceRectangle);
            graphics.DrawString(disclaim, disclaimFont, whiteBrush, DisclaimRectangle);
            graphics.DrawString(writeNumber, linkFont, blueBrush, NumberRectangle);
            graphics.DrawString(writeFrom, fromFont, whiteBrush, FromRectangle);
            graphics.DrawString(writeAgencyName, agencyFont, yellowBrush, AgencyRectangle);
            graphics.DrawString(writeNumberOfNights, or12XFont, yellowBrush, HotelRectangle);
            graphics.DrawString(write12XFrom, or12XFont, yellowBrush, PriceBy12Rectangle);


            //centralizar a imagem no retangulo
            float border = ((upperRectangle.Height) - heightResizedPartnerImage) / 2;

            //Colocar imagem do agente na imagem de destino            
            graphics.DrawImage(resizedPartnerImage, widthGrid * 2, (yUpperRectangle + border));

            //Save the drawing into desired image format
            destinationImage.Save(@$"campanha_{destination.Replace(' ', '_')}_{agencyName.Replace(' ', '_')}_1080x1080.jpg");
            //"campanha_" + Destino + "_" + nomeAgente + ".jpg"

        }

        public static byte[] ReadResource(string resName)
        {
            var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resName);
            var fontBytes = new byte[resourceStream.Length];
            resourceStream.Read(fontBytes, 0, (int)resourceStream.Length);
            resourceStream.Close();
            return fontBytes;

        }

        private static void AddFontFromResource(PrivateFontCollection privateFontCollection, string fontResourceName)
        {
            var fontBytes = ReadResource(fontResourceName);
            var fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
            Marshal.Copy(fontBytes, 0, fontData, fontBytes.Length);
            privateFontCollection.AddMemoryFont(fontData, fontBytes.Length);
            // Marshal.FreeCoTaskMem(fontData);  Nasty bug alert, read the comment
        }


        public static void ResizeImage(string originalFilename, int canvasWidth, int canvasHeight, string partnerPhoto, SolidBrush background)
        {
            //Image imagemAgente = Image.FromFile("agente.jpg");

            System.Net.WebRequest request = System.Net.WebRequest.Create(partnerPhoto);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();
            Bitmap partnerImage = new Bitmap(responseStream);
            Graphics graphics = Graphics.FromImage(partnerImage);

            Image thumbnail = new Bitmap(canvasWidth, canvasHeight);
            Graphics graphic = Graphics.FromImage(thumbnail);

            // Descobrir a proporção
            double ratioX = (double)canvasWidth / (double)partnerImage.Width;
            double ratioY = (double)canvasHeight / (double)partnerImage.Height;
            // Usar o menor multiplicador
            double ratio = ratioX < ratioY ? ratioX : ratioY;

            // Pegar a nova altura e nova largura
            int newHeight = Convert.ToInt32(partnerImage.Height * ratio);
            int newWidth = Convert.ToInt32(partnerImage.Width * ratio);

            // Calcular a posição do canto superior esquerdo
            int posX = Convert.ToInt32((canvasWidth - (partnerImage.Width * ratio)) / 2);
            int posY = Convert.ToInt32((canvasHeight - (partnerImage.Height * ratio)) / 2);

            graphic.Clear(background.Color); //Cor das bordas
            graphic.DrawImage(partnerImage, posX, posY, newWidth, newHeight);

            thumbnail.Save("Resized." + originalFilename);
        }
    }
}
