using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cakes_and_Pastries_Project.Models;

namespace Cakes_and_Pastries_Project.Controllers
{
    public class DeliveryEmployeeController : Controller
    {
        Cakes_and_PastriesEntities1 DB = new Cakes_and_PastriesEntities1();
        // GET: Admin
        public ActionResult Logoff()
        {
            Session.Clear();
            return RedirectToAction("Delivery_Employee_Login");
        }
        public ActionResult Delivery_Employee_Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Delivery_Employee_Login(Delivery_Employee_Details delivery_Employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Delivery_Employee_Details employee_Details = DB.Delivery_Employee_Details.FirstOrDefault(deliver => deliver.Delivery_Employee_ID == delivery_Employee.Delivery_Employee_ID && deliver.Delivery_Employee_Password == delivery_Employee.Delivery_Employee_Password);
                    if (employee_Details != null)
                    {
                        ViewBag.deliveryemployeedetails = ":" + employee_Details.Delivery_Employee_Name;
                        Session["emp_id"] = employee_Details.Delivery_Employee_ID;
                        return RedirectToAction("View_delivery_Details");
                    }
                    else
                    {
                        ViewBag.msg = "Invalid credentials";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            return View();
        }

        public ActionResult View_delivery_Details()
        {
            try
            {
                if (Session["emp_id"] != null)
                {
                    string emp_id = Session["emp_id"].ToString();
                    List<Order_delivery_Details> order_Delivery_Details = DB.Order_delivery_Details.Where(E => E.Delivery_Employee_ID == emp_id).ToList();
                    return View(order_Delivery_Details);
                }
                else
                {
                    return RedirectToAction("Delivery_Employee_Login");
                }
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            return View();

        }

        public ActionResult Edit(int orderid)
        {
            if(Session["emp_id"]!=null)
            {
                Ordered_Cake_Details ordered_Cake_ = DB.Ordered_Cake_Details.Single(i => i.Order_ID == orderid);
                return View(ordered_Cake_);
            }
            else
            {
                return RedirectToAction("Delivery_Employee_Login");
            }
          
        }
        [HttpPost]
        public ActionResult Edit(Ordered_Cake_Details ordered_Cake_Details)
        {
            if (ModelState.IsValid)
            {
                DB.Ordered_Cake_Details.Attach(ordered_Cake_Details);
                DB.Entry(ordered_Cake_Details).Property(status => status.Status_of_Order).IsModified = true;
                DB.SaveChanges();
                return RedirectToAction("View_delivery_Details");
            }
            return View(ordered_Cake_Details);
        }
    }
}