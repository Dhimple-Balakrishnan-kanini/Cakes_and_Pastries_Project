using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cakes_and_Pastries_Project.Models;

namespace Cakes_and_Pastries_Project.Controllers
{
    public class CustomerController : Controller
    {
        Cakes_and_PastriesEntities1 DB = new Cakes_and_PastriesEntities1();
        

       

        // GET: Customer
        public ActionResult Logoff()
        {
            Session.Clear();
            return RedirectToAction("Home");

        }

        public ActionResult Register()
        {
            Customer_Details customer = new Customer_Details();
            return View(customer);
        }

        [HttpPost]
        public ActionResult Register(Customer_Details customer)
        {

            try
            {
                if(ModelState.IsValid)
                {
                    if(customer!=null)
                    {
                        DB.Customer_Details.Add(customer);
                        DB.SaveChanges();
                        return RedirectToAction("Customer_Login");
                    }
                }
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View();
        }
       
        public ActionResult Customer_Login()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Customer_Login(Customer_Details customer)
        {
            try
            {
               
                    Customer_Details customer_Details = DB.Customer_Details.FirstOrDefault(cus => cus.Email_ID == customer.Email_ID && cus.Password == customer.Password);
                    if(customer_Details != null)
                    {
                        
                        Session["Cus_email"] = customer_Details.Email_ID;
                        return RedirectToAction("Home");
                    }
                    else
                    {
                        ViewBag.msg = "Invalid credentials";
                    }
                
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message; ;
            }
            return View();
        }
        public ActionResult Home()
        {

            try
            {
                
                List<Cakes_and_Pastries_Details> cakes_And_Pastries = DB.Cakes_and_Pastries_Details.ToList();
                if(Session["Cus_email"]!=null)
                {
                    ViewBag.msg = Session["Cus_email"].ToString();
                }
                return View(cakes_And_Pastries);
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            return View();
        }

         public ActionResult Add_To_Cart(string cakeid)
        {
            try
            {
                if (Session["Cus_email"] != null)
                {
                    Cakes_and_Pastries_Details cakes_And_Pastries_ = DB.Cakes_and_Pastries_Details.FirstOrDefault(id => id.Cake_ID == cakeid);
                    ViewCartItems view = new ViewCartItems();
                    if (cakes_And_Pastries_ != null)
                    {
                        view.Cake_ID = cakes_And_Pastries_.Cake_ID;
                        view.Cake_and_Pastries_Type = cakes_And_Pastries_.Cake_and_Pastries_Type;
                        view.Cake_Name = cakes_And_Pastries_.Cake_Name;
                        view.Price = cakes_And_Pastries_.Price;
                        return View(view);

                    }
                    //return View(view);
                }
                else
                {
                    return RedirectToAction("Customer_Login");
                }
            }
            catch(Exception ex)
            {
                ViewBag.msg = "Error" + ex.Message;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Add_To_Cart(ViewCartItems viewCart)
        {
            string mail = Session["Cus_email"].ToString();
            //Cakes_and_Pastries_Details cakes_And_Pastries_ = DB.Cakes_and_Pastries_Details.FirstOrDefault(c => c.Cake_ID == viewCart.Cake_ID);
            Add_to_Cart add = new Add_to_Cart();
            add.Email_ID = mail;
            add.Cake_ID = viewCart.Cake_ID;
            add.Cake_and_Pastries_Type = viewCart.Cake_and_Pastries_Type;
            add.Cake_Name = viewCart.Cake_Name;
            add.Quantity = viewCart.Quantity;
            add.Price = (viewCart.Price)*(viewCart.Quantity);
            DB.Add_to_Cart.Add(add);
            DB.SaveChanges();
            return RedirectToAction("Home");
        }
        
        public ActionResult Remove_From_Cart(string cakeid)
        {
            if(Session["Cus_email"!=null)
            {
                if (cakeid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Add_to_Cart cart = DB.Add_to_Cart.Find(cakeid);
                if (cart == null)
                {
                    return HttpNotFound();
                }
                return View(cart);
            }
            else
            {
                return RedirectToAction("Customer_Login");
            }
        }
        [HttpPost,ActionName("Remove_From_Cart")]
        public ActionResult Delete(string cakeid)
        {
            Add_to_Cart cart = DB.Add_to_Cart.Find(cakeid);
            DB.Add_to_Cart.Remove(cart);
            DB.SaveChanges();
            return RedirectToAction("viewcartitems");
        }

        [Route("View Cart")]
        public ActionResult viewcartitems()
        {
            try
            {
                if(Session["Cus_email"]!=null)
                {
                    string id = Session["Cus_email"].ToString();
                    List<Add_to_Cart> viewcitem = DB.Add_to_Cart.Where(c=>c.Email_ID==id).ToList();
                    return View(viewcitem);
                }
                else
                {
                    return RedirectToAction("Customer_Login");
                }
              
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            return View();
        }
        
        public ActionResult DeliveryDetails()
        {
            string mail = Session["Cus_email"].ToString();
            Customer_Details customer_Details = DB.Customer_Details.FirstOrDefault(id => id.Email_ID == mail);
            CustomerDeliveryDetails customer = new CustomerDeliveryDetails();
            if(customer_Details!=null)
            {
                customer.Email_ID = mail;
                customer.Customer_Name = customer_Details.Customer_Name;
                customer.Mobile_Number = customer_Details.Mobile_Number;
            }
            return View(customer);
        }
        [HttpPost]
        public ActionResult DeliveryDetails(CustomerDeliveryDetails customerDeliveryDetails)
        {
            Customer_Details customer = DB.Customer_Details.FirstOrDefault(c => c.Email_ID == customerDeliveryDetails.Email_ID);
            Delivery_Details details = new Delivery_Details();

            details.Email_ID = customerDeliveryDetails.Email_ID;
            details.Customer_Name = customerDeliveryDetails.Customer_Name;
            details.Mobile_Number = customerDeliveryDetails.Mobile_Number;
            details.Delivery_Address = customerDeliveryDetails.Delivery_Address;
            details.Date_Of_Delivery = customerDeliveryDetails.Date_Of_Delivery;
            details.Time_Of_Delivery = customerDeliveryDetails.Time_Of_Delivery;
            DB.Delivery_Details.Add(details);
            DB.SaveChanges();
            return RedirectToAction("ViewOrderedDetails");
        }
       
        public ActionResult ViewOrderedDetails()
        {
            try
            {
                if (Session["Cus_email"] != null)
                {
                    string mail = Session["Cus_email"].ToString();
                    Customer_Details customer_Details = DB.Customer_Details.FirstOrDefault(id => id.Email_ID == mail);
                    Add_to_Cart add_To_Cart = DB.Add_to_Cart.FirstOrDefault(m => m.Email_ID == mail);
                    ConfirmOrderDetails confirmOrder = new ConfirmOrderDetails();
                    if (add_To_Cart != null)
                    {
                        confirmOrder.Email_ID = mail;
                        confirmOrder.Cake_Id = add_To_Cart.Cake_ID;
                        confirmOrder.Cake_Name = add_To_Cart.Cake_Name;
                        confirmOrder.Quantity = add_To_Cart.Quantity;
                        confirmOrder.Price = add_To_Cart.Price;
                    }
                    else
                    {
                        ViewBag.msg = "Your cart is empty";
                    }
                    return View(confirmOrder);
                }
                else
                {
                    return RedirectToAction("Customer_Login");
                }
            }
            catch(Exception ex)
            {
                ViewBag.msg = "Error" + ex.Message;
            }
            return View();
        }
        [HttpPost]
        public ActionResult ViewOrderedDetails(ConfirmOrderDetails confirm)
        {
            Ordered_Cake_Details ordered = new Ordered_Cake_Details();
            try
            {
                ordered.Email_ID = confirm.Email_ID;
                ordered.Cake_Id = confirm.Cake_Id;
                ordered.Cake_Name = confirm.Cake_Name;
                ordered.Quantity = confirm.Quantity;
                ordered.Price = confirm.Price;
                ordered.Status_of_Order = "Order Placed";
                DB.Ordered_Cake_Details.Add(ordered);
                DB.SaveChanges();
            }
            catch(Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            return RedirectToAction("Home");

        }
        [Route("View Order")]
        public ActionResult ViewOrder()
        {
            if(Session["Cus_email"]!=null)
            {
                return View(DB.Ordered_Cake_Details.ToList());
            }
            else
            {
                return RedirectToAction("Customer_Login");
            }
            
        }

        public ActionResult FeedBack()
        {
          if(Session["Cus_email"]!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Customer_Login");
            }
            
        }
        [HttpPost]
        public ActionResult FeedBack(FeedBack feed,string cakeid,string cakename)
        {
            feed.Cake_ID = cakeid;
            feed.Cake_Name = feed.Cake_Name;
            DB.FeedBacks.Add(feed);
            DB.SaveChanges();
            return View();
        }
        public ActionResult ViewFeed(string cakeid)
        {
            if(Session["Cus_email"]!=null)
            {
                var feedback = DB.FeedBacks.Where(id => id.Cake_ID == cakeid);
                return View(feedback);
            }
            else
            {
                return RedirectToAction("Customer_Login");
            }
          
        }

        public ActionResult Search()
        {
            return View(DB.Cakes_and_Pastries_Details.ToList());
        }
        public JsonResult GetSearchData(string SearchBy, string SearchValue)
        {
            List<Cakes_and_Pastries_Details> cakes = new List<Cakes_and_Pastries_Details>();
            if (SearchBy == "Cake_and_Pastries_Type")
            {
                try
                {
                    DB.Configuration.ProxyCreationEnabled = false;
                    var cake = SearchValue;
                    cakes = DB.Cakes_and_Pastries_Details.Where(b => b.Cake_and_Pastries_Type == cake || SearchValue == null).ToList();

                }
                catch (FormatException)
                {
                    ViewBag.error = "Entered Value is not Category of the Book";
                }
                return Json(cakes, JsonRequestBehavior.AllowGet);
            }
            return Json(cakes, JsonRequestBehavior.AllowGet);

        }
    }


}
