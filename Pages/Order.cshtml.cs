using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Pastel.Data;
using Pastel.Model;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace Pastel.Pages
{
    public class OrderModel : PageModel
    {PastelDataContext _context;
        public OrderModel(PastelDataContext context)
        {
            _context = context;
        }

        [Required(ErrorMessage ="Email Obrigatório")]
        [BindProperty]
        public string ClientEmail { get; set; }
        [Required(ErrorMessage = "Morada Obrigatória")]
        [BindProperty]
        public string ShippingAddress { get; set; }
        [Required(ErrorMessage = "Quantidade Obrigatória")]
        [BindProperty]
        public int OrderQty { get; set; } = 1;

        public Product Product { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            Product = await _context.Products.FindAsync(id);
            if (Product==null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult>OnPostAsync(int id)
        {
            if (!ModelState.IsValid) return Page();
            Product = await _context.Products.FindAsync(id);

            //credenciais do envio email
            var smtpUsername = "";
            var smtpPassword = "";
            var From = "";

            if(string.IsNullOrEmpty(smtpUsername) || string.IsNullOrEmpty(smtpPassword) || string.IsNullOrEmpty(From))
            {
                TempData["NoEmail"] = 1;
                return RedirectToPage("OrderSuccess");
            }
            else
            {
                try
                {
                    await SendMailAsync(smtpUsername, smtpPassword, From);
                    Response.Redirect("../OrderSuccess");
                }
                catch
                {
                    ModelState.AddModelError("ClientEmail", "Existe um problema ao enviar o email da encomenda!");
                }
                }

            return Page();
        }

        private async Task SendMailAsync(string smtpusername, string smtppassword, string from)
        {
            string body = BuildEmailBody();
            var smtpServer = "smtp.gmail.com";
            var smtPort = 465;
            var enableSSL = false;
            SmtpClient Client = new SmtpClient(smtpServer, smtPort);
            Client.Credentials = new NetworkCredential
            {
                UserName = smtpusername,
                Password = smtppassword,
            };
            Client.EnableSsl = enableSSL;
            try
            {
                await Client.SendMailAsync(from, ClientEmail, "Encomenda Pastelaria", body);
            }
            catch
            {

            }
        }

        private string BuildEmailBody()
        {
            var body = $"Obrigado pela encomenda de {OrderQty} unidades do {Product.Name}!<br/>";
            var morada = ShippingAddress;
            var emailC = ClientEmail;
            if (!string.IsNullOrEmpty(morada))
            {
                var encomenda = morada.Replace("\r\n", "<br/>");
                body += "A morada de envio é: " + encomenda + "<br/>";
            }
            body += "Total da encomenda " + Product.Price * OrderQty + "<br/>";
            body += "Entraremos em contacto quando a encomenda se encontrar disponível para entrega.<br/>";
            return body;
        }
    }
}
