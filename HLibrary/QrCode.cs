using QRCoder;
using System;
using System.Drawing;
using System.IO;

namespace HLibrary
{
    public static class QrCode
    {
        public static Byte[] GerarImagem(string qrTexto)
        {
            var qrGen = new QRCodeGenerator();
            var qrCode = qrGen.CreateQrCode(qrTexto, QRCodeGenerator.ECCLevel.Q);
            var qrBmp = new BitmapByteQRCode(qrCode);
            var qrCodeImage = qrBmp.GetGraphic(20);

            return qrBmp.GetGraphic(20);
        }
    }
}
