using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Net.WebRequestMethods;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class PictureController
    {
        // De window die we laten zien op het scherm
        public static Home Window { get; set; }
        public static List<string> shownPhotos = new List<string> { };


        // De lijst met fotos die we laten zien
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();
        
        
        // Start methode die wordt aangeroepen wanneer de foto pagina opent.
        public void Start()
        {

            // Initializeer de lijst met fotos
            // WAARSCHUWING. ZONDER FILTER LAADT DIT ALLES!
            // foreach is een for-loop die door een array loopt
            var now = DateTime.Now;
            int day = (int)now.DayOfWeek;
            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                /**
                 * dir string is de map waar de fotos in staan. Bijvoorbeeld:
                 * \fotos\0_Zondag
                 */
                //MessageBox.Show(dir);
                if (dir.Substring(15, 1) == day.ToString())
                {
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        String minuteLater = raise(file);
                        /**
                         * file string is de file van de foto. Bijvoorbeeld:
                         * \fotos\0_Zondag\10_05_30_id8824.jpg      
                         */
                        DateTime twomin = now.AddMinutes(-2);
                        DateTime thirtymin = now.AddMinutes(-30);

                        int twostring = int.Parse(twomin.ToString().Remove(0,11).Replace(@":", ""));
                        int thirtystring = int.Parse(thirtymin.ToString().Remove(0, 11).Replace(@":", ""));
                        int filestring = int.Parse(file.Substring(25, 8).Replace(@"_", ""));

                        bool display = shownPhotos.Contains(filestring.ToString());
                        
                        display = !display;
                        

                        if (filestring <= twostring && filestring >= thirtystring && display)
                        {
                            PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file });
                            foreach (string file2 in Directory.GetFiles(dir))
                            {
                                int file2string = int.Parse(file2.Substring(25, 8).Replace(@"_", ""));

                                if (minuteLater == file2string.ToString())
                                {
                                    PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file2 });
                                }
                            }
                        }
                    }
                }
            }

            // Update de fotos
            PictureManager.UpdatePictures(PicturesToDisplay);
        }

        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {

        }
        private static string raise(string file)
        {
            if (int.Parse(file.Substring(29, 1)) == 9 && int.Parse(file.Substring(28, 1)) == 5)
            {
                string photo2 = file.Substring(25, 8).Replace(@"_", "");
                //MessageBox.Show(photo2);

                photo2 = photo2.Remove(2, 1).Insert(2, "0");
                photo2 = photo2.Remove(3, 1).Insert(3, "0");
                char secondDigit = photo2[1];
                int newSecondDigit = int.Parse(secondDigit.ToString()) + 1;
                photo2 = photo2.Remove(1, 1).Insert(1, newSecondDigit.ToString());
                shownPhotos.Add(photo2);
                return photo2;
            }
            else if (int.Parse(file.Substring(29, 1)) == 9)
            {
                string photo2 = file.Substring(25, 8).Replace(@"_", "");


                photo2 = photo2.Remove(3, 1).Insert(3, "0");
                char thirdDigit = photo2[2];
                int newThirdDigit = int.Parse(thirdDigit.ToString()) + 1;
                photo2 = photo2.Remove(2, 1).Insert(2, newThirdDigit.ToString());
                shownPhotos.Add(photo2);
                return photo2;
            }
            else
            {
                string photo2 = file.Substring(25, 8).Replace(@"_", "");
                char fourthDigit = photo2[3];
                int newFourthDigit = int.Parse(fourthDigit.ToString()) + 1;
                photo2 = photo2.Remove(3, 1).Insert(3, newFourthDigit.ToString());
                shownPhotos.Add(photo2);
                return photo2;
            }
        }

    }
}
