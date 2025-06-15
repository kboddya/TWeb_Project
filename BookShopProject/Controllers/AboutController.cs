using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.User;
using BookShopProject.Extension;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class AboutController : BaseController
    {
        IMessageUser _messageUser; 
        public AboutController()
        {
            _messageUser = new BusinessLogic.BusinessLogic().GetMessageUserBL();
        }
        public ActionResult Index()
        {
            SessionStatus();
            var m = new MessageForAdmin(System.Web.HttpContext.Current.GetMySessionObject());
            return View(m);
        }

        [HttpPost]
        public ActionResult SendMessage(MessageForAdmin m)
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            if (user != null && m.Email != user.Email && m.Name != user.Name)
            {
                return RedirectToAction("Index");
            }
            
            var config = new AutoMapper.MapperConfiguration(cfg=>cfg.CreateMap<MessageForAdmin, MessageDbTable>());
            var mapper = config.CreateMapper();
            var messageDb = mapper.Map<MessageDbTable>(m);

            var result = _messageUser.SendMessageToAdmin(messageDb);

            if (result)
            {
                TempData["SuccessMessage"] = "Your message has been sent successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "There was an error sending your message. Please try again later.";
            }
            

            return RedirectToAction("Index");
        }
    }
}
