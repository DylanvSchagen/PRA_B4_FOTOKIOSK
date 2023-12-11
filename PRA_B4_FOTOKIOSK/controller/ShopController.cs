using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class ShopController
    {

        public static Home Window { get; set; }

        public List<KioskProduct> Products { get; set; }

        public void Start()
        {
            // Stel de prijslijst in aan de rechter kant.
            ShopManager.SetShopPriceList("Prijzen:\nFoto 10x15: \nFoto 20x15");

            // Stel de bon in onderaan het scherm
            ShopManager.SetShopReceipt("Eindbedrag\n€");

            // Vul de productlijst met producten
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 10x15", Price = 2.55F, Description = "Foto" });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 20x15", Price = 2.80F, Description = "Foto" });


            // Update dropdown met producten
            ShopManager.UpdateDropDownProducts();

            foreach (KioskProduct product in ShopManager.Products)
            {
                // Voeg de naam van het huidige product toe aan de prijslijst
                ShopManager.AddShopPriceList(product.Price.ToString());

            }
        }





    // Wordt uitgevoerd wanneer er op de Toevoegen knop is geklikt
    public void AddButtonClick()
        {
            KioskProduct selectedProduct = ShopManager.GetSelectedProduct();

            int? fotoId = ShopManager.GetFotoId(); 
            int? amount = ShopManager.GetAmount();
            float price = selectedProduct.Price;

            string receipt = $"{price * amount}"; 
            ShopManager.AddShopReceipt(receipt);
        }

        // Wordt uitgevoerd wanneer er op de Resetten knop is geklikt
        public void ResetButtonClick()
        {
            ShopManager.SetShopReceipt("Eindbedrag\n€");
        }

        // Wordt uitgevoerd wanneer er op de Save knop is geklikt
        public void SaveButtonClick()
        {
            string receipt = ShopManager.GetShopReceipt();
            string filePath = "downloads\\receipt.txt";
            File.WriteAllText(filePath, receipt);
        }

    }
}
