using System;
using System.Text;
using SmartPaymentFormat.Utilities;

namespace SmartPaymentFormat.QR
{
    public class SmartPaymentQRUtils
    {
  /*      public static BufferedImage getQRCode(int size, string paymentString)
        {
            return getQRCode(size, paymentString, true);
        }

        public static BufferedImage getQRCode(int size, string paymentString, bool hasBranging)
        {
            if (size == null)
            {
                size = SmartPaymentConstants.DefQRSize;
            }
            else if (size < SmartPaymentConstants.MinQRSize)
            {
                size = SmartPaymentConstants.MinQRSize;
            }
            else if (size > SmartPaymentConstants.MaxQRSize)
            {
                size = SmartPaymentConstants.MaxQRSize;
            }

            BitMatrix matrix = null;
            int h = size;
            int w = size;
            int barsize = -1;
            Writer writer = new MultiFormatWriter();
            try
            {
                Map<EncodeHintType, Object> hints = new EnumMap<EncodeHintType, Object>(EncodeHintType.class)
                ;
                hints.put(EncodeHintType.CHARACTER_SET, "ISO-8859-1");
                QRCode code = Encoder.encode(paymentString, ErrorCorrectionLevel.M, hints);
                hints.put(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.M);
                barsize = size/(code.getMatrix().getWidth() + 8);
                matrix = writer.encode(paymentString, com.google.zxing.BarcodeFormat.QR_CODE, w, h, hints);
            }
            catch (com.google.zxing.WriterException e)
            {
                System.out.
                println(e.getMessage());
            }

            if (matrix == null || barsize < 0)
            {
                throw new InvalidFormatException();
            }

            BufferedImage image = MatrixToImageWriter.toBufferedImage(matrix);

            if (hasBranging)
            {
                Graphics2D g = (Graphics2D) image.getGraphics();

                BasicStroke bs = new BasicStroke(2);
                g.setStroke(bs);
                g.setColor(Color.BLACK);
                g.drawLine(0, 0, w, 0);
                g.drawLine(0, 0, 0, h);
                g.drawLine(w, 0, w, h);
                g.drawLine(0, h, w, h);

                string str = "QR Platba";
                int fontSize = size/12;

                g.setFont(new Font("Verdana", Font.BOLD, fontSize));

                FontMetrics fm = g.getFontMetrics();
                Rectangle2D rect = fm.getStringBounds(str, g);

                g.setColor(Color.WHITE);
                g.fillRect(2*barsize, h - fm.getAscent(), (int) rect.getWidth() + 4*barsize, (int) rect.getHeight());

                int padding = 4*barsize;

                BufferedImage paddedImage = new BufferedImage(w + 2*padding, h + padding + (int) rect.getHeight(), image.getType());
                Graphics2D g2 = paddedImage.createGraphics();
                g2.setRenderingHint(RenderingHints.KEY_TEXT_ANTIALIASING, RenderingHints.VALUE_TEXT_ANTIALIAS_GASP);
                g2.setFont(new Font("Verdana", Font.BOLD, fontSize));
                g2.setPaint(Color.WHITE);
                g2.fillRect(0, 0, paddedImage.getWidth(), paddedImage.getHeight());
                g2.drawImage(image, padding, padding, Color.WHITE, null);

                g2.setColor(Color.BLACK);
                g2.drawString(str, padding + 4*barsize, (padding + h + rect.getHeight() - barsize));

                image = paddedImage;
            }

            return image;
        }*/
    }
}
