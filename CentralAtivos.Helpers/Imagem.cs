using System;
using System.IO;

namespace CentralAtivos.Helpers
{
    public class Imagem
    {
        public static void SalvarImagem(string striBase64, string path)
        {
            try
            {
                var bytes = Convert.FromBase64String(striBase64);

                using (var imageFile = new FileStream(path, FileMode.Create))
                {
                    imageFile.Write(bytes, 0, bytes.Length);
                    imageFile.Flush();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
