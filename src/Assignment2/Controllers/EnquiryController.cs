using Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

public class EnquiryController : Controller
{
    // GET: Home
    public ActionResult Index()
    {
        return View();
    }


    [HttpPost]
    public ActionResult Index(Enquiry enq)
    {
        string connectionString = null;
        string sql = null;


        connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=Assignment2; Trusted_Connection=True";

        using (SqlConnection cnn = new SqlConnection(connectionString))
        {
            sql = "insert into Enquiry (Email, [Message]) values (@Email, @Message)";
            cnn.Open();

            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                cmd.Parameters.AddWithValue("@Email", enq.Email);
                cmd.Parameters.AddWithValue("@Message", enq.Message);

                if (ModelState.IsValid)
                {
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                    ViewBag.Message = "1";
                    return RedirectToAction("Index");
                }
                return View(enq);
            }
        }
    }
}
