using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using projectCrudwhiteoutEf.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace projectCrudwhiteoutEf.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _config;
        private string SQLconnect;

        public EmployeeController(IConfiguration config)
        {
            _config = config;
            SQLconnect = _config.GetValue<string>("ConnectionStrings:DefaultConnection");
        }

        // GET: EmployeeController
        public ActionResult Index()
        {
            List<Employees> employees = new List<Employees>();

            using (SqlConnection Conn = new SqlConnection(SQLconnect))
            {
                SqlCommand Cmd = new SqlCommand("Sp_selectAllEmployess", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Conn.Open();
                SqlDataReader dr = Cmd.ExecuteReader();
                while (dr.Read())
                {
                    Employees emp = new Employees();
                    emp.EmployeeId = (int)dr["EmployeeId"];
                    emp.Name = dr["Name"].ToString();
                    emp.Surname = dr["Surname"].ToString();

                    emp.Birthday = (DateTime?)dr["Birthday"];

                    emp.GenderId = (int?)dr["GenderId"];
                    emp.PositionId = (int?)dr["PositionId"];
                    employees.Add(emp);
                }
                Conn.Close();
            };

            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            ViewData["GenderId"] = new List<SelectListItem>
            {
              new SelectListItem {  Text = "Male", Value = "1"} ,
              new SelectListItem {  Text = "Female", Value = "2"} ,
        };

            ViewData["PositionId"] = new List<SelectListItem>
            {
              new SelectListItem {  Text = "Programer", Value = "1"} ,
              new SelectListItem {  Text = "System Analyst", Value = "2"} ,
              new SelectListItem {  Text = "Tester", Value = "3"} ,
        };

            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,Birthday,GenderId,PositionId")] Employees emp)
        {
            try
            {
                using (SqlConnection Conn = new SqlConnection(SQLconnect))
                {
                     SqlCommand Cmd = new SqlCommand("Sp_insertEmployess", Conn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@Name", emp.Name);
                    Cmd.Parameters.AddWithValue("@Surname", emp.Surname);

                    Cmd.Parameters.AddWithValue("@Birthday", 
                        emp.Birthday);

                    Cmd.Parameters.AddWithValue("@GenderID", emp.GenderId);
                    Cmd.Parameters.AddWithValue("@PositionID", emp.PositionId);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                };

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }
            
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Employees emp = new Employees();
                using (SqlConnection Conn = new SqlConnection(SQLconnect))
                {
                    SqlCommand Cmd = new SqlCommand("SP_GetEmployess", Conn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@ID", id);
                    Conn.Open();

                    SqlDataReader dr = Cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        emp.EmployeeId = (int)dr["EmployeeId"];
                        emp.Name = dr["Name"].ToString();
                        emp.Surname = dr["Surname"].ToString();

                        emp.Birthday = (DateTime?)dr["Birthday"];

                        emp.GenderId = (int?)dr["GenderId"];
                        emp.PositionId = (int?)dr["PositionId"];
                    }

                    Conn.Close();
                };
                return View(emp);
            }
            catch (Exception)
            {

                throw;
            }

            
        }

        // POST: EmployeeController/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSQl(int id, [Bind("EmployeeId,Name,Surname,Birthday,GenderId,PositionId")] Employees emp)
        {
            try
            {
                using (SqlConnection Conn = new SqlConnection(SQLconnect))
                {
                    SqlCommand Cmd = new SqlCommand("Sp_UpdateEmployees", Conn);
                    Cmd.CommandType = CommandType.StoredProcedure;

                    Cmd.Parameters.AddWithValue("@ID", id);
                    Cmd.Parameters.AddWithValue("@Name", emp.Name);
                    Cmd.Parameters.AddWithValue("@Surname", emp.Surname);

                    Cmd.Parameters.AddWithValue("@Birthday",
                        emp.Birthday);

                    Cmd.Parameters.AddWithValue("@GenderID", emp.GenderId);
                    Cmd.Parameters.AddWithValue("@PositionID", emp.PositionId);


                    //List<string> allvaluekeys = new List<string>();
                    //foreach (string item in collection.Keys)
                    //{
                    //    allvaluekeys.Add(item);
                    //}
                    //var x = allvaluekeys[0];

                    //Cmd.Parameters.AddWithValue("@ID", allvaluekeys[0]);
                    //Cmd.Parameters.AddWithValue("@Name", allvaluekeys[1]);
                    //Cmd.Parameters.AddWithValue("@Surname", allvaluekeys[2]);
                    //Cmd.Parameters.AddWithValue("@Birthday",Convert.ToDateTime( allvaluekeys[3]));
                    //Cmd.Parameters.AddWithValue("@GenderID", Convert.ToInt32( allvaluekeys[4]));
                    //Cmd.Parameters.AddWithValue("@PositionID",Convert.ToInt32( allvaluekeys[5]));


                    //Cmd.Parameters.AddWithValue("@ID", collection["ID"]);
                    //Cmd.Parameters.AddWithValue("@Name", collection["Name"]);
                    //Cmd.Parameters.AddWithValue("@Surname", collection["Surname"]);
                    //Cmd.Parameters.AddWithValue("@Birthday", collection["Birthday"]);
                    //Cmd.Parameters.AddWithValue("@GenderID", collection["GenderID"]);
                    //Cmd.Parameters.AddWithValue("@PositionID", collection["PositionID"]);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                };

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Employees emp = new Employees();
                using (SqlConnection Conn = new SqlConnection(SQLconnect))
                {
                    SqlCommand Cmd = new SqlCommand("SP_GetEmployess", Conn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@ID", id);
                    Conn.Open();

                    SqlDataReader dr = Cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        emp.EmployeeId = (int)dr["EmployeeId"];
                        emp.Name = dr["Name"].ToString();
                        emp.Surname = dr["Surname"].ToString();

                        emp.Birthday = (DateTime?)dr["Birthday"];

                        emp.GenderId = (int?)dr["GenderId"];
                        emp.PositionId = (int?)dr["PositionId"];
                    }

                    Conn.Close();
                };
                return View(emp);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        public ActionResult DeleteSQL(int id)
        {
            try
            {
                using (SqlConnection Conn = new SqlConnection(SQLconnect))
                {
                    SqlCommand Cmd = new SqlCommand("Sp_deleteEmploy", Conn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@ID", id);

                    Conn.Open();
                    Cmd.ExecuteNonQuery();
                    Conn.Close();
                };

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }
        }
    }
}