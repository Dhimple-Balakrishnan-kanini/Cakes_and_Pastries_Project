using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cakes_and_Pastries_Project.Models;

namespace Cakes_and_Pastries_Project.Controllers
{
    public class AdminController : Controller
    {
        Cakes_and_PastriesEntities1 DB = new Cakes_and_PastriesEntities1();
        // GET: Admin
        public ActionResult Logoff()
        {
            Session.Clear();
            return RedirectToAction("Admin_Login");
        }
        public ActionResult Admin_Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Admin_Login(Admin_Login admin_Login)
        {
            try
            {
                
                    Admin_Login admin = DB.Admin_Login.FirstOrDefault(ad => ad.Email_ID == admin_Login.Email_ID && ad.Password == admin_Login.Password);
                    if (admin != null && admin.Role == "Admin")
                    {
                        ViewBag.Customerdetails = "Welcome Admin";
                        Session["adm_email"] = admin.Email_ID;
                        ViewBag.msg = false;
                        return RedirectToAction("View_Ordered_Details");
                    }
                    else
                    {
                        ViewBag.msg = "Invalid credentials";
                    }
                
                
            }
            catch (Exception ex)
            {
                ViewBag.msg = "Invalid credentials" ;
            }
            return View();
        }
        public ActionResult View_Ordered_Details()
        {
            try
            {
                if (Session["adm_email"] != null)
                {
                    List<Ordered_Cake_Details> ordered_Cake_Details = DB.Ordered_Cake_Details.ToList();
                    return View(ordered_Cake_Details);
                }
                else
                {
                    return RedirectToAction("Admin_Login");
                }
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            return View();

        }

        public ActionResult AddCakes()
        {
            if(Session["adm_email"]!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Admin_Login");
            }
            

        }

        [HttpPost]
        public ActionResult AddCakes(Cakes_and_Pastries_Details cakes_And_Pastries_)
        {
            string filename = Path.GetFileNameWithoutExtension(cakes_And_Pastries_.cakeimg.FileName);
            string extension = Path.GetExtension(cakes_And_Pastries_.cakeimg.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            cakes_And_Pastries_.Cake_Image = "~/CakeImages/" + filename;
            filename = Path.Combine(Server.MapPath("~/CakeImages/"), filename);
            cakes_And_Pastries_.cakeimg.SaveAs(filename);
            using (Cakes_and_PastriesEntities1 cakes = new Cakes_and_PastriesEntities1())
            {
                cakes.Cakes_and_Pastries_Details.Add(cakes_And_Pastries_);
                cakes.SaveChanges();
            }
            //string cakeid = cakes_And_Pastries_.Cake_ID;
            //string caketype = cakes_And_Pastries_.Cake_and_Pastries_Type;
            //string cakename = cakes_And_Pastries_.Cake_Name;
            //int price = Convert.ToInt32(cakes_And_Pastries_.Price);
            //Add_to_Cart add_To_Cart = new Add_to_Cart();
            //add_To_Cart.Cake_ID = cakeid;
            //add_To_Cart.Cake_and_Pastries_Type = caketype;
            //add_To_Cart.Cake_Name = cakename;
            //add_To_Cart.Price = price;
            //using(Cakes_and_PastriesEntities1 cakes_=new Cakes_and_PastriesEntities1())
            //{
            //    cakes_.Add_to_Cart.Add(add_To_Cart);
            //    cakes_.SaveChanges();
            //}

            ModelState.Clear();
            return View();

        }

        //public ActionResult ViewOrder_Admin()
        //{
        //    return View(DB.Ordered_Cake_Details.ToList());
        //}
        public ActionResult Edit(int orderid)
        {
            if(Session["adm_email"]!=null)
            {
                Ordered_Cake_Details ordered_Cake_ = DB.Ordered_Cake_Details.Single(i => i.Order_ID == orderid);
                return View(ordered_Cake_);
            }
            else
            {
                return RedirectToAction("Admin_Login");
            }
           
        }
        [HttpPost]
        public ActionResult Edit(Ordered_Cake_Details ordered_Cake_Details)
        {
            if(ModelState.IsValid)
            {
                DB.Ordered_Cake_Details.Attach(ordered_Cake_Details);
                DB.Entry(ordered_Cake_Details).Property(status => status.Status_of_Order).IsModified = true;
                DB.SaveChanges();
                return RedirectToAction("View_Ordered_Details");
            }
            return View(ordered_Cake_Details);
        }
        public ActionResult Redirect_to_DeliveryBoy(int orderid)
        {
            if(Session["adm_email"]!=null)
            {
                Ordered_Cake_Details ordered = DB.Ordered_Cake_Details.FirstOrDefault(id => id.Order_ID == orderid);
                Delivery_Details delivery = DB.Delivery_Details.FirstOrDefault(mail => mail.Email_ID == ordered.Email_ID);
                OrderDeliveryViewModel viewModel = new OrderDeliveryViewModel();
                viewModel.Order_ID = ordered.Order_ID;
                viewModel.Email_ID = delivery.Email_ID;
                viewModel.Customer_Name = delivery.Customer_Name;
                viewModel.Mobile_Number = delivery.Mobile_Number;
                viewModel.Delivery_Address = delivery.Delivery_Address;
                viewModel.Date_Of_Delivery = delivery.Date_Of_Delivery;
                viewModel.Time_Of_Delivery = delivery.Time_Of_Delivery;
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Admin_Login");
            }
            
        }
        [HttpPost]
        public ActionResult Redirect_to_DeliveryBoy(OrderDeliveryViewModel orderDeliveryView)
        {
            Ordered_Cake_Details ordered_Cake_Details = DB.Ordered_Cake_Details.FirstOrDefault(id => id.Order_ID == orderDeliveryView.Order_ID);
            Order_delivery_Details _Delivery_Details = new Order_delivery_Details();
            _Delivery_Details.Delivery_Employee_ID = orderDeliveryView.Delivery_Employee_ID;
            _Delivery_Details.Order_ID = orderDeliveryView.Order_ID;
            _Delivery_Details.Email_ID = orderDeliveryView.Email_ID;
            _Delivery_Details.Customer_Name = orderDeliveryView.Customer_Name;
            _Delivery_Details.Mobile_Number = orderDeliveryView.Mobile_Number;
            _Delivery_Details.Delivery_Address = orderDeliveryView.Delivery_Address;
            _Delivery_Details.Date_Of_Delivery = orderDeliveryView.Date_Of_Delivery;
            _Delivery_Details.Time_Of_Delivery = orderDeliveryView.Time_Of_Delivery;
            DB.Order_delivery_Details.Add(_Delivery_Details);
            DB.SaveChanges();
            return RedirectToAction("View_Ordered_Details");
        }
    }
}