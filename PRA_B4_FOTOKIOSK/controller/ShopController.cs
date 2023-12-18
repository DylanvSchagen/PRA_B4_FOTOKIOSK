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
        public List<OrderProduct> OrderedProducts { get; set; }
        public ShopController()
        {
            // Initialize OrderedProducts
            OrderedProducts = new List<OrderProduct>();
        }

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

            foreach (OrderProduct product in OrderedProducts)
            {
                ShopManager.AddShopPriceList(product.FotoId.ToString());
                ShopManager.AddShopPriceList(product.ProductName.ToString());
                ShopManager.AddShopPriceList(product.Amount.ToString());
                ShopManager.AddShopPriceList(product.PriceTotal.ToString());


            }

        }
        // Wordt uitgevoerd wanneer er op de Toevoegen knop is geklikt
        public void AddButtonClick()
        {
            KioskProduct selectedProduct = ShopManager.GetSelectedProduct();
            string productName = selectedProduct.Name;
            int? fotoId = ShopManager.GetFotoId();
            int? amount = ShopManager.GetAmount();
            float price = selectedProduct.Price;

            // Create a new OrderProduct instance
            OrderProduct newOrderProduct = new OrderProduct
            {
                FotoId = fotoId,
                ProductName = productName,
                Amount = amount,
                PriceTotal = amount * price
            };

            OrderedProducts.Add(newOrderProduct);
            // Update the receipt
            string receipt = $"{price * amount}\n FotoId: {fotoId}\n Product naam: {productName}\n Aantal: {amount} ";
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
                string orderedReceipt = ShopManager.GetShopReceipt();
                string filePath = "C:\\laragon\\www\\PRA_B4_FOTOKIOSK\\PRA_B4_FOTOKIOSKreceipt.txt";
                File.WriteAllText(filePath, receipt);
            }

    }
}
