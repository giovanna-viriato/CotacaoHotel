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
    class GeraImagem
    {
        public static void GerarImagem(string nomeAgente, string nomeAgencia, string linkAgente, string numeroAgente, string fotoAgente, string corPrincipal, string corSecundaria, string corTextosPrincipal, string corTextosSecundaria, string Destino, double Preco, string Disclaim, int widthAgente, int heightAgente, string fileName, string fotoHotel)

        {

            Bitmap imagemDestino = new Bitmap("SantaCatarina.jpg");
            Graphics graphics = Graphics.FromImage(imagemDestino);

            //Código para buscar a imagem pelo URL e converter para bitmap

            /*System.Net.WebRequest request = System.Net.WebRequest.Create(fotoHotel);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();
            Bitmap imagemDestino = new Bitmap(responseStream);
            Graphics graphics = Graphics.FromImage(imagemDestino);*/

            //Cria os brushes com as cores
            SolidBrush brushTextos = new SolidBrush(ColorTranslator.FromHtml(corTextosPrincipal));
            SolidBrush brushFundoBaixo = new SolidBrush(ColorTranslator.FromHtml(corPrincipal));
            SolidBrush brushFundoCima = new SolidBrush(ColorTranslator.FromHtml(corSecundaria));
            SolidBrush brushTextos2 = new SolidBrush(ColorTranslator.FromHtml(corTextosSecundaria));

            //Puxar arquivo da imagem do Agente e redimensionar
            ResizeImage(fileName, widthAgente, heightAgente, fotoAgente, brushFundoCima);
            Image imagemAgente = Image.FromFile("Resized." + fileName);

            //Pegar tamanho da Imagem do Agente
            float realWidthAgente = imagemAgente.Width;
            float realHeightAgente = imagemAgente.Height;

            //Pegar tamanho da Imagem de Destino
            float destinoWidth = imagemDestino.Width;
            float destinoHeight = imagemDestino.Height;

            //Definir o Grid
            float gridW = (destinoWidth / 50);
            float gridH = (destinoHeight / 50);


            int preco12 = Convert.ToInt32(Preco / 12 * 1.0459F);
            // Cria as strings que estarão na imagem
            String escreveNomeAgente = nomeAgente;
            String escreveLinkAgente = linkAgente.ToUpper();
            String escreveDestino = Destino.ToUpper();
            String escrevePreco = "R$" + (Preco.ToString()).ToUpper();
            String apartir = "A partir de";
            String escreveDisclaim = "*" + Disclaim;
            String escreveNumero = numeroAgente.ToUpper();
            String escreveAgencia = nomeAgencia.ToUpper();
            String escreveDias = "HOTEL, 5 NOITES";
            String escreve12X = "OU 12X DE R$" + preco12.ToString();

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
            Font fonteAgente = new Font(kanitMedium, 28);
            Font fonteLink = new Font(kanitMedium, 16);
            Font fonteDestino = new Font(kanitSemiBold, 38);
            Font fontePreco = new Font(kanitSemiBold, 50);
            Font fonteDisclaim = new Font(kanitMedium, 12);
            Font fonteAgencia = new Font(kanitBold, 24);
            Font fonteApartir = new Font(kanitSemiBold, 14);
            Font fonte12x = new Font(kanitMedium, 24);

            //Calcular os tamanhos das strings

            //LinkAgente
            SizeF tamanhoLink = new SizeF();
            tamanhoLink = graphics.MeasureString(escreveLinkAgente, fonteLink);
            //Whatsapp
            SizeF tamanhoNumero = new SizeF();
            tamanhoNumero = graphics.MeasureString(escreveNumero, fonteLink);
            //Preco
            SizeF tamanhoPreco = new SizeF();
            tamanhoPreco = graphics.MeasureString(escrevePreco, fontePreco);
            //Agente
            SizeF tamanhoAgente = new SizeF();
            tamanhoAgente = graphics.MeasureString(escreveNomeAgente, fonteAgente);
            //Destino
            SizeF tamanhoDestino = new SizeF();
            tamanhoDestino = graphics.MeasureString(escreveDestino, fonteDestino);
            //Disclaim
            SizeF tamanhoDisclaim = new SizeF();
            tamanhoDisclaim = graphics.MeasureString(Disclaim, fonteDisclaim);
            //Apartir
            SizeF tamanhoApartir = new SizeF();
            tamanhoApartir = graphics.MeasureString(apartir, fonteApartir);
            //Agencia
            SizeF tamanhoAgencia = new SizeF();
            tamanhoAgencia = graphics.MeasureString(escreveAgencia, fonteAgencia);
            //Hotel, 5 noites
            SizeF tamanhoDias = new SizeF();
            tamanhoDias = graphics.MeasureString(escreveDias, fonte12x);
            //12x de
            SizeF tamanho12x = new SizeF();
            tamanho12x = graphics.MeasureString(escreve12X, fonte12x);


            //Verificar o que é maior, o nome do agente ou o link
            float maiorstring;
            if (tamanhoAgente.Width > tamanhoLink.Width)
            {
                maiorstring = tamanhoAgente.Width;
            }
            else
            {
                maiorstring = tamanhoLink.Width;
            }
            if (maiorstring < tamanhoNumero.Width) { maiorstring = tamanhoNumero.Width + gridW; }

            //Verificar o que é maior, o nome da agencia ou do destino
            float maiorS, widthAgencia, widthDestino, linhaAdicionalA = 0, linhaAdicionalD = 0, linha3 = 0;
            if ((tamanhoAgencia.Width >= tamanhoDestino.Width) && nomeAgencia.Length <= 22)
            {
                maiorS = tamanhoAgencia.Width;
                widthAgencia = tamanhoAgencia.Width;
                widthDestino = tamanhoDestino.Width;
            }
            else if ((tamanhoAgencia.Width < tamanhoDestino.Width) && tamanhoDestino.Width <= 503)
            {
                maiorS = tamanhoDestino.Width;
                widthDestino = tamanhoDestino.Width;
                widthAgencia = tamanhoAgencia.Width;
            }
            else if ((tamanhoAgencia.Width >= tamanhoDestino.Width) && nomeAgencia.Length > 22)
            {
                maiorS = 500;
                widthAgencia = 500;
                linhaAdicionalA = tamanhoAgencia.Height * 0.8F;
                if (tamanhoDestino.Width > 503)
                {
                    widthDestino = 500;
                    linhaAdicionalD = tamanhoDestino.Height * 0.8F;
                }
                else { widthDestino = tamanhoDestino.Width; }
            }
            else if ((tamanhoAgencia.Width < tamanhoDestino.Width) && (tamanhoDestino.Width > 503 && tamanhoDestino.Width < 1006))
            {
                maiorS = 500;
                widthDestino = 500;
                linhaAdicionalD = tamanhoDestino.Height * 0.8F;
                if (nomeAgencia.Length > 22)
                {
                    widthAgencia = 500;
                    linhaAdicionalA = tamanhoAgencia.Height * 0.8F;
                }
                else { widthAgencia = tamanhoDestino.Width; }
            }
            else if ((tamanhoAgencia.Width < tamanhoDestino.Width) && (tamanhoDestino.Width >= 1006 && tamanhoDestino.Width < 1509))
            {
                maiorS = 500;
                widthDestino = 500;
                linhaAdicionalD = tamanhoDestino.Height * 1.6F;

                if (nomeAgencia.Length > 23)
                {
                    widthAgencia = 500;
                    linhaAdicionalA = tamanhoAgencia.Height * 0.8F;
                }
                else { widthAgencia = tamanhoDestino.Width; }
            }
            else if ((tamanhoAgencia.Width < tamanhoDestino.Width) && tamanhoDestino.Width >= 2012)
            {
                maiorS = 500;
                widthDestino = 500;
                linhaAdicionalD = tamanhoDestino.Height * 2.4F;
                linha3 = tamanhoDestino.Height * 0.4F;
                if (nomeAgencia.Length > 23)
                {
                    widthAgencia = 500;
                    linhaAdicionalA = tamanhoAgencia.Height * 0.8F;
                }
                else { widthAgencia = tamanhoAgencia.Width; }
            }
            else
            {
                maiorS = tamanhoAgencia.Width;
                widthDestino = tamanhoDestino.Width;
                widthAgencia = tamanhoAgencia.Width;
            }

            if (maiorS < 350) { maiorS = 350; }

            // Criar retangulos para as strings.
            //Retangulo de Baixo
            float x8 = destinoWidth - (maiorS + gridW * 3.5F);
            float y8 = (destinoHeight - (tamanhoAgencia.Height + linhaAdicionalA + tamanhoApartir.Height + tamanhoDestino.Height + linhaAdicionalD + tamanhoDias.Height + tamanhoPreco.Height + tamanhoDisclaim.Height * 2 + (gridH * 6)));
            float width8 = maiorS + gridW;
            float height8 = (tamanhoAgencia.Height + linhaAdicionalA + tamanhoApartir.Height + tamanhoDestino.Height + linhaAdicionalD + tamanhoDias.Height + tamanhoPreco.Height + tamanhoDisclaim.Height * 2 +(gridH * 6));
            RectangleF drawRect8 = new RectangleF(x8, y8, width8, height8);

            //Destino
            float x = x8 + (gridW * 0.9F);
            float y = y8 + tamanhoAgencia.Height + linhaAdicionalA + gridH * 0.8F;
            float width = widthDestino;
            float height = tamanhoDestino.Height + linhaAdicionalD;
            RectangleF drawRect = new RectangleF(x, y, width, height);

            //Link
            float x1 = realWidthAgente + (gridW * 3);
            float y1 = gridH * 5 + tamanhoAgente.Height;
            float width1 = tamanhoLink.Width;
            float height1 = tamanhoLink.Height;
            RectangleF drawRect1 = new RectangleF(x1, y1, width1, height1);

            //Whatsapp
            float x5 = realWidthAgente + (gridW * 3);
            float y5 = tamanhoAgente.Height + tamanhoLink.Height + gridH * 5;
            float width5 = tamanhoNumero.Width;
            float height5 = tamanhoNumero.Height;
            RectangleF drawRect5 = new RectangleF(x5, y5, width5, height5);

            //Retangulo de cima
            float x7 = 0F;
            float y7 = gridH * 3;
            float width7 = realWidthAgente + maiorstring + gridW * 4;
            float height7 = tamanhoAgente.Height + tamanhoLink.Height + tamanhoNumero.Height + gridH * 3;
            RectangleF drawRect7 = new RectangleF(x7, y7, width7, height7);

            //Nome Agente
            float eixox = realWidthAgente + (gridW * 3);
            float eixoy = y7 + gridH;
            float largura = tamanhoAgente.Width;
            float altura = tamanhoAgente.Height;
            RectangleF desenhaRet = new RectangleF(eixox, eixoy, largura, altura);

            //Nome Agencia
            float x10 = x8 + (gridW * 1);
            float y10 = y8 + (gridH * 1);
            float width10 = widthAgencia;
            float height10 = tamanhoAgencia.Height + linhaAdicionalA;
            RectangleF drawRect10 = new RectangleF(x10, y10, width10, height10);

            //Hotel, 5 noites
            float x11 = x8 + gridW;
            float y11 = (y + (graphics.MeasureString(escreveDestino, fonteDestino)).Height + linhaAdicionalD * 1.3F + linha3) - (gridH * 0.3F);
            float width11 = tamanhoDias.Width;
            float height11 = tamanhoDias.Height;
            RectangleF drawRect11 = new RectangleF(x11, y11, width11, height11);

            //A partir
            float x9 = x8 + (gridW * 1.1F);
            float y9 = y11 + tamanhoDias.Height;
            float width9 = tamanhoApartir.Width;
            float height9 = tamanhoApartir.Height;
            RectangleF drawRect9 = new RectangleF(x9, y9, width9, height9);

            //Preço
            float x2 = x8 + (gridW * 0.65F);
            float y2 = y9 + tamanhoApartir.Height - gridH * 0.5F; 
            float width2 = tamanhoPreco.Width;
            float height2 = tamanhoPreco.Height;
            RectangleF drawRect2 = new RectangleF(x2, y2, width2, height2);

            //12x de
            float x12 = x8 + gridW;
            float y12 = y2 + (tamanhoPreco.Height * 0.8f);
            float width12 = tamanho12x.Width;
            float height12 = tamanho12x.Height;
            RectangleF drawRect12 = new RectangleF(x12, y12, width12, height12);

            //Disclaim
            float width4 = width8 - gridW * 1.6F;
            float height4 = tamanhoDisclaim.Height * 5;
            float x4 = x8 + (gridW * 1.165F);
            float y4 = y12 + tamanho12x.Height; 
            RectangleF drawRect4 = new RectangleF(x4, y4, width4, height4);


            //Cria as Pens para desenhar as formas
            Pen penBaixo = new Pen(ColorTranslator.FromHtml(corPrincipal));
            Pen penCima = new Pen(ColorTranslator.FromHtml(corSecundaria));


            //Puxa a função para criar os retangulos
            Rectangle RetanguloCima = new Rectangle(Convert.ToInt32(x7), Convert.ToInt32(y7), Convert.ToInt32(width7), Convert.ToInt32(height7));

            using (GraphicsPath path = ArredondarRetangulo.CreateRoundedRectangle(RetanguloCima, 0, 20, 20, 0))
            {
                graphics.FillPath(brushFundoCima, path);
            }

            using (GraphicsPath path = ArredondarRetangulo.CreateRoundedRectangle(RetanguloCima, 0, 20, 20, 0))
            {
                graphics.DrawPath(penCima, path);
            }

            Rectangle RetanguloBaixo = new Rectangle(Convert.ToInt32(x8), Convert.ToInt32(y8), Convert.ToInt32(width8), Convert.ToInt32(height8));

            using (GraphicsPath path = ArredondarRetangulo.CreateRoundedRectangle(RetanguloBaixo, 20, 20, 0, 0))
            {
                graphics.FillPath(brushFundoBaixo, path);
            }

            using (GraphicsPath path = ArredondarRetangulo.CreateRoundedRectangle(RetanguloBaixo, 20, 20, 0, 0))
            {
                graphics.DrawPath(penBaixo, path);
            }

            // Desenha as strings na tela
            graphics.DrawString(escreveNomeAgente, fonteAgente, brushTextos, desenhaRet);
            graphics.DrawString(escreveDestino, fonteDestino, brushTextos, drawRect);
            graphics.DrawString(escreveLinkAgente, fonteLink, brushFundoBaixo, drawRect1);
            graphics.DrawString(escrevePreco, fontePreco, brushTextos2, drawRect2);
            graphics.DrawString(Disclaim, fonteDisclaim, brushTextos, drawRect4);
            graphics.DrawString(escreveNumero, fonteLink, brushFundoBaixo, drawRect5);
            graphics.DrawString(apartir, fonteApartir, brushTextos, drawRect9);
            graphics.DrawString(escreveAgencia, fonteAgencia, brushTextos2, drawRect10);
            graphics.DrawString(escreveDias, fonte12x, brushTextos2, drawRect11);
            graphics.DrawString(escreve12X, fonte12x, brushTextos2, drawRect12);


            //centralizar a imagem no retangulo
            float borda = ((RetanguloCima.Height) - realHeightAgente) / 2;

            //Colocar imagem do agente na imagem de destino            
            graphics.DrawImage(imagemAgente, gridW * 2, (y7 + borda));

            //Save the drawing into desired image format
            imagemDestino.Save(@$"campanha_{Destino.Replace(' ', '_')}_{nomeAgencia.Replace(' ', '_')}_1080x1080.jpg");
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


        public static void ResizeImage(string originalFilename, int canvasWidth, int canvasHeight, string fotoAgente, SolidBrush fundo)
        {
            //Image imagemAgente = Image.FromFile("agente.jpg");

            System.Net.WebRequest request = System.Net.WebRequest.Create(fotoAgente);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();
            Bitmap imagemAgente = new Bitmap(responseStream);
            Graphics graphics = Graphics.FromImage(imagemAgente);

            Image thumbnail = new Bitmap(canvasWidth, canvasHeight);
            Graphics graphic = Graphics.FromImage(thumbnail);

            // Descobrir a proporção
            double ratioX = (double)canvasWidth / (double)imagemAgente.Width;
            double ratioY = (double)canvasHeight / (double)imagemAgente.Height;
            // Usar o menor multiplicador
            double ratio = ratioX < ratioY ? ratioX : ratioY;

            // Pegar a nova altura e nova largura
            int newHeight = Convert.ToInt32(imagemAgente.Height * ratio);
            int newWidth = Convert.ToInt32(imagemAgente.Width * ratio);

            // Calcular a posição do canto superior esquerdo
            int posX = Convert.ToInt32((canvasWidth - (imagemAgente.Width * ratio)) / 2);
            int posY = Convert.ToInt32((canvasHeight - (imagemAgente.Height * ratio)) / 2);

            graphic.Clear(fundo.Color); //Cor das bordas
            graphic.DrawImage(imagemAgente, posX, posY, newWidth, newHeight);

            thumbnail.Save("Resized." + originalFilename);
        }
    }
}
