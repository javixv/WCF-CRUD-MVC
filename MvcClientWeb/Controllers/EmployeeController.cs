using MvcClientWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcClientWeb.Controllers
{
    public class EmployeeController : Controller
    {
        ServiceReference1.MisServiciosClient cliente = new ServiceReference1.MisServiciosClient();
        ServiceReference1.EmployeeDetails DM = new ServiceReference1.EmployeeDetails();
        

        // GET: Employee
        public ActionResult Index()
        {
            ViewBag.MiListado = getAll();   
            return View();
        }

        public List<MvcEmployee> getAll() {

            List<MvcEmployee> lista = new List<MvcEmployee>();            
            foreach (var s in cliente.GetAllEmployee().ToList()) {
                MvcEmployee E = new MvcEmployee();
                E.ID = s.Id;
                E.name = s.Name;
                E.salary = s.Salary;
                lista.Add(E);
            }
            return lista;
        }

        public MvcEmployee GetByName(string name) {           
            MvcEmployee Emp = new MvcEmployee();
            foreach (var s in cliente.GetEmployeeDetails(name).ToList()) {                
                Emp.ID = s.Id;
                Emp.name = s.Name;
                Emp.salary = s.Salary;             

            }
            return Emp;
        }

        public ActionResult Update(string name) {       

            if (name != null) {
                
                return View(GetByName(name));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(FormCollection form) {
            if (form["name"] != null && form["salary"] != null)
            {
                DM.Id = Convert.ToInt32(form["ID"]);
                DM.Name = form["name"];
                DM.Salary = Convert.ToInt32(form["salary"]);
                cliente.UpdateEmployee(DM);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            if (form["ID"] != null && form["name"] != null && form["salary"] != null) {
                DM.Id = Convert.ToInt32(form["ID"]);
                DM.Name = form["name"];
                DM.Salary = Convert.ToInt32(form["salary"]);

                cliente.InsertEmployeeDetails(DM);

                return RedirectToAction("Index");

            }
            return View();
        }

        public ActionResult Delete(string name) {

            return View(GetByName(name));
        }

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            cliente.DeleteEmployee(ID);
            return RedirectToAction("Index");
        }
    }
}